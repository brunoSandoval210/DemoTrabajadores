using DemoTrabajadoresBackend.Entitdades;

namespace DemoTrabajadoresBackend.DTOs
{
    public class CrearTrabajadorDTO
    {
        public string? TipoDocumento { get; set; }

        public string? NroDocumento { get; set; }

        public string? Nombres { get; set; }

        public string? Sexo { get; set; }

        public int? IdDistrito { get; set; }
    }
}
