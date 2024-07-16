using SuperAparcamiento.DataAccess.Persistence;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.DataAccess.Repositories;

public class EstanciaRepository(AparcamientoDbContext context) : BaseRepository<Estancia>(context), IEstanciaRepository { }


