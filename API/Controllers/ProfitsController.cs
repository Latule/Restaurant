using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.ServicesInterfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitsController : ControllerBase
    {

        private readonly ICommandService _commandService;
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger<ProfitsController> _logger;

        public ProfitsController(ICommandService commandService, IInvoiceService invoiceService, ILogger<ProfitsController> logger)
        {
            _commandService = commandService;
            _invoiceService = invoiceService;
            _logger = logger;
        }

        [HttpGet("{day}/{month}/{year}")]

        public IActionResult GetIncome(int day, int month, int year)
        {
            try
            {
                if (!ModelState.IsValid || day>31 || day <1 || month <1 || month>12 || year<2000 || year >DateTime.Now.Year )
                {
                    return BadRequest("Invalid date");
                }

                var result = _commandService
                    .GetAllAsDtos()
                    .FindAll(c => day==c.Date.Day && month==c.Date.Month && year==c.Date.Year)
                    .Sum(p=>p.Price);
   
     
                return Ok(result);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }


        [HttpGet("{month}/{year}")]

        public IActionResult GetProfitMonth(int month,int year)
        {
            try
            {
                if (!ModelState.IsValid || year < 2000 || year > DateTime.Now.Year || month<1 || month>12 )
                {
                    return BadRequest("Invalid date");
                }

                var income = _commandService
                    .GetAllAsDtos()
                    .FindAll(c =>month == c.Date.Month && year == c.Date.Year)
                    .Sum(p => p.Price);

                var spend = _invoiceService
                    .GetAll()
                    .FindAll(c => month == c.Date.Month && year == c.Date.Year)
                    .Sum(p => p.Price);

                return Ok(income-spend);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("{year}")]

        public IActionResult GetProfitYear(int year)
        {
            try
            {
                if (!ModelState.IsValid || year<2000 || year>DateTime.Now.Year)
                {
                    return BadRequest(ModelState);
                }



                var income = _commandService
                    .GetAllAsDtos()
                    .FindAll(c => year == c.Date.Year)
                    .Sum(p => p.Price);

                var spend = _invoiceService
                    .GetAll()
                    .FindAll(c => year == c.Date.Year)
                    .Sum(p => p.Price);

                return Ok(income - spend);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(500, exception.Message);
            }
        }

    }
}
