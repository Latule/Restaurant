using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Data.Entities;
using Data.Entities.Invoice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Dtos;
using Repositories.ServicesInterfaces;
using Services.Services;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {

        private readonly IStoreService _storeService;
        private readonly IInvoiceService _invoiceService;
        private readonly IProviderService _providerService;

        private readonly ILogger<StoresController> _logger;

        public StoresController(IStoreService storeService, IInvoiceService invoiceService, IProviderService providerService, ILogger<StoresController> logger)
        {
            _storeService = storeService;
            _invoiceService = invoiceService;
            _providerService = providerService;
            _logger = logger;
        }


        // GET: api/Stores
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_storeService.GetAllAsDtos());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }


        // POST: api/Stores
        [HttpPost]
        public IActionResult Post([FromBody] List< StoreIngredientDto> storeDto)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                Invoice invoice = new Invoice
                {
                    Id = Guid.NewGuid(), Date = DateTime.Now, InvoiceProviders = new List<InvoiceProviders>()
                };


                var providers = _providerService.GetAll();

                foreach (var ingredient in storeDto)
                {
                   //we get all the providers with that ingredient name
                   var providersWithIngredient= providers.FindAll(p =>
                                                                  p.ProviderIngredients.Exists(i =>
                                                                                               i.Ingredient.Name.Equals(ingredient.Name)));


                   if (providersWithIngredient.Count != 0)
                   {
                        //sort them by the ingredient price
                        providersWithIngredient.Sort((p1,p2) =>
                            p1.ProviderIngredients.Find(i=>
                                i.Ingredient.Name.Equals(ingredient.Name)).Ingredient.Price.CompareTo(
                            p2.ProviderIngredients.Find(i=> 
                                i.Ingredient.Name.Equals(ingredient.Name)).Ingredient.Price)  );


                        var currentQuantity = 0;

                        providersWithIngredient.ForEach(
                            p =>
                            {
                                if (currentQuantity < ingredient.Quantity)
                                {
                                    var findIngredient =
                                        p.ProviderIngredients.Find(i => i.Ingredient.Name.Equals(ingredient.Name));

                                    if (findIngredient.Ingredient.Quantity > 0)
                                    {
                                        int quantity;

                                        if (ingredient.Quantity - currentQuantity <= findIngredient.Ingredient.Quantity)
                                        {
                                            quantity = ingredient.Quantity - currentQuantity;
                                        }
                                        else
                                        {
                                            quantity = findIngredient.Ingredient.Quantity;
                                        }

                                        currentQuantity += quantity;

                                        var ing = new InvoiceProviderIngredient()
                                        {
                                            Id = Guid.NewGuid(),
                                            Name = ingredient.Name,
                                            Price = findIngredient.Ingredient.Price,
                                            Quantity = quantity
                                        };

                                        invoice.Price += quantity * findIngredient.Ingredient.Price;

                                        var exist = invoice.InvoiceProviders.Find(ip =>
                                            ip.Provider.IdProvider.Equals(p.Id));

                                        if (exist == null)
                                        {
                                            var pi = new InvoiceProviderIngredients()
                                            {
                                                Id = Guid.NewGuid(), IdIngredient = ing.Id, IdProvider = p.Id,
                                                Ingredient = ing,
                                                Provider = new InvoiceProvider()
                                                {
                                                    Id = Guid.NewGuid(), Name = p.Name,
                                                    ProviderIngredients = new List<InvoiceProviderIngredients>(),
                                                    IdProvider = p.Id
                                                }
                                            };

                                            pi.Provider.ProviderIngredients.Add(pi);

                                            var invoiceProviders = new InvoiceProviders
                                            {
                                                Id = Guid.NewGuid(),
                                                Invoice = invoice,
                                                IdInvoice = invoice.Id,
                                                IdProvider = p.Id,
                                                Provider = new InvoiceProvider()
                                                {
                                                    Id = Guid.NewGuid(), Name = p.Name,
                                                    ProviderIngredients = new List<InvoiceProviderIngredients>(),
                                                    IdProvider = p.Id
                                                }

                                            };

                                            invoiceProviders.Provider.ProviderIngredients.Add(pi);
                                            invoice.InvoiceProviders.Add(invoiceProviders);

                                        }
                                        else
                                        {
                                            exist.Provider.ProviderIngredients.Add(
                                                new InvoiceProviderIngredients()
                                                {
                                                    Id = Guid.NewGuid(),
                                                    IdIngredient = ing.Id,
                                                    IdProvider = exist.IdProvider,
                                                    Ingredient = ing,
                                                    Provider = exist.Provider
                                                }
                                            );
                                        }

                                    }
                                }

                            }

                        );                            
                   }

                }


                if (invoice.InvoiceProviders.Count == 0)
                {
                    return NotFound();
                }

                var result= _invoiceService.Create(invoice);
                //update stores

                foreach (var provider in invoice.InvoiceProviders)
                {
                    foreach (var ingredient in provider.Provider.ProviderIngredients)
                    {
                        var storage = _storeService.GetAll().Find(s => s.Name.Equals(ingredient.Ingredient.Name));
                        if (storage==null)
                        {
                            _storeService.Create(new StoreIngredientDto()
                                {Name = ingredient.Ingredient.Name, Quantity = ingredient.Ingredient.Quantity});
                        }
                        else
                        {
                            _storeService.Update(storage,new StoreIngredient{Id = storage.Id,Name = storage.Name,Quantity = storage.Quantity+ingredient.Ingredient.Quantity});
                        }

                        var dbProvider = _providerService.GetById(provider.IdProvider);

                        var providerStorage = _providerService.Transform(dbProvider);

                        providerStorage.Ingredients.Find(p => p.Name.Equals(ingredient.Ingredient.Name)).Quantity -=ingredient.Ingredient.Quantity;

                        _providerService.Update(dbProvider,providerStorage);

                    }
                    
                }

                return Ok(result);


            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }


    }
}
