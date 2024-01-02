using DemoTrabajadoresBackend.Entitdades;

namespace DemoTrabajadoresBackend.Repositorios
{
    public interface IRepositoryDepartamento
    {
        Task<List<Departamento>> ListaDepartamentos();
        Task<Departamento?> ObtenerDepartamentoPorId(int id);
        Task<int> CrearDepartamento(Departamento departamento);
        Task<bool> ExistenciaDepartamento(int id);
        Task ActualizarDepartamento(Departamento departamento);
        Task Borrar(int id);
    }
}
