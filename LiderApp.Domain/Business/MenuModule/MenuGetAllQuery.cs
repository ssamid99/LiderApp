using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.Domain.Business.MenuModule
{
    public class MenuGetAllQuery : IRequest<List<Menu>>
    {
        public class MenuGetAllHandler : IRequestHandler<MenuGetAllQuery, List<Menu>>
        {
            private readonly LiderAppDbContext db;

            public MenuGetAllHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Menu>> Handle(MenuGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await db.Menus.Where(m => m.DeletedDate == null).ToListAsync(cancellationToken);
                if(query == null)
                {
                    throw new ArgumentNullException();
                }
                return query;
            }
        }
    }
}
