using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.WarrantyModule
{
    public class WarrantyGetAllQuery : IRequest<List<Warranty>>
    {
        public class WarrantyGetAllHandler : IRequestHandler<WarrantyGetAllQuery, List<Warranty>>
        {
            private readonly LiderAppDbContext db;

            public WarrantyGetAllHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Warranty>> Handle(WarrantyGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await db.Warranties.Where(w => w.DeletedDate == null).ToListAsync(cancellationToken);

                if(query.Count == 0)
                {
                    return null;
                }

                return query ;
            }
        }
    }
}
