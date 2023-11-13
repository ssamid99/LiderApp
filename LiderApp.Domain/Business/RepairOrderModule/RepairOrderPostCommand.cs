using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;

namespace LiderApp.Domain.Business.RepairOrderModule
{
    public class RepairOrderPostCommand : IRequest<RepairOrder>
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
        public string ServiceId { get; set; }
        public class RepairOrderPostHandler : IRequestHandler<RepairOrderPostCommand, RepairOrder>
        {
            private readonly LiderAppDbContext db;

            public RepairOrderPostHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<RepairOrder> Handle(RepairOrderPostCommand request, CancellationToken cancellationToken)
            {
                var data = new RepairOrder();
                data.FullName = request.FullName;
                data.PhoneNumber = request.PhoneNumber;
                data.Text = request.Text;
                data.ServiceId = request.ServiceId;

                await db.RepairOrders.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
