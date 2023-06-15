using HoursBank.Domain.Dtos;
using HoursBank.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HoursBank.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _bankService.GetAll();
                if (result != null)
                {
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _bankService.Get(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }

        [HttpPost]
        [Route("Filter")]
        public async Task<ActionResult> Get([FromBody] BankDto bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _bankService.Get(bank);
                if (result != null)
                {
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }

        [HttpGet]
        [Route("GetByCoordinator/{id}")]
        public async Task<ActionResult> GetByCoordinator(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _bankService.GetByCoordinator(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> Post([FromBody] BankDto bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _bankService.Post(bank);
                if (result != null)
                {
                    return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.Created }; // StatusCode 201
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Put([FromBody] BankDto bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _bankService.Put(bank);
                if (result != null)
                {
                    return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.Accepted }; // StatusCode 202
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _bankService.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }
    }
}
