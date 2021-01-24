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
    public class CategoriaController : ControllerBase
    {


        private readonly ApplicationDbContext _context;
        public CategoriaController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }

        /// <summary>
        /// Obtem todos as Categorias
        /// </summary>
        /// <returns>Objetos Categoria</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Categorias do banco de dados");

            }


        }


        [HttpGet("Filmes")]
        public ActionResult<IEnumerable<Categoria>> CategoriaFilmes()
        {
            try
            {
                return _context.Categorias.Include(x => x.Filmes).ToList();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Categorias Filmes do banco de dados");
            }





        }

        /// <summary>
        /// Obtem uma Categoria pelo seu Id
        /// </summary>
        ///  Obtem uma Categoria pelo seu Id
        /// <returns>Objetos Categoria</returns>
        [HttpGet("{id}")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {

                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"A Categoria com id={id} não foi encontrada");
                }
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter as Categorias do banco de dados");

            }
        }

        /// <summary>
        /// Inclui uma nova Categoria
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/Categoria
        ///     {
        ///        "categoriaId": 1,
        ///        "nome": "Nome da categoria com 80 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="categoria">objeto Ator</param>
        /// <returns>O objeto Ator incluido</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {

            try
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Erro ao tentar criar uma nova Categoria");
            }

        }

        /// <summary>
        /// Atualiza uma Categoria
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     PUT api/Categoria
        ///     {
        ///        "categoriaId": 1,
        ///        "nome": "Nome da categoria com 80 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="categoria">objeto Ator</param>
        /// <returns>O objeto Ator Atualizado</returns>

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest($"Não foi possível atualizar os dados com id={id}");
                }

                _context.Entry(categoria).State = EntityState.Modified;
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
        /// Exclui uma nova Categoria
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     DELETE api/Categoria
        ///     {
        ///        "categoriaId": 1
        ///       
        ///       
        ///     }
        /// </remarks>
        /// <param name="id">objeto Ator</param>
        /// <returns>O objeto Categoria excluido</returns>
        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {

            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"Os dados com id={id} não foi encontrado");
                }

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao excluir os dados de id = {id}");
            }

        }
    }



}
