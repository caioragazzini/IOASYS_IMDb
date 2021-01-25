using IOASYS_IMDb.Authentication;
using IOASYS_IMDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Data;
using System.Configuration;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Microsoft.AspNetCore.Authorization;

namespace WebApiFilmesIMDb.Controllers
{
    
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _config;
      
        public FilmesController(ApplicationDbContext context, IConfiguration configuration)
        {
            _config = configuration;
            _context = context;
        }


        /// <summary>
        /// Obtem todos os Filmes
        /// </summary>
        /// <returns>ObjetosFilme</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Filme>> Get()
        {
            try
            {
                return _context.Filmes.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Filmes do banco de dados");

            }


        }
                
        /// <summary>
        ///Obtem um Filme pelo nome do Ator
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     Get /api/Filmes/FilmesNomeAtor
        ///     {
        ///       
        ///        "nome": "Nome do Ator com 150 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="nome">objeto Filme</param>
        /// <returns>O objeto Filme</returns>
        [HttpGet("FilmesNomeAtor")]
        public ActionResult<IEnumerable<Filme>> GetFilmesAtorNome(string nome)
        {
            try
            {
                

                using (MySqlConnection conexao = new MySqlConnection( _config.GetConnectionString("ConnStr")))
                {
                    conexao.Open();

                   var teste= conexao.Query<dynamic>("SELECT f.Titulo,a.Nome FROM filme f inner join atorfilme u on f.FilmeId = u.FilmeId inner join ator a on a.AtorId= u.AtorId Where (a.Nome Like \'%" + (nome + "%\')"));

                    return Ok(teste);

                }
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Atores e Filmes do banco de dados");

            }


        }


        /// <summary>
        ///Obtem um Filme pelo nome do Diretor
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     Get /api/Filmes/FilmesDiretor
        ///     {
        ///       
        ///        "nome": "Nome do Diretor com 150 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="nome">objeto Filme</param>
        /// <returns>O objeto Filme</returns>
        [HttpGet("FilmesDiretor")]
        public ActionResult<IEnumerable<Filme>> GetFilmesDiretor(string nome)
        {
            try
            {


                using (MySqlConnection conexao = new MySqlConnection(_config.GetConnectionString("ConnStr")))
                {
                    conexao.Open();

                    var teste = conexao.Query<dynamic>("SELECT f.Titulo FROM filme f inner join diretor d on f.DiretorId = d.DiretorId Where (d.Nome Like \'%" + (nome + "%\')"));

                    return Ok(teste);

                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Atores e Filmes do banco de dados");

            }


        }


        /// <summary>
        ///Obtem um Filme pelo nome da categira 
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     Get /api/Filmes/FilmesGenero
        ///     {
        ///       
        ///        "nome": "Nome da Categoria com 150 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="nome">objeto Filme</param>
        /// <returns>O objeto Filme</returns>
        [HttpGet("FilmesGenero")]
        public ActionResult<IEnumerable<Filme>> GetFilmesGenero(string nome)
        {
            try
            {


                using (MySqlConnection conexao = new MySqlConnection(_config.GetConnectionString("ConnStr")))
                {
                    conexao.Open();

                    var teste = conexao.Query<dynamic>("SELECT f.Titulo FROM filme f inner join categoria c on f.CategoriaId = c.CategoriaId Where (c.Nome Like \'%" + (nome + "%\')"));

                    return Ok(teste);

                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Atores e Filmes do banco de dados");

            }


        }

        /// <summary>
        ///Obtem um Filme pelo Titulo e retorna a media dos votos recebidos e demais informações
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     Get /api/Filmes/FilmesMedia
        ///     {
        ///       
        ///        "nome": "Nome do Titulo com 150 caracteres no maximo"
        ///       
        ///     }
        /// </remarks>
        /// <param name="nome">objeto Filme</param>
        /// <returns>O objeto Filme</returns>
        [HttpGet("FilmesMedia")]
        public ActionResult<IEnumerable<Filme>> GetFilmesMedia(string nome)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_config.GetConnectionString("ConnStr")))
                {
                    conexao.Open();

                    var teste = conexao.Query<dynamic>("SELECT avg(f.Nota),f.Titulo,f.Duracao,f.Descricao,f.AnoLancamento FROM filme f Where (f.Titulo Like \'%" + (nome + "%\')"));

                    return Ok(teste);

                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Atores e Filmes do banco de dados");

            }


        }

        /// <summary>
        /// Obtem um Filme pelo seu Id
        /// </summary>
        ///  Obtem um Filme pelo seu Id
        /// <returns>Objetos Filmes</returns>
        [HttpGet("{id}")]
        public ActionResult<Filme> Get(int id)
        {
            try
            {
                var filme = _context.Filmes.FirstOrDefault(p => p.FilmeId == id);
                if (filme == null)
                {
                    return NotFound($"O Filme com id={id} não foi encontrado");
                }
                return filme;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Filmes do banco de dados");

            }
        }




        //POST/ PUT/ DELETE

        /// <summary>
        /// Inclui um novo Filme Necessario ser usuario Admin
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/AtorFilme
        ///     {
        ///        "FilmeId": 1,
        ///        "titulo": "titulo do filme com 80 caracteres no maximo",
        ///        "duracao": 120,
        ///        "descricao": "Descrição do Filme com 300 caracteres no maximo",
        ///        "anoLancamento": 04/03/1978,
        ///        "nota": de 0 a 4,
        ///        "categoriaId": id vinculado a tabela de Categorias,
        ///        "diretorId": id vinculado a tabela de Diretor
        ///       
        ///     }
        /// </remarks>
        /// <param name="filme">objeto Filme</param>
        /// <returns>O objeto Filme incluido</returns>
        /// 
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public ActionResult Post([FromBody] Filme filme)
        {

            try
            {
                _context.Filmes.Add(filme);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterFilme",
                    new { id = filme.FilmeId }, filme);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Erro ao tentar criar uma novo Filme");
            }


        }

        /// <summary>
        /// Atualiza um Filme
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     PUT api/Filme/id
        ///     {
        ///        "FilmeId": 1,
        ///        "titulo": "titulo do filme com 80 caracteres no maximo",
        ///        "duracao": 120,
        ///        "descricao": "Descrição do Filme com 300 caracteres no maximo",
        ///        "anoLancamento": 04/03/1978,
        ///        "nota": de 0 a 4,
        ///        "categoriaId": id vinculado a tabela de Categorias,
        ///        "diretorId": id vinculado a tabela de Diretor
        ///       
        ///     }
        /// </remarks>
        /// <param name="filme">objeto Filme</param>
        /// <returns>O objeto Filme atualizado</returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Filme filme)
        {
            try
            {
                if (id != filme.FilmeId)
                {
                    return BadRequest($"Não foi possível atualizar os dados com id={id}");
                }

                _context.Entry(filme).State = EntityState.Modified;
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
        /// Exclui um Filme
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/AtorFilme
        ///     {
        ///        "FilmeId": 1
        ///       
        ///       
        ///     }
        /// </remarks>
        /// <param name="id">objeto Filme</param>
        /// <returns>O objeto Filme exluido</returns>
        [HttpDelete("{id}")]
        public ActionResult<Filme> Delete(int id)
        {
            try
            {
                var filme = _context.Filmes.FirstOrDefault(p => p.FilmeId == id);

                if (filme == null)
                {
                    return NotFound($"Os dados com id={id} não foi encontrado");
                }

                _context.Filmes.Remove(filme);
                _context.SaveChanges();
                return filme;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                              $"Erro ao excluir os dados de id = {id}");
            }

        }















    }



}

