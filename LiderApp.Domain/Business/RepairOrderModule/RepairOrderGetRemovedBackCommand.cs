using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiderApp.Domain.Business.RepairOrderModule
{
    public class RepairOrderGetRemovedBackCommand : IRequest<RepairOrder>
    {
        public string Id { get; set; }
        public class RepairOrderGetRemovedBackHandler : IRequestHandler<RepairOrderGetRemovedBackCommand, RepairOrder>
        {
            private readonly LiderAppDbContext db;

            public RepairOrderGetRemovedBackHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<RepairOrder> Handle(RepairOrderGetRemovedBackCommand request, CancellationToken cancellationToken)
            {
                var command = await db.RepairOrders.FirstOrDefaultAsync(r => r.Id == request.Id && r.DeletedDate != null, cancellationToken);
                if (command == null)
                    throw new ArgumentNullException();

                command.DeletedDate = null;
                command.UpdatedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return command;
            }
        }
    }
}
