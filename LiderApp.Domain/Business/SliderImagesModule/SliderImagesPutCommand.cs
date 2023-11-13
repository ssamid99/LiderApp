using LiderApp.Domain.AppCode.Extensions;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LiderApp.Domain.Business.SliderImagesModule
{
    public class SliderImagesPutCommand : IRequest<SliderImages>
    {
        public string Id { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public class SliderImagesPutHandler : IRequestHandler<SliderImagesPutCommand, SliderImages> 
        {
            private readonly LiderAppDbContext db;
            private readonly IHostEnvironment env;

            public SliderImagesPutHandler(LiderAppDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }

            public async Task<SliderImages> Handle(SliderImagesPutCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.SliderImages.FirstOrDefaultAsync(s=>s.Id == request.Id, cancellationToken);
                string extension = Path.GetExtension(request.Image.FileName); //.jpg-ni goturur
                request.ImagePath = $"blogpost-{Guid.NewGuid().ToString().ToLower()}{extension}";

                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                string oldPath = env.GetImagePhysicalPath(entity.ImgPath);


                System.IO.File.Move(oldPath, env.GetImagePhysicalPath($"archive{DateTime.Now:yyyyMMdd}-{entity.ImgPath}"));

                entity.ImgPath = request.ImagePath;

                await db.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
