using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.ServiceModule
{
    public class ServicePutCommand : IRequest<Service>, IServiceInterface
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ServicePutCommand GetServiceData()
        {
            ServicePutCommand command = new ServicePutCommand();
            return command;
        }

        public class ServicePutHandler : IRequestHandler<ServicePutCommand, Service>
        {
            private readonly LiderAppDbContext db;

            public ServicePutHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<Service> Handle(ServicePutCommand request, CancellationToken cancellationToken)
            {
                var command = await db.Services.FirstOrDefaultAsync(s => s.Id == request.Id && s.DeletedDate == null, cancellationToken);
                if (command == null)
                {
                    throw new ArgumentNullException();
                }
                command.Name = request.Name;
                command.UpdatedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return command;
            }
        }
    }
}
