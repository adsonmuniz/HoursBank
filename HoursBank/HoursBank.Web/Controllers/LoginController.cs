using HoursBank.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HoursBank.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Login(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44382/api/Login/Login", model);
            var authenticationResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            if (authenticationResponse != null && authenticationResponse.Authenticated)
            {
                // usuário autenticado com sucesso
                return Ok(authenticationResponse);
            }
            else
            {
                // exiba mensagem de erro ao usuário
                return BadRequest(authenticationResponse.Message);
            }
        }
    }
}
