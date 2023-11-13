using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.ServiceModule
{
    public class ServiceGetBackRemovedCommand : IRequest<Service>
    {
        public string Id { get; set; }
        public class ServiceGetBackRemovedHandler : IRequestHandler<ServiceGetBackRemovedCommand, Service>
        {
            private readonly LiderAppDbContext db;

            public ServiceGetBackRemovedHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<Service> Handle(ServiceGetBackRemovedCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Services.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate != null, cancellationToken);

                if (data == null)
                {
                    return null;
                }
                data.DeletedDate = null;
                data.UpdatedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
