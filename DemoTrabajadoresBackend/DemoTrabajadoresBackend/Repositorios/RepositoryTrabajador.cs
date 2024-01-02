using DemoTrabajadoresBackend.Entitdades;
using Microsoft.EntityFrameworkCore;

namespace DemoTrabajadoresBackend.Repositorios
{
    public class RepositoryTrabajador : IRepositoryTrabajador
    {
        private readonly ApplicationDbContext context;

        public RepositoryTrabajador(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task ActualizarTrabajador(Trabajador trabajador)
        {
            context.Update<Trabajador>(trabajador);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Trabajadors.Where(x => x.IdTrabajador == id).ExecuteDeleteAsync();
        }

        public async Task<int> CrearTrabajador(Trabajador trabajador)
        {
            context.Add(trabajador);
            await context.SaveChangesAsync();

            return trabajador.IdTrabajador;
        }

        public async Task<bool> ExistenciaTrabajador(int id)
        {
            return await context.Trabajadors.AnyAsync(x => x.IdTrabajador == id);
        }

        public async Task<List<Trabajador>> ListaTrabajadores()
        {
            return await context.Trabajadors.OrderBy(x => x.Nombres).ToListAsync();
        }

        public async Task<Trabajador?> ObtenerTrabajadoresPorId(int id)
        {
            return await context.Trabajadors.FirstOrDefaultAsync(x => x.IdTrabajador == id);
        }

        public async Task<List<Trabajador>> ListaTrabajadoresConDetalleDesdeProcedure()
        {
            var trabajadores = await context.Trabajadors.FromSqlRaw("ObtenerTrabajadoresConDetalle").ToListAsync();

            return trabajadores;
        }

        public async Task<List<Trabajador>> ListaTrabajadoresPorSexo(string sexo)
        {
            var trabajadores = await context.Trabajadors.FromSqlRaw($"EXEC ObtenerTrabajadoresPorSexo {sexo}").ToListAsync();

            return trabajadores;
        }

    }
}
