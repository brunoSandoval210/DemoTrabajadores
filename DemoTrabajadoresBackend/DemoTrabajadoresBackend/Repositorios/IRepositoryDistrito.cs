using DemoTrabajadoresBackend.Entitdades;

namespace DemoTrabajadoresBackend.Repositorios
{
    public interface IRepositoryDistrito
    {
        Task<List<Distrito>> ListaDistritos();
        Task<Distrito?> ObtenerDistritoPorId(int id);
        Task<int> CrearDistrito(Distrito distrito);
        Task<bool> ExistenciaDistrito(int id);
        Task ActualizarDistrito(Distrito distrito);
        Task Borrar(int id);
        Task<List<Distrito>> ListaDistritosPorProvincia(int idProvincia);
    }
}
