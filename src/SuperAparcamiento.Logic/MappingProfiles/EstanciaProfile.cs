using AutoMapper;
using SuperAparcamiento.Logic.Contract.Estancias;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.Logic.MappingProfiles;

public class EstanciaProfile : Profile
{
    public EstanciaProfile()
    {
        // Responses
        CreateMap<Estancia, RegistrarEntradaResponseContract>(MemberList.Destination);
    }
}
