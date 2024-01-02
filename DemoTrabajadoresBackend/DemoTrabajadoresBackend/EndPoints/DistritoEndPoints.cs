using AutoMapper;
using DemoTrabajadoresBackend.DTOs;
using DemoTrabajadoresBackend.Entitdades;
using DemoTrabajadoresBackend.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace DemoTrabajadoresBackend.EndPoints
{
    public static class DistritoEndPoints
    {
        public static RouteGroupBuilder MapDistritos(this RouteGroupBuilder group)
        {
            group.MapGet("/", ListaDistritos)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("distrito-get"));

            group.MapGet("/{id:int}", ObtenerDistritoPorId);

            group.MapPost("/", CrearDistrito);

            group.MapPut("/{id:int}", ActualizarDistrito);

            group.MapDelete("/{id:int}", BorrarDistrito);

            group.MapGet("/provincia/{idProvincia:int}", ListaDistritosPorProvincia)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("distrito-get"));
            return group;
        }
        static async Task<Ok<List<DistritoDTO>>> ListaDistritos (IRepositoryDistrito repository, IMapper mappeer)
        {
            var distritos = await repository.ListaDistritos();
            var distritosDTO = mappeer.Map<List<DistritoDTO>>(distritos);
            return TypedResults.Ok(distritosDTO);
        }
        static async Task<Results<Ok<DistritoDTO>, NotFound>> ObtenerDistritoPorId(IRepositoryDistrito repository, int id, IMapper mapper)
        {
            var distritos = await repository.ObtenerDistritoPorId(id);
            if (distritos == null)
            {
                return TypedResults.NotFound();
            }
            var distritosDTO = mapper.Map<DistritoDTO>(distritos);
            return TypedResults.Ok(distritosDTO);
        }
        static async Task<Created<DistritoDTO>> CrearDistrito(CrearDistritoDTO crearDistritoDTO, IRepositoryDistrito repository,
        IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var distritos = mapper.Map<Distrito>(crearDistritoDTO);
            var id = await repository.CrearDistrito(distritos);
            await outputCacheStore.EvictByTagAsync("distrito-get", default);
            var distritosDTO = mapper.Map<DistritoDTO>(distritos);
            return TypedResults.Created($"/distritos/{id}", distritosDTO);
        }

        static async Task<Results<NoContent, NotFound>> ActualizarDistrito(int id, CrearDistritoDTO crearDistritoDTO, IRepositoryDistrito repository,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repository.ExistenciaDistrito(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var distritos = mapper.Map<Distrito>(crearDistritoDTO);
            distritos.IdDistrito = id;
            await repository.ActualizarDistrito(distritos);
            await outputCacheStore.EvictByTagAsync("distrito-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> BorrarDistrito(int id, IRepositoryDistrito repository, IOutputCacheStore outputCacheStore)
        {
            var exite = await repository.ExistenciaDistrito(id);
            if (!exite)
            {
                return TypedResults.NotFound();
            }

            await repository.Borrar(id);
            await outputCacheStore.EvictByTagAsync("distrito-get", default);
            return TypedResults.NoContent();

        }
        static async Task<Ok<List<DistritoDTO>>> ListaDistritosPorProvincia(IRepositoryDistrito repository, int idProvincia, IMapper mapper)
        {
            var distritos = await repository.ListaDistritosPorProvincia(idProvincia);
            var distritosDTO = mapper.Map<List<DistritoDTO>>(distritos);
            return TypedResults.Ok(distritosDTO);
        }
    }
}
