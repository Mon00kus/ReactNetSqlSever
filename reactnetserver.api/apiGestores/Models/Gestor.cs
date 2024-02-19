using System.ComponentModel.DataAnnotations;

namespace apiGestores.Models
{
    public class Gestor
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Lanzamiento { get; set; }
        public string Desarrollador { get; set; }
    }
}
