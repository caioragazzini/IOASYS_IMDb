using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IOASYS_IMDb.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {

            Voto = new Collection<Voto>();
        }

        [Key]
        public string UsuarioId { get; set; }

        public string Username { get; set; }


        public string Email { get; set; }
                 

        public ICollection<Voto> Voto { get; set; }
    }
}
