using DemoTrabajadoresBackend.Entitdades;
using Microsoft.EntityFrameworkCore;

namespace DemoTrabajadoresBackend.Repositorios
{
    public class RepositoryDepartamento : IRepositoryDepartamento
    {
        private readonly ApplicationDbContext context;

        public RepositoryDepartamento(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task ActualizarDepartamento(Departamento departamento)
        {
            context.Update<Departamento>(departamento);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Departamentos.Where(x => x.IdDepartamento==id).ExecuteDeleteAsync(); 
        }

        public async Task<int> CrearDepartamento(Departamento departamento)
        {
            context.Add(departamento);
            await context.SaveChangesAsync();

            return departamento.IdDepartamento;
        }

        public async Task<bool> ExistenciaDepartamento(int id)
        {
            return await context.Departamentos.AnyAsync(x => x.IdDepartamento==id);
        }

        public async Task<List<Departamento>> ListaDepartamentos()
        {
            return await context.Departamentos.OrderBy(x=> x.NombreDepartamento).ToListAsync();
        }

        public async Task<Departamento?> ObtenerDepartamentoPorId(int id)
        {
            return await context.Departamentos.FirstOrDefaultAsync(x => x.IdDepartamento == id);
        }
    }
}
