using LiderApp.Domain.AppCode.Extensions;
using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LiderApp.Domain.Business.CargoModule
{
    public class CargoPutCommand : IRequest<Cargo>, ICargoInterface
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }

        public CargoPutCommand GetData()
        {
            return new CargoPutCommand();
        }

        public class CargoPutHandler : IRequestHandler<CargoPutCommand, Cargo>
        {
            private readonly LiderAppDbContext db;
            private readonly IHostEnvironment env;

            public CargoPutHandler(LiderAppDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Cargo> Handle(CargoPutCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Cargos.FirstOrDefaultAsync(c => c.Id == request.Id && c.DeletedDate == null, cancellationToken);
                if(entity == null)
                {
                    throw new ArgumentNullException();
                }
                if(request.Text !=  null)
                {
                    entity.Text = request.Text;
                }
                if(request.Image == null)
                {
                    goto end;
                }

                var extension = Path.GetExtension(request.Image.FileName);

                request.ImagePath = $"cargo-{Guid.NewGuid().ToString().ToLower()}{extension}";

                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                string oldPath = env.GetImagePhysicalPath(entity.ImagePath);

                System.IO.File.Move(oldPath, env.GetImagePhysicalPath($"archive-{DateTime.Now.AddHours(4):yyyyMMdd}-{entity.ImagePath}"));

                entity.ImagePath = request.ImagePath;

            end:
                entity.UpdatedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
