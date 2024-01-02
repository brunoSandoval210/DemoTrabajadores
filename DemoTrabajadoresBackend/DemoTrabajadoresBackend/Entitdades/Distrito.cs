namespace DemoTrabajadoresBackend.Entitdades
{
    public class Distrito
    {
        public int IdDistrito { get; set; }

        public int IdProvincia { get; set; }

        public string? NombreDistrito { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual List<Trabajador> Trabajadores { get; set; }
    }
}
