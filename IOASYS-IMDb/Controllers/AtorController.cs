using IOASYS_IMDb.Authentication;
using IOASYS_IMDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace IOASYS_IMDb.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AtorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AtorController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }


        /// <summary>
        /// Obtem todos os Atores
        /// </summary>
        /// <returns>Objetos Ator</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Ator>> Get()
        {
            try
            {

                return _context.Ators.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Atores do banco de dados");

            }


        }


        [HttpGet("AtorFilmes")]
        public ActionResult<IEnumerable<Ator>> GetAtorFilmes()
        {
            try
            {
                return _context.Ators.Include(x => x.AtorFilmes).ToList();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Atores do banco de dados");

            }

        }

        /// <summary>
        /// Obtem um Ator pelo seu Id
        /// </summary>
        ///  Obtem um Ator pelo seu Id
        /// <returns>Objetos Ator</returns>
        [HttpGet("{id}")]
        public ActionResult<Ator> Get(int id)
        {
            try
            {
                var ator = _context.Ators.FirstOrDefault(p => p.AtorId == id);
                if (ator == null)
                {
                    return NotFound($"O Ator com id={id} não foi encontrado");
                }

                return ator;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter a  Ator do banco de dados");
            }





        }


        /// <summary>
        /// Inclui um novo Ator
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/Ator
        ///     {
        ///        "atorId": 1,
        ///        "nome": "Nome do Ator com 150 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="ator">objeto Ator</param>
        /// <returns>O objeto Ator incluido</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Ator ator)
        {
            try
            {
                _context.Ators.Add(ator);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterAtor",
                    new { id = ator.AtorId }, ator);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Erro ao tentar criar uma novo Ator");
            }



        }

        /// <summary>
        /// Atualiza um Ator
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     PUT api/Ator
        ///     {
        ///        "atorId": 1,
        ///        "nome": "Nome do Ator com 150 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="ator">objeto Ator</param>
        /// <returns>O objeto Ator atualizado</returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Ator ator)
        {
            try
            {
                if (id != ator.AtorId)
                {
                    return BadRequest($"Não foi possível atualizar os dados com id={id}");
                }

                _context.Entry(ator).State = EntityState.Modified;
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
        /// Exclui um Ator
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     PUT api/Ator
        ///     {
        ///        "atorId": 1
        ///       
        ///       
        ///     }
        /// </remarks>
        /// <param name="id">objeto Ator</param>
        /// <returns>O objeto Ator excluido</returns>
        [HttpDelete("{id}")]
        public ActionResult<Ator> Delete(int id)
        {
            try
            {
                var ator = _context.Ators.FirstOrDefault(p => p.AtorId == id);

                if (ator == null)
                {
                    return NotFound($"Os dados com id={id} não foi encontrado");
                }

                _context.Ators.Remove(ator);
                _context.SaveChanges();
                return ator;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao excluir os dados de id = {id}");
            }


        }
    }
}
