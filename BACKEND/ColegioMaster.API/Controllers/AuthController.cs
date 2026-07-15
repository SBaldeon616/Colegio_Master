using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ColegioMaster.DtoModels.Auth;
using ColegioMaster.DtoModels.Comp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ColegioMaster.API.Controllers
{
    /// <summary>
    /// Controlador para manejar las autenticaciones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="config"></param>
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Metodo que permite realizar el login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<AuthResponse>>> Login([FromBody] AuthRequest request)
        {
            GeneralResponse<AuthResponse> response = new GeneralResponse<AuthResponse>();

            if (request == null)
            {
                response.Title = "ADVERTENCIA";
                response.Message = "Credenciales incorrectas 001";
                response.Success = false;
                response.ShowAlert = true;

                return BadRequest(response);
            }

            // CORRECCIÓN: Cambiado '&&' por '||' para que valide correctamente ambas credenciales
            if (request.Username != "admin" || request.Password != "123456")
            {
                response.Title = "ADVERTENCIA";
                response.Message = "Credenciales incorrectas 002";
                response.Success = false;
                response.ShowAlert = true;
                return Unauthorized(response);
            }

            AuthResponse auth = new();
            auth.Id = 1;
            auth.ChangedPassword = false;
            auth.IdRole = 1;

            // GENERAMOS EL TOKEN usando el método privado y lo asignamos al DTO
            auth.Token = GenerarJwtToken(auth.Id.ToString(), request.Username, auth.IdRole.ToString());

            response.Title = "OK";
            response.Message = "Autenticación Correcta";
            response.Success = true;
            response.ShowAlert = true;
            response.Content = auth;

            return Ok(response);
        }

        // METODO PRIVADO PARA LA GENERACIÓN DEL TOKEN
        private string GenerarJwtToken(string usuarioId, string username, string rolId)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Estructura de la información (Claims) que viajará encriptada en el Token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioId),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, rolId)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // El token expira en 2 horas
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}