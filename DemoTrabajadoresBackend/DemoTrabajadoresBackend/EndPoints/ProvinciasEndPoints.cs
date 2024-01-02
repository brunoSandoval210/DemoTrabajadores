using AutoMapper;
using DemoTrabajadoresBackend.DTOs;
using DemoTrabajadoresBackend.Entitdades;
using DemoTrabajadoresBackend.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace DemoTrabajadoresBackend.EndPoints
{
    public static class ProvinciasEndPoints
    {
        public static RouteGroupBuilder MapProvincias(this RouteGroupBuilder group)
        {
            group.MapGet("/", ListaProvincias)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("provincias-get"));

            group.MapGet("/{id:int}", ObtenerProvinciasPorId);

            group.MapPost("/", CrearProvincia);

            group.MapPut("/{id:int}", ActualizarProvincia);

            group.MapDelete("/{id:int}", BorrarProvincia);

            group.MapGet("/departamento/{idDepartamento:int}", ObtenerProvinciasPorDepartamento);

            return group;
        }

        static async Task<Ok<List<ProvinciaDTO>>> ListaProvincias(IRepositoryProvincia repository, IMapper mappeer)
        {
            var provincia = await repository.ListaProvincia();
            var provinciaDTO = mappeer.Map<List<ProvinciaDTO>>(provincia);
            return TypedResults.Ok(provinciaDTO);
        }

        static async Task<Results<Ok<ProvinciaDTO>, NotFound>> ObtenerProvinciasPorId(IRepositoryProvincia repository, int id, IMapper mapper)
        {
            var provincia = await repository.ObtenerProvinciaPorId(id);
            if (provincia == null)
            {
                return TypedResults.NotFound();
            }
            var provinciaDTO = mapper.Map<ProvinciaDTO>(provincia);
            return TypedResults.Ok(provinciaDTO);
        }

        static async Task<Created<ProvinciaDTO>> CrearProvincia(CrearProvinciaDTO crearProvinciaDTO, IRepositoryProvincia repository,
        IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var provincia = mapper.Map<Provincia>(crearProvinciaDTO);
            var id = await repository.CrearProvincia(provincia);
            await outputCacheStore.EvictByTagAsync("provincia-get", default);
            var provinciaDTO = mapper.Map<ProvinciaDTO>(provincia);
            return TypedResults.Created($"/provincias/{id}", provinciaDTO);
        }

        static async Task<Results<NoContent, NotFound>> ActualizarProvincia(int id, CrearProvinciaDTO crearProvinciaDTO, IRepositoryProvincia repository,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repository.ExistenciaProvincia(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var provincia = mapper.Map<Provincia>(crearProvinciaDTO);
            provincia.IdProvincia = id;
            await repository.ActualizarProvincia(provincia);
            await outputCacheStore.EvictByTagAsync("provincia-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> BorrarProvincia(int id, IRepositoryProvincia repository, IOutputCacheStore outputCacheStore)
        {
            var exite = await repository.ExistenciaProvincia(id);
            if (!exite)
            {
                return TypedResults.NotFound();
            }

            await repository.Borrar(id);
            await outputCacheStore.EvictByTagAsync("provincia-get", default);
            return TypedResults.NoContent();

        }

        static async Task<Ok<List<ProvinciaDTO>>> ObtenerProvinciasPorDepartamento(IRepositoryProvincia repository, int idDepartamento, IMapper mapper)
        {
            var provincias = await repository.ObtenerProvinciasPorDepartamento(idDepartamento);
            var provinciasDTO = mapper.Map<List<ProvinciaDTO>>(provincias);
            return TypedResults.Ok(provinciasDTO);
        }
    }
}
