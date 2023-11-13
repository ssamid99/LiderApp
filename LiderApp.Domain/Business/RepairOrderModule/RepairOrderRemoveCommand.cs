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
    public class RepairOrderRemoveCommand : IRequest<RepairOrder>
    {
        public string Id { get; set; }
        public class RepairOrderRemoveHandler : IRequestHandler<RepairOrderRemoveCommand, RepairOrder>
        {
            private readonly LiderAppDbContext db;

            public RepairOrderRemoveHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<RepairOrder> Handle(RepairOrderRemoveCommand request, CancellationToken cancellationToken)
            {
                var command = await db.RepairOrders.FirstOrDefaultAsync(r => r.Id == request.Id && r.DeletedDate == null, cancellationToken);
                if (command == null)
                    throw new ArgumentNullException();

                command.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return command;
            }
        }
    }
}
