using LiderApp.Domain.AppCode.Extensions;
using LiderApp.Domain.AppCode.Infrastucture;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LiderApp.Domain.Business.SliderImagesModule
{
    public class SliderImagesPostCommand : IRequest<SliderImages>
    {
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public class SliderImagesPostHandler : IRequestHandler<SliderImagesPostCommand, SliderImages>
        {
            private readonly LiderAppDbContext db;
            private readonly IHostEnvironment env;
            private readonly IConfiguration configuration;

            public SliderImagesPostHandler(LiderAppDbContext db, IHostEnvironment env, IConfiguration configuration)
            {
                this.db = db;
                this.env = env;
                this.configuration = configuration;
            }
            public async Task<SliderImages> Handle(SliderImagesPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new SliderImages();
                string extension = Path.GetExtension(request.Image.FileName);//.jpg

                request.ImagePath = $"blogpost-{Guid.NewGuid().ToString().ToLower()}{extension}";
                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                entity.ImgPath = request.ImagePath;
                await db.SliderImages.AddAsync(entity, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
