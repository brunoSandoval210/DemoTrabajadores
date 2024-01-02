using DemoTrabajadoresBackend.Entitdades;
using Microsoft.EntityFrameworkCore;

namespace DemoTrabajadoresBackend.Repositorios
{
    public class RepositoryDistrito : IRepositoryDistrito
    {
        private readonly ApplicationDbContext context;

        public RepositoryDistrito(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task ActualizarDistrito(Distrito distrito)
        {
            context.Update<Distrito>(distrito);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Distritos.Where(x => x.IdDistrito == id).ExecuteDeleteAsync();
        }

        public async Task<int> CrearDistrito(Distrito distrito)
        {
            context.Add(distrito);
            await context.SaveChangesAsync();

            return distrito.IdDistrito;
        }

        public async Task<bool> ExistenciaDistrito(int id)
        {
            return await context.Distritos.AnyAsync(x => x.IdDistrito == id);
        }

        public async Task<List<Distrito>> ListaDistritos()
        {
            return await context.Distritos.OrderBy(x => x.NombreDistrito).ToListAsync();
        }

        public async Task<Distrito?> ObtenerDistritoPorId(int id)
        {
            return await context.Distritos.FirstOrDefaultAsync(x => x.IdDistrito == id);
        }

        public async Task<List<Distrito>> ListaDistritosPorProvincia(int idProvincia)
        {
            return await context.Distritos.Where(x => x.IdProvincia == idProvincia).OrderBy(x => x.NombreDistrito).ToListAsync();
        }
    }
}
