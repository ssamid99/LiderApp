using LiderApp.Domain.AppCode.Extensions;
using LiderApp.Domain.AppCode.Infrastucture;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace LiderApp.Domain.Business.AboutUsModule
{
    public class AboutUsPostCommand : IRequest<AboutUs>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public ImageItem[] Images { get; set; }
        public class AboutUsPostHandler : IRequestHandler<AboutUsPostCommand, AboutUs>
        {
            private readonly LiderAppDbContext db;
            private readonly IHostEnvironment env;

            public AboutUsPostHandler(LiderAppDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<AboutUs> Handle(AboutUsPostCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = new AboutUs();
                    entity.Title = request.Title;
                    entity.Text = request.Text;

                    if (request.Images != null && request.Images.Where(i => i.File != null).Count() > 0)
                    {
                        entity.WebImages = new List<WebImage>();
                        foreach (var item in request.Images.Where(i => i.File != null))
                        {
                            var image = new WebImage();

                            image.IsMain = item.IsMain;

                            var extension = Path.GetExtension(item.File.FileName);

                            image.Name = $"aboutus-{Guid.NewGuid().ToString().ToLower()}{extension}";

                            var fullPath = env.GetImagePhysicalPath(image.Name);

                            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                            {
                                await item.File.CopyToAsync(fs, cancellationToken);
                            }

                            entity.WebImages.Add(image);
                        }

                    }
                    await db.AboutUs.AddAsync(entity, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                    return entity;
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }
    }
}
