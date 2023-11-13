using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.RepairOrderModule
{
    public class RepairOrderGetSingleQuery : IRequest<RepairOrder>
    {
        public string Id { get; set; }
        public class RepairOrderGetSingleHandler : IRequestHandler<RepairOrderGetSingleQuery, RepairOrder>
        {
            private readonly LiderAppDbContext db;

            public RepairOrderGetSingleHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<RepairOrder> Handle(RepairOrderGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = db.RepairOrders
                    .Include(r => r.Services)
                    .AsQueryable();
                var data = await query.FirstOrDefaultAsync(r => r.Id == request.Id && r.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    throw new ArgumentNullException();
                }
                return data;
            }
        }
    }
}
