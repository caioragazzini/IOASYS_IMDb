using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IOASYS_IMDb.Models
{
    [Table("Ator")]
    public class Ator
    {
        
        public Ator()
        {
            AtorFilmes = new Collection<AtorFilme>();

        }
        [Key]
        public int AtorId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }
               

        public ICollection<AtorFilme> AtorFilmes { get; set; }
    }
}
