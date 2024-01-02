using DemoTrabajadoresBackend.Entitdades;
using Microsoft.EntityFrameworkCore;

namespace DemoTrabajadoresBackend.Repositorios
{
    public class RepositoryProvincia :IRepositoryProvincia
    {
        private readonly ApplicationDbContext context;

        public RepositoryProvincia(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task ActualizarProvincia(Provincia provincia)
        {
            context.Update<Provincia>(provincia);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Provincia.Where(x => x.IdProvincia == id).ExecuteDeleteAsync();
        }

        public async Task<int> CrearProvincia(Provincia provincia)
        {
            context.Add(provincia);
            await context.SaveChangesAsync();

            return provincia.IdProvincia;
        }

        public async Task<bool> ExistenciaProvincia(int id)
        {
            return await context.Provincia.AnyAsync(x => x.IdProvincia == id);
        }

        public async Task<List<Provincia>> ListaProvincia()
        {
            return await context.Provincia.OrderBy(x => x.NombreProvincia).ToListAsync();
        }

        public async Task<Provincia?> ObtenerProvinciaPorId(int id)
        {
            return await context.Provincia.FirstOrDefaultAsync(x => x.IdProvincia == id);
        }

        public async Task<List<Provincia>> ObtenerProvinciasPorDepartamento(int idDepartamento)
        {
            return await context.Provincia.Where(p => p.IdDepartamento == idDepartamento).OrderBy(p => p.NombreProvincia)
            .ToListAsync();
        }
    }
}
