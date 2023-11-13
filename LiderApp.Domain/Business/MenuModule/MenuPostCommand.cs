using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiderApp.Domain.Business.MenuModule
{
    public class MenuPostCommand : IRequest<Menu>
    {
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string? ParentId { get; set; }
        public class MenuPostHandler : IRequestHandler<MenuPostCommand, Menu>
        {
            private readonly LiderAppDbContext db;

            public MenuPostHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<Menu> Handle(MenuPostCommand request, CancellationToken cancellationToken)
            {
                var data = new Menu(request.Name, request.ControllerName);
                data.ParentId = request.ParentId;
                if (request.ParentId == null)
                {
                    throw new ArgumentNullException(request.ParentId, "Yeni Categoriya növü yaradila bilməz. Uyğun Menyu növlərindən seçim edin!");
                }
                else
                {
                    await db.Menus.AddAsync(data, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                }

                return data;
            }
        }
    }
}
