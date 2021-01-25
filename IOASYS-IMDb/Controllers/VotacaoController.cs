using Dapper;
using IOASYS_IMDb.Authentication;
using IOASYS_IMDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOASYS_IMDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotacaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _config;
        public VotacaoController(ApplicationDbContext context, IConfiguration configuration)
        {
            _config = configuration;
            _context = context;
        }

        [HttpPost]
        public ActionResult Post([FromBody]  string idUsuario,int nota, int idFilme)
        {

            try
            {
                Voto votacao = new Voto()
                {
                    FilmeId = idFilme,
                    UsuarioId = idUsuario,
                    Nota = nota

                };

                _context.Voto.Add(votacao);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterVotacao",
                    new { id = votacao.VotacaoId }, votacao);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Erro ao tentar criar uma nova Votação");
            }


        }

        [HttpGet("NotaFimes")]
        public ActionResult<IEnumerable<Filme>> GetFilmesAtorNome(string nome)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_config.GetConnectionString("ConnStr")))
                {
                    conexao.Open();

                    var teste = conexao.Query<dynamic>("SELECT avg(f.Nota),f.Titulo,f.Duracao,f.Descricao,f.AnoLancamento FROM filme f inner join votacao v on f.FilmeId=v.FilmeId where (f.Titulo Like \'%" + (nome + "%\')"));

                    return Ok(teste);

                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar obter os Atores e Filmes do banco de dados");

            }


        }

    }
}
