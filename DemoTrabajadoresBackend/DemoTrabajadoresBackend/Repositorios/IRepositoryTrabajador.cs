using DemoTrabajadoresBackend.Entitdades;

namespace DemoTrabajadoresBackend.Repositorios
{
    public interface IRepositoryTrabajador
    {
        Task<List<Trabajador>> ListaTrabajadores();
        Task<Trabajador?> ObtenerTrabajadoresPorId(int id);
        Task<int> CrearTrabajador(Trabajador trabajador);
        Task<bool> ExistenciaTrabajador(int id);
        Task ActualizarTrabajador(Trabajador trabajador);
        Task Borrar(int id);
        Task<List<Trabajador>> ListaTrabajadoresConDetalleDesdeProcedure();
        Task<List<Trabajador>> ListaTrabajadoresPorSexo(string sexo);
    }
}
