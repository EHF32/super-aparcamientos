using SuperAparcamiento.DataAccess.Persistence;
using SuperAparcamiento.DataAccess.Repositories.Interfaces;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.DataAccess.Repositories;

public class VehiculoRepository(AparcamientoDbContext context) : BaseRepository<Vehiculo>(context), IVehiculoRepository { }


