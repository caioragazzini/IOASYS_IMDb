using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IOASYS_IMDb.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        public Categoria()
        {
            Filmes = new Collection<Filme>();
        }

        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }


        public ICollection<Filme> Filmes { get; set; }
    }
}
