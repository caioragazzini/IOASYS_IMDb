using IOASYS_IMDb.Authentication;
using IOASYS_IMDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiFilmesIMDb.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DiretorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DiretorController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }


        /// <summary>
        /// Obtem todos os Diretores
        /// </summary>
        /// <returns>Objetos Diretor</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Diretor>> Get()
        {
            try
            {
                return _context.Diretors.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Diretores do banco de dados");

            }


        }

        [HttpGet("Filmes")]
        public ActionResult<IEnumerable<Diretor>> GetDiretorFilmes()
        {
            try
            {
                return _context.Diretors.Include(x => x.Filmes).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Filmes e Diretores do banco de dados");

            }

        }

        /// <summary>
        /// Obtem um Diretor pelo seu Id
        /// </summary>
        ///  Obtem um Diretor pelo seu Id
        /// <returns>ObjetosDiretor</returns>
        [HttpGet("{id}")]
        public ActionResult<Diretor> Get(int id)
        {
            try
            {
                var diretor = _context.Diretors.FirstOrDefault(p => p.DiretorId == id);

                if (diretor == null)
                {
                    return NotFound($"O Diretor com id={id} não foi encontrado");
                }
                return diretor;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Diretores do banco de dados");

            }

        }

        /// <summary>
        /// Inclui um novo Diretor
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/Diretor
        ///     {
        ///        "diretorId": 1,
        ///        "nome": "Nome do Diretor com 150 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="diretor">objeto Diretor</param>
        /// <returns>O objeto Diretor incluido</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Diretor diretor)
        {

            try
            {
                _context.Diretors.Add(diretor);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterDiretor",
                    new { id = diretor.DiretorId }, diretor);
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Erro ao tentar criar uma novo Diretor(a)");
            }
        }

        /// <summary>
        /// Atualiza um Diretor
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     PUT api/Diretor
        ///     {
        ///        "diretorId": 1,
        ///        "nome": "Nome do Diretor com 150 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="diretor">objeto Diretor</param>
        /// <returns>O objeto Diretor atualizado</returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Diretor diretor)
        {

            try
            {

                if (id != diretor.DiretorId)
                {
                    return BadRequest($"Não foi possível atualizar os dados com id={id}");
                }

                _context.Entry(diretor).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok($"Dados com id={id} foi atualizada com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Erro ao tentar atualizar dados com id={id}");
            }

        }

        /// <summary>
        /// Exclui um Diretor
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     PUT api/Diretor
        ///     {
        ///        "diretorId": 1
        ///        
        ///       
        ///     }
        /// </remarks>
        /// <param name="id">objeto Diretor</param>
        /// <returns>O objeto Diretor Excluido</returns>
        [HttpDelete("{id}")]
        public ActionResult<Diretor> Delete(int id)
        {
            try
            {
                var diretor = _context.Diretors.FirstOrDefault(p => p.DiretorId == id);

                if (diretor == null)
                {
                    return NotFound($"Os dados com id={id} não foi encontrado");
                }

                _context.Diretors.Remove(diretor);
                _context.SaveChanges();
                return diretor;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao excluir os dados de id = {id}");
            }

        }
    }

}

