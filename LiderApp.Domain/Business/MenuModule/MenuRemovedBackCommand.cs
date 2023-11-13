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
    public class MenuRemovedBackCommand : IRequest<Menu>
    {
        public string Id { get; set; }
        public class MenuRemovedBackHandler : IRequestHandler<MenuRemovedBackCommand, Menu>
        {
            private readonly LiderAppDbContext db;

            public MenuRemovedBackHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<Menu> Handle(MenuRemovedBackCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Menus.FirstOrDefaultAsync(m=>m.Id == request.Id && m.DeletedDate != null, cancellationToken);

                if(data == null)
                {
                    throw new ArgumentNullException();
                }

                data.UpdatedDate = DateTime.UtcNow.AddHours(4);
                data.DeletedDate = null;

                await db.SaveChangesAsync();

                return data;
            }
        }
    }
}
