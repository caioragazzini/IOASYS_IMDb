using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IOASYS_IMDb.Models
{
    [Table("Filme")]
    public class Filme
    {
        public Filme()
        {
            AtorFilmes = new Collection<AtorFilme>();
        }

        [Key]
        public int FilmeId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Titulo { get; set; }

        [Required]
        public int Duracao { get; set; }

        [Required]
        [MaxLength(300)]
        public string Descricao { get; set; }

        [Required]
        public int AnoLancamento { get; set; }
        
        public int Nota { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        public Diretor Diretor { get; set; }
        public int DiretorId { get; set; }

        public ICollection<AtorFilme> AtorFilmes { get; set; }


    }
}
