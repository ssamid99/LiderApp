using LiderApp.Domain.AppCode.Extensions;
using LiderApp.Domain.AppCode.Infrastucture;
using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LiderApp.Domain.Business.WarrantyModule
{
    public class WarrantyPutCommand : IRequest<Warranty>, IWarrantyInterface
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }

        public WarrantyPutCommand GetData()
        {
            return new WarrantyPutCommand();
        }

        public class WarrantyPutHandler : IRequestHandler<WarrantyPutCommand, Warranty>
        {
            private readonly LiderAppDbContext db;
            private readonly IHostEnvironment env;

            public WarrantyPutHandler(LiderAppDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Warranty> Handle(WarrantyPutCommand request, CancellationToken cancellationToken)
            {
                var command = await db.Warranties.FirstOrDefaultAsync(w => w.Id == request.Id && w.DeletedDate == null, cancellationToken);
                if(command == null)
                throw new ArgumentNullException();

                if (request.Title != null)
                    command.Title = request.Title;

                if(request.Text != null)
                command.Text = request.Text;

                command.UpdatedDate = DateTime.UtcNow.AddHours(4);
                if (request.Image == null)
                    goto end;

                var extension = Path.GetExtension(request.Image.FileName);

                request.ImagePath = $"warrant-{Guid.NewGuid().ToString().ToLower()}{extension}";

                var fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                string oldPath = env.GetImagePhysicalPath(command.ImagePath);

                System.IO.File.Move(oldPath, env.GetImagePhysicalPath($"archive{DateTime.Now:yyyyMMdd}-{command.ImagePath}"));

                command.ImagePath = request.ImagePath;

            end:

                await db.SaveChangesAsync(cancellationToken);

                return command;
            }
        }
    }
}
