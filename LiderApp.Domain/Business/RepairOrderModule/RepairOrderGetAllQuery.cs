
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.RepairOrderModule
{
    public class RepairOrderGetAllQuery : IRequest<List<RepairOrder>>
    {
        public class RepairOrderGetAllHandler : IRequestHandler<RepairOrderGetAllQuery, List<RepairOrder>>
        {
            private readonly LiderAppDbContext db;

            public RepairOrderGetAllHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<List<RepairOrder>> Handle(RepairOrderGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await db.RepairOrders.Where(r => r.DeletedDate == null)
                    .Include(r=>r.Services)
                    .ToListAsync(cancellationToken);
                if (query == null)
                {
                    throw new ArgumentNullException();
                }
                return query;
            }
        }
    }
}
