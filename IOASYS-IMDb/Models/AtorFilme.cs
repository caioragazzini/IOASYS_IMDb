using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IOASYS_IMDb.Models
{
    [Table("AtorFilme")]
    public class AtorFilme
    {

        [Key]
        public int AtorFilmeId { get; set; }
               
        public Ator Ator { get; set; }
        public int AtorId { get; set; }
       
        public Filme Filme { get; set; }
        public int FilmeId { get; set; }
        
       
       
    }
}
