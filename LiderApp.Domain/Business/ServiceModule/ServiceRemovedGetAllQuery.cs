using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.ServiceModule
{
    public class ServiceRemovedGetAllQuery : IRequest<List<Service>>
    {
        public class ServiceRemovedGetAllHandler : IRequestHandler<ServiceRemovedGetAllQuery, List<Service>>
        {
            private readonly LiderAppDbContext db;

            public ServiceRemovedGetAllHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Service>> Handle(ServiceRemovedGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await db.Services.Where(s => s.DeletedDate != null).ToListAsync(cancellationToken);
                if(query == null)
                {
                    throw new ArgumentNullException();
                }
                return query;
            }
        }
    }
}
