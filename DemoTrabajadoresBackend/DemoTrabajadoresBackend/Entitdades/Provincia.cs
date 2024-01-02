namespace DemoTrabajadoresBackend.Entitdades
{
    public class Provincia
    {
        public int IdProvincia { get; set; }

        public int IdDepartamento { get; set; }

        public string? NombreProvincia { get; set; }

        public virtual Departamento Departamento { get; set; }
        public virtual List<Distrito> Distritos { get; set; }
    }
}
