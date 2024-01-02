namespace DemoTrabajadoresBackend.DTOs
{
    public class TrabajadorDetalleDTO
    {
        public int IdTrabajador { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NroDocumento { get; set; }
        public string? Nombres { get; set; }
        public string? Sexo { get; set; }
        public string? NombreDistrito { get; set; }
        public string? NombreProvincia { get; set; }
        public string? NombreDepartamento { get; set; }
        public int? IdDistrito { get; set; }
    }
}
