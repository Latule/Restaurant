using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Repositories.Dtos;
using Repositories.ServicesInterfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly IStoreService _storeService;
        private readonly ILogger<CommandsController> _logger;


        public CommandsController(ICommandService commandService, IStoreService storeService, ILogger<CommandsController> logger)
        {
            _commandService = commandService;
            _storeService = storeService;
            _logger = logger;
        }


        // GET: api/Commands
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_commandService.GetAllAsDtos());
            }
            catch(Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }


        [HttpGet("{date1}/{date2}")]

        public IActionResult Get(DateTime date1, DateTime date2)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //check for bad interval
                if (date1 > date2)
                {
                    return BadRequest("Bad interval");
                }

                var result = _commandService.GetAllAsDtos().FindAll(c => date1 <= c.Date && c.Date <= date2);
                if (result.Count==0)
                {
                    return NoContent();
                }

                return Ok(result);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var dbProvider = _commandService.GetByIdDto(id);

                if (dbProvider == null)
                {
                    return NotFound($"Command with id {id} hasn't been found");
                }

                return Ok(dbProvider);


            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }

        // POST: api/Commands
        [HttpPost]
        public IActionResult Post([FromBody] CommandDto command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pro = _commandService.Create(command);

                if (pro==null)
                {
                    return NoContent();
                }

                var uri = new Uri($"{Request.GetDisplayUrl()}/{pro.Id}");


                //update storage


                foreach (var menu in pro.CommandMenus)
                {
                    foreach (var ingredient in menu.Menu.MenuIngredients)
                    {
                        var storage = _storeService.GetAll().Find(s => s.Name.Equals(ingredient.Ingredient.Name));

                        _storeService.Update(storage, new StoreIngredient { Id = storage.Id, Name = storage.Name, Quantity = storage.Quantity - ingredient.Ingredient.Quantity });
                    }
                    
                }


                return Created(uri,pro);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }


    }
}
