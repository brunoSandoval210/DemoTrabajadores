using DemoTrabajadoresBackend.Entitdades;

namespace DemoTrabajadoresBackend.Repositorios
{
    public interface IRepositoryProvincia
    {
        Task<List<Provincia>> ListaProvincia();
        Task<Provincia?> ObtenerProvinciaPorId(int id);
        Task<int> CrearProvincia(Provincia provincia);
        Task<bool> ExistenciaProvincia(int id);
        Task ActualizarProvincia(Provincia provincia);
        Task Borrar(int id);
        Task<List<Provincia>> ObtenerProvinciasPorDepartamento(int idDepartamento);
    }
}
