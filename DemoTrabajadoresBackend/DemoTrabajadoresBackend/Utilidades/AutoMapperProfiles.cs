using AutoMapper;
using DemoTrabajadoresBackend.DTOs;
using DemoTrabajadoresBackend.Entitdades;

namespace DemoTrabajadoresBackend.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearDepartamentoDTO, Departamento>();
            CreateMap<Departamento, CrearDepartamentoDTO>();
            CreateMap<Departamento, DepartamentoDTO>();

            CreateMap<CrearProvinciaDTO, Provincia>();
            CreateMap<Provincia, CrearProvinciaDTO>();
            CreateMap<Provincia, ProvinciaDTO>();

            CreateMap<CrearDistritoDTO, Distrito>();
            CreateMap<Distrito, CrearDistritoDTO>();
            CreateMap<Distrito, DistritoDTO>();

            CreateMap<CrearTrabajadorDTO, Trabajador>();
            CreateMap<Trabajador, CrearTrabajadorDTO>();
            CreateMap<Trabajador, TrabajadorDTO>();

            CreateMap<Trabajador, TrabajadorDetalleDTO>()
                .ForMember(dest => dest.NombreDistrito, opt => opt.MapFrom(src => src.Distrito.NombreDistrito))
                .ForMember(dest => dest.NombreProvincia, opt => opt.MapFrom(src => src.Distrito.Provincia.NombreProvincia))
                .ForMember(dest => dest.NombreDepartamento, opt => opt.MapFrom(src => src.Distrito.Provincia.Departamento.NombreDepartamento));
        }
    }
}
