using LiderApp.Domain.AppCode.Interfaces;
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
    public class MenuPutCommand : IRequest<Menu> , IMenuInterface
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? SubMenuName { get; set; }
        public string ControllerName { get; set; }
        public string? ParentId { get; set; }

        public MenuPutCommand GetMenuData()
        {
            return new MenuPutCommand();
        }

        public class MenuPutHandler : IRequestHandler<MenuPutCommand, Menu>
        {
            private readonly LiderAppDbContext db;

            public MenuPutHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<Menu> Handle(MenuPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Menus.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                if(data == null)
                {
                    throw new ArgumentNullException();
                }
                data.Name = request.Name;
                data.ControllerName = request.ControllerName;
                data.ParentId = request.ParentId;
                data.UpdatedDate = DateTime.UtcNow.AddHours(4);

                await db.SaveChangesAsync(cancellationToken);

                return data;
            }
        }
    }
}
