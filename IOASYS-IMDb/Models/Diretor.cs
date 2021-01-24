using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOASYS_IMDb.Models
{
    [Table("Diretor")]
    public class Diretor
    {
       
        public Diretor()
        {
            Filmes=new Collection<Filme>();
        }

        [Key]
        public int DiretorId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }
        
        public ICollection<Filme> Filmes { get; set; }
    }
}
