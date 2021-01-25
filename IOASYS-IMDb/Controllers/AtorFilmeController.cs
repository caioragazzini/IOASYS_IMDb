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
    public class AtorFilmeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AtorFilmeController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }



        /// <summary>
        /// Obtem todos os AtorFilme
        /// </summary>
        /// <returns>Objetos AtorFilme</returns>
        [HttpGet]
        public ActionResult<IEnumerable<AtorFilme>> Get()
        {
            try
            {
                return _context.AtorFilmes.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Atores do banco de dados");

            }

        }

        /// <summary>
        /// Obtem um AtorFilme pelo seu Id
        /// </summary>
        ///  Obtem um AtorFilme pelo seu Id
        /// <returns>Objetos AtorFilme</returns>
        [HttpGet("{id}")]
        public ActionResult<AtorFilme> Get(int id)
        {
            try
            {
                var ator = _context.AtorFilmes.FirstOrDefault(p => p.AtorFilmeId == id);

                if (ator == null)
                {
                    return NotFound($"Os Atores Filmes com id={id} não foram encontrados");
                }
                return ator;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os  Atores Filmes do banco de dados");

            }
        }

        /// <summary>
        /// Inclui uma novo AtorFilme
        /// </summary>
        /// <remarks>
        /// <param name="atorFilme">objeto AtorFilme</param>
        /// <returns>O objeto AtorFilme incluido</returns>
        /// <remarks>O objeto AtorFilme incluido</remarks>
        [HttpPost]
        public ActionResult Post([FromBody] AtorFilme atorFilme)
        {
            try
            {

                _context.AtorFilmes.Add(atorFilme);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterAtorFilme",
                    new { id = atorFilme.AtorFilmeId }, atorFilme);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Erro ao tentar criar uma novo Ator Filme");
            }


        }

        /// <summary>
        /// Atualiza um novo AtorFilme
        /// </summary>
        /// <remarks>
        /// <param name="atorFilme">objeto AtorFilme</param>
        /// <returns>O objeto AtorFilme incluido</returns>
        /// <remarks>O objeto AtorFilme incluido</remarks>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AtorFilme atorFilme)
        {
            try
            {
                if (id != atorFilme.AtorFilmeId)
                {
                    return BadRequest($"Não foi possível atualizar os dados com id={id}");
                }

                _context.Entry(atorFilme).State = EntityState.Modified;
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
        /// Exclui um AtorFilme
        /// </summary>
        /// <remarks>
        /// <param name="atorFilme">objeto AtorFilme</param>
        /// <returns>O objeto AtorFilme incluido</returns>
        /// <remarks>O objeto AtorFilme incluido</remarks>
        [HttpDelete("{id}")]
        public ActionResult<AtorFilme> Delete(int id)
        {
            try
            {
                var atorFilme = _context.AtorFilmes.FirstOrDefault(p => p.AtorFilmeId == id);

                if (atorFilme == null)
                {
                    return NotFound($"Os dados com id={id} não foi encontrado");
                }

                _context.AtorFilmes.Remove(atorFilme);
                _context.SaveChanges();
                return atorFilme;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                              $"Erro ao excluir os dados de id = {id}");
            }

        }

    }
}
