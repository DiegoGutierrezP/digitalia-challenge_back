using System.ComponentModel.DataAnnotations;

namespace BackendDigitaliaChallenge.Models
{
    public class Recibo
    {
        [Key]
        public int id { get; set; }
        public int usuarioId { get; set; }
        public Usuario usuario { get; set; }
        public string moneda { get; set; }
        public decimal monto { get; set; }
        public string titulo { get; set; }
        public string? descripcion { get; set; }
        public string? direccion { get; set; }
        public string nombres { get; set; }
        public string?  apellidos { get; set; }
        public string tipoDoc { get; set; }
        public string numDoc { get; set; }


    }
}
