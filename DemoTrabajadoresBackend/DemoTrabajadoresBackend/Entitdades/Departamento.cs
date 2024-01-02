namespace DemoTrabajadoresBackend.Entitdades
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }

        public string? NombreDepartamento { get; set; }

        // Propiedad de navegación
        public virtual List<Provincia> Provincias { get; set; }
    }
}
