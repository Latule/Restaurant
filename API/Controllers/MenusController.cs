using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Dtos;
using Repositories.ServicesInterfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {

        private readonly IMenuService _menuService;

        private readonly ILogger<MenusController> _logger;

        public MenusController(IMenuService menuService, ILogger<MenusController> logger)
        {
            _menuService = menuService;
            _logger = logger;
        }

        // GET: api/Menus
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_menuService.GetAllAsDtos());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var dbProvider = _menuService.GetByIdDto(id);

                if (dbProvider == null)
                {
                    return NotFound($"Menu with id {id} hasn't been found");
                }

                return Ok(dbProvider);


            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }

        // POST: api/Menus
        [HttpPost]
        public IActionResult Post([FromBody] MenuDto menuDto )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pro = _menuService.Create(menuDto);

                var uri = new Uri($"{Request.GetDisplayUrl()}/{pro.Id}");

                return Created(uri, pro);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }

        // PUT: api/Menus/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] MenuDto menuDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var dbMenu = _menuService.GetById(id);

                if (dbMenu == null)
                {
                    return NotFound($"Provider with id {id} hasn't been found");
                }

                _menuService.Update(dbMenu, menuDto);

                var uri = new Uri($"{Request.GetDisplayUrl()}/{dbMenu.Id}");

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
                var provider = _menuService.GetById(id);

                if (provider == null)
                {
                    return NotFound($"Provider with id {id} hasn't been found");
                }
                else
                {
                    _menuService.Delete(provider);

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
