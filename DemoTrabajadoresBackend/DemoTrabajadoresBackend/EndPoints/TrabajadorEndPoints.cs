using AutoMapper;
using DemoTrabajadoresBackend.DTOs;
using DemoTrabajadoresBackend.Entitdades;
using DemoTrabajadoresBackend.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace DemoTrabajadoresBackend.EndPoints
{
    public static class TrabajadorEndPoints
    {
        public static RouteGroupBuilder MapTrabajadores(this RouteGroupBuilder group)
        {
            group.MapGet("/", ListaTrabajadores)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("trabajador-get"));

            group.MapGet("/{id:int}", ObtenerTrabajadoresPorId);

            group.MapPost("/", CrearTrabajador);

            group.MapPut("/{id:int}", ActualizarTrabajador);

            group.MapDelete("/{id:int}", BorrarTrabajador);

            group.MapGet("/detalle", ListaTrabajadoresConDetalleDesdeProcedure)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("trabajador-get"));

            group.MapGet("/sexo/{sexo}", ListaTrabajadoresPorSexo)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("trabajador-get"));

            return group;
        }
        static async Task<Ok<List<TrabajadorDTO>>> ListaTrabajadores(IRepositoryTrabajador repository, IMapper mappeer)
        {
            var trabajadores = await repository.ListaTrabajadores();
            var trabajadoresDTO = mappeer.Map<List<TrabajadorDTO>>(trabajadores);
            return TypedResults.Ok(trabajadoresDTO);
        }

        static async Task<Results<Ok<TrabajadorDTO>, NotFound>> ObtenerTrabajadoresPorId(IRepositoryTrabajador repository, int id, IMapper mapper)
        {
            var trabajadores = await repository.ObtenerTrabajadoresPorId(id);
            if (trabajadores == null)
            {
                return TypedResults.NotFound();
            }
            var trabajadoresDTO = mapper.Map<TrabajadorDTO>(trabajadores);
            return TypedResults.Ok(trabajadoresDTO);
        }

        static async Task<Created<TrabajadorDTO>> CrearTrabajador(CrearTrabajadorDTO crearTrabajadorDTO, IRepositoryTrabajador repository,
        IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var trabajadores = mapper.Map<Trabajador>(crearTrabajadorDTO);
            var id = await repository.CrearTrabajador(trabajadores);
            await outputCacheStore.EvictByTagAsync("trabajador-get", default);
            var trabajadoresDTO = mapper.Map<TrabajadorDTO>(trabajadores);
            return TypedResults.Created($"/trabajadores/{id}", trabajadoresDTO);
        }

        static async Task<Results<NoContent, NotFound>> ActualizarTrabajador(int id, CrearTrabajadorDTO crearTrabajadorDTO, IRepositoryTrabajador repository,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repository.ExistenciaTrabajador(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var trabajadores = mapper.Map<Trabajador>(crearTrabajadorDTO);
            trabajadores.IdTrabajador = id;
            await repository.ActualizarTrabajador(trabajadores);
            await outputCacheStore.EvictByTagAsync("trabajador-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> BorrarTrabajador(int id, IRepositoryTrabajador repository, IOutputCacheStore outputCacheStore)
        {
            var exite = await repository.ExistenciaTrabajador(id);
            if (!exite)
            {
                return TypedResults.NotFound();
            }

            await repository.Borrar(id);
            await outputCacheStore.EvictByTagAsync("trabajador-get", default);
            return TypedResults.NoContent();

        }

        static async Task<Ok<List<TrabajadorDetalleDTO>>> ListaTrabajadoresConDetalleDesdeProcedure(IRepositoryTrabajador repository, IMapper mappeer)
        {
            var trabajadores = await repository.ListaTrabajadoresConDetalleDesdeProcedure();
            var trabajadoresDTO = mappeer.Map<List<TrabajadorDetalleDTO>>(trabajadores);
            return TypedResults.Ok(trabajadoresDTO);
        }

        public static async Task<Ok<List<TrabajadorDetalleDTO>>> ListaTrabajadoresPorSexo(IRepositoryTrabajador repository, IMapper mappeer, string sexo)
        {
            var trabajadores = await repository.ListaTrabajadoresPorSexo(sexo);
            var trabajadoresDTO = mappeer.Map<List<TrabajadorDetalleDTO>>(trabajadores);
            return TypedResults.Ok(trabajadoresDTO);
        }
    }
}
