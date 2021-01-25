using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IOASYS_IMDb.Models
{
    [Table("Voto")]
    public class Voto
    {
        [Key]
        public int VotacaoId { get; set; }
             

        [Range(typeof(int), "0", "4")]
        public int Nota { get; set; }

        public Filme Filme { get; set; }
        public int FilmeId { get; set; }
        public Usuario Usuario { get; set; }
        public string UsuarioId { get; set; }

    }
}
