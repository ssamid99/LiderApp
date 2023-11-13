using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiderApp.Domain.Business.MenuModule
{
    public class MenuGetSingleQuery : IRequest<Menu>
    {
        public string Id { get; set; }
        public class MenuGetSingleHandler : IRequestHandler<MenuGetSingleQuery, Menu>
        {
            private readonly LiderAppDbContext db;

            public MenuGetSingleHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<Menu> Handle(MenuGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = db.Menus
                    .Include(m => m.Parent)
                    .Include(m => m.Children)
                    .AsQueryable();

                if (query == null)
                    throw new ArgumentNullException();

                return await query.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
            }
        }
    }
}
