using HoursBank.Domain.Dtos;
using HoursBank.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HoursBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
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
                var result = await _teamService.GetAll();
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
        [Route("GetById/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _teamService.Get(id);
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
        [Route("GetByName")]
        public async Task<ActionResult> GetByName([FromBody] TeamDto team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _teamService.GetByName(team.Name);
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
        public async Task<ActionResult> Post([FromBody] TeamDto team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _teamService.Post(team);
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
        public async Task<ActionResult> Put([FromBody] TeamDto team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _teamService.Put(team);
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
                return Ok(await _teamService.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }
    }
}
