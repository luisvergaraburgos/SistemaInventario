using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre es requerido")]
        [MaxLength(60, ErrorMessage ="Nombre debe contener máximo 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Descripción es requerida")]
        [MaxLength(100, ErrorMessage = "Descripción debe contener máximo 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage ="Estado es requerido")]
        public bool Estado { get; set; }
    }
}
