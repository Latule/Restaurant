using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Dtos;
using Repositories.ServicesInterfaces;
using Serilog;
using ILogger = Castle.Core.Logging.ILogger;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {

        private readonly IProviderService _providerService;

        private readonly ILogger<ProvidersController> _logger;

        public ProvidersController(IProviderService providerService, ILogger<ProvidersController> logger)
        {
            _providerService = providerService;
            _logger = logger;
        }

        // GET: api/Providers
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_providerService.GetAllAsDtos());
            }
            catch(Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }

        // GET: api/Providers/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var dbProvider = _providerService.GetByIdDto(id);

                if (dbProvider == null)
                {
                    return NotFound($"Provider with id {id} hasn't been found");
                }

               return Ok(dbProvider);
               
                
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }   
        }

        // POST: api/Providers
        [HttpPost]
        public IActionResult Post([FromBody] ProviderDto provider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_providerService.GetAll().Exists(p => p.Name.Equals(provider.Name)))
                    return BadRequest("Already exist a producer with this name");

                var pro = _providerService.Create(provider);

                var uri = new Uri($"{Request.GetDisplayUrl()}/{pro.Id}");

                return Created(uri, pro);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }

        // PUT: api/Providers/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProviderDto provider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var dbProvider = _providerService.GetById(id);

                if (dbProvider == null)
                {
                    return NotFound($"Provider with id {id} hasn't been found");
                }

                _providerService.Update(dbProvider, provider);

                var uri = new Uri($"{Request.GetDisplayUrl()}/{dbProvider.Id}");

                return Accepted(uri);


            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var provider = _providerService.GetById(id);

                if (provider == null)
                {
                    return NotFound($"Provider with id {id} hasn't been found");
                }
                else
                {
                    _providerService.Delete(provider);

                    return NoContent();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }
    }
}
