using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;

namespace LiderApp.Domain.Business.ServiceModule
{
    public class ServicePostCommand : IRequest<Service>
    {
        public string Name { get; set; }
        public class ServicePostHandler : IRequestHandler<ServicePostCommand, Service>
        {
            private readonly LiderAppDbContext db;

            public ServicePostHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<Service> Handle(ServicePostCommand request, CancellationToken cancellationToken)
            {
                var command = new Service();
                command.Name = request.Name;
                await db.Services.AddAsync(command, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return command;
            }
        }
    }
}
