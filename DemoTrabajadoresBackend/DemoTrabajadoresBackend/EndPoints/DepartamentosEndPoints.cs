using AutoMapper;
using DemoTrabajadoresBackend.DTOs;
using DemoTrabajadoresBackend.Entitdades;
using DemoTrabajadoresBackend.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace DemoTrabajadoresBackend.EndPoints
{
    public static class DepartamentosEndPoints
    {
        public static RouteGroupBuilder MapDepartamentos(this RouteGroupBuilder group)
        {
            group.MapGet("/", ListaDepartamentos)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("departamentos-get"));

            group.MapGet("/{id:int}", ObtenerDepartamentoPorId);

            group.MapPost("/", CrearDepartamento);

            group.MapPut("/{id:int}", ActualizarDepartamento);

            group.MapDelete("/{id:int}", BorrarDepartamento);
            return group;
        }

        static async Task<Ok<List<DepartamentoDTO>>> ListaDepartamentos(IRepositoryDepartamento repository, IMapper mappeer)
        {
            var departamentos = await repository.ListaDepartamentos();
            var departamentosDTO= mappeer.Map<List<DepartamentoDTO>>(departamentos);
            return TypedResults.Ok(departamentosDTO);
        }

        static async Task<Results<Ok<DepartamentoDTO>, NotFound>> ObtenerDepartamentoPorId(IRepositoryDepartamento repository, int id, IMapper mapper)
        {
            var departamentos = await repository.ObtenerDepartamentoPorId(id);
            if(departamentos== null)
            {
                return TypedResults.NotFound();
            }
            var departamentosDTO= mapper.Map<DepartamentoDTO>(departamentos);
            return TypedResults.Ok(departamentosDTO );
        }

        static async Task<Created<DepartamentoDTO>> CrearDepartamento(CrearDepartamentoDTO crearDepartamentoDTO, IRepositoryDepartamento repository,
        IOutputCacheStore outputCacheStore, IMapper mapper)  
        {
            var departamentos = mapper.Map<Departamento>(crearDepartamentoDTO); 
            var id = await repository.CrearDepartamento(departamentos);
            await outputCacheStore.EvictByTagAsync("departamentos-get", default);
            var departamentosDTO = mapper.Map<DepartamentoDTO>(departamentos);
            return TypedResults.Created($"/departamentos/{id}", departamentosDTO);
        }



        static async Task<Results<NoContent,NotFound>> ActualizarDepartamento(int id, CrearDepartamentoDTO crearDepartamentoDTO, IRepositoryDepartamento repository,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repository.ExistenciaDepartamento(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var departamentos = mapper.Map<Departamento>(crearDepartamentoDTO);
            departamentos.IdDepartamento = id;
            await repository.ActualizarDepartamento(departamentos);
            await outputCacheStore.EvictByTagAsync("departamentos-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> BorrarDepartamento(int id, IRepositoryDepartamento repository, IOutputCacheStore outputCacheStore)
        {
            var exite = await repository.ExistenciaDepartamento(id);
            if (!exite)
            {
                return TypedResults.NotFound();
            }

            await repository.Borrar(id);
            await outputCacheStore.EvictByTagAsync("departamentos-get", default);
            return TypedResults.NoContent();

        }
    }
}
