using IOASYS_IMDb.Authentication;
using IOASYS_IMDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOASYS_IMDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;


        public UsuarioController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext contexto)
        {
            _context = contexto;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Inclui um novo Usuario
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/AtorFilme
        ///     {
        ///        "username": "string",
        ///       "email": "string",
        ///        "password": "string"
        ///               ///       
        ///     }
        /// </remarks>
        /// <param name="model">objeto Filme</param>
        /// <returns>O objeto Filme incluido</returns>
        [HttpPost]
        [Route("register-usuario")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser();

            user.Email = model.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.UserName = model.Username;
           
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            var userExists2 = await userManager.FindByNameAsync(user.UserName);
            Usuario usuario = new Usuario
            {
                UsuarioId = userExists2.Id,
                Username = user.UserName,
                Email = user.Email

            };

            
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();




            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] RegisterModel model)
        {

            try
            {
                ApplicationUser userExist = await userManager.FindByIdAsync(id);

                if (model.Email == null || model.Username == null)
                {
                    model.Email = userExist.Email;
                    model.Username = userExist.UserName;
                }
                userExist.Email = model.Email;
                userExist.UserName = model.Username;
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                    userExist.PasswordHash = passwordHash.ToString();

                }

                var result = await userManager.UpdateAsync(userExist);

                Usuario usuario = new Usuario
                {
                    Username = model.Username,
                    Email = model.Email

                };

                _context.Entry(usuario).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok($"Dados com id={id} foi atualizada com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Erro ao tentar atualizar dados com id={id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                ApplicationUser userExist = await userManager.FindByIdAsync(id);

                if (userExist == null)
                {
                    return NotFound($"Os dados com id={id} não foi encontrado");
                }
                var result = await userManager.DeleteAsync(userExist);

                var usuario = _context.Usuarios.FirstOrDefault(p => p.UsuarioId == id);
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();

                return Ok($"Dados com id={id} foi atualizada com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Erro ao tentar atualizar dados com id={id}");
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }




    }
}
