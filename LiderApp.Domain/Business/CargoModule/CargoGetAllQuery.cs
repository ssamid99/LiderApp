using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.CargoModule
{
    public class CargoGetAllQuery : IRequest<List<Cargo>>
    {
        public class CargoGetAllHandler : IRequestHandler<CargoGetAllQuery, List<Cargo>>
        {
            private readonly LiderAppDbContext db;

            public CargoGetAllHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Cargo>> Handle(CargoGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await db.Cargos.Where(c => c.DeletedDate == null).ToListAsync(cancellationToken);
                if(query.Count() == 0)
                {
                    return null;
                }
                return query;
            }
        }
    }
}
