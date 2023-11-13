using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.AboutUsModule
{
    public class AboutUsGetAllQuery : IRequest<List<AboutUs>>
    {
        public class AboutUsGetAllHandler : IRequestHandler<AboutUsGetAllQuery, List<AboutUs>>
        {
            private readonly LiderAppDbContext db;

            public AboutUsGetAllHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<List<AboutUs>> Handle(AboutUsGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await db.AboutUs.Where(r => r.DeletedDate == null)
                   .Include(r => r.WebImages)
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
