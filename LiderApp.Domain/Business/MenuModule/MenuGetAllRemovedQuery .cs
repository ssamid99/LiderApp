using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.MenuModule
{
    public class MenuGetAllRemovedQuery : IRequest<List<Menu>>
    {
        public class MenuGetAllRemovedHandler : IRequestHandler<MenuGetAllRemovedQuery, List<Menu>>
        {
            private readonly LiderAppDbContext db;

            public MenuGetAllRemovedHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Menu>> Handle(MenuGetAllRemovedQuery request, CancellationToken cancellationToken)
            {
                var query = db.Menus
                    .Include(m => m.Parent)
                    .Include(m => m.Children)
                    .AsQueryable();
                if(query == null)
                {
                    throw new ArgumentNullException();
                }
                return await query.Where(m => m.DeletedDate != null).ToListAsync(cancellationToken);
            }
        }
    }
}
