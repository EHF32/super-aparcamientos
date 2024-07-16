using SuperAparcamiento.DataAccess.Persistence;
using SuperAparcamiento.DataAccess.Repositories.Interfaces;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.DataAccess.Repositories;

public class TipoVehiculoRepository(AparcamientoDbContext context) : BaseRepository<TipoVehiculo>(context), ITipoVehiculoRepository { }


