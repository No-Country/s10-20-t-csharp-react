using AutoMapper;
using NetTopologySuite;
using quejapp.Models;
using s10.Back.DTO;

namespace s10.Back.Handler;

public class AutoMapperHandler : Profile
{
    public AutoMapperHandler()
    {
        CreateMap<Category, CategoryDTO>();

        CreateMap<Queja, QuejaResponseDTO>()
            .ForMember(q => q.Latitude, q => q.MapFrom(q => q.Location!.Y))
            .ForMember(q => q.Longitude, q => q.MapFrom(q => q.Location!.X));

        //var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        //CreateMap<QuejaResponseDTO, Queja>()
        //    .ForMember(q => q.Location, 
        //    q => q.MapFrom(l => 
        //    geometryFactory.CreatePoint(
        //        new NetTopologySuite.Geometries.Coordinate(l.Longitude, l.Latitude))));
    }
}
