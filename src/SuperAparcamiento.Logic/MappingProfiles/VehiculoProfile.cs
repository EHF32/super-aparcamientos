using AutoMapper;
using SuperAparcamiento.Logic.Contract.Vehiculo;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.Logic.MappingProfiles;

public class VehiculoProfile : Profile
{
    public VehiculoProfile()
    {
        // Requests
        CreateMap<CreateVehiculoContract, Vehiculo>(MemberList.Source);

        // Responses
        CreateMap<Vehiculo, VehiculoResponseContract>();
    }
}
