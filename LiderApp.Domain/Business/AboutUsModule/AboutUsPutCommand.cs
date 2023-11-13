using LiderApp.Domain.AppCode.Extensions;
using LiderApp.Domain.AppCode.Infrastucture;
using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LiderApp.Domain.Business.AboutUsModule
{
    public class AboutUsPutCommand : IRequest<AboutUs>, IAboutUsInterface
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ImageItem[] Images { get; set; }

        public AboutUsPutCommand GetData()
        {
            return new AboutUsPutCommand();
        }

        public class AboutUsPutHandler : IRequestHandler<AboutUsPutCommand, AboutUs>
        {
            private readonly LiderAppDbContext db;
            private readonly IHostEnvironment env;

            public AboutUsPutHandler(LiderAppDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<AboutUs> Handle(AboutUsPutCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await db.AboutUs
                    .Include(a => a.WebImages.Where(w=>w.DeletedDate == null))
                    .FirstOrDefaultAsync(a => a.Id == request.Id && a.DeletedDate == null, cancellationToken);

                    entity.Title = request.Title;
                    entity.Text = request.Text;

                    var imgTable = await db.WebImages.Where(w => w.DeletedDate == null).ToListAsync(cancellationToken);

                    if (request.Images != null && request.Images.Where(i => i.File != null).Count() > 0)
                    {
                        //db.WebImages.RemoveRange(imgTable);
                        //entity.WebImages = new List<WebImage>();
                        //foreach (var item in request.Images.Where(i => i.File != null))
                        //{
                        //    var image = new WebImage();

                        //    image.IsMain = item.IsMain;

                        //    var extension = Path.GetExtension(item.File.FileName);

                        //    image.Name = $"aboutus-{Guid.NewGuid().ToString().ToLower()}{extension}";

                        //    var fullPath = env.GetImagePhysicalPath(image.Name);

                        //    using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                        //    {
                        //        await item.File.CopyToAsync(fs, cancellationToken);
                        //    }

                        //    entity.WebImages.Add(image);
                        //}
                        #region Elave edilen Files
                        foreach (var imageItem in request.Images.Where(i => i.File != null && i.Id == null))
                        {
                            var image = new WebImage();
                            image.IsMain = imageItem.IsMain;
                            image.AboutUsId = entity.Id;

                            string extension = Path.GetExtension(imageItem.File.FileName);//.jpg
                            image.Name = $"aboutus-{Guid.NewGuid().ToString().ToLower()}{extension}";

                            string fullName = env.GetImagePhysicalPath(image.Name);

                            using (var fs = new FileStream(fullName, FileMode.Create, FileAccess.Write))
                            {
                                await imageItem.File.CopyToAsync(fs, cancellationToken);
                            }
                            entity.WebImages.Add(image);
                        }
                        #endregion

                        #region Movcud shekilerden silinibse
                        foreach (var imageItem in request.Images.Where(i => i.Id > 0 && string.IsNullOrWhiteSpace(i.TempPath)))
                        {
                            var data = await db.WebImages.FirstOrDefaultAsync(pi => pi.Id == imageItem.Id && pi.AboutUsId == entity.Id);
                            if (data != null)
                            {
                                data.IsMain = false;
                                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                            }
                        }
                        #endregion

                        #region Deyishiklik edilmeyibse
                        foreach (var imageItem in entity.WebImages)
                        {
                            var formForm = request.Images.FirstOrDefault(i => i.Id == imageItem.Id);
                            if (formForm != null)
                            {

                                imageItem.IsMain = formForm.IsMain;
                            }
                        }
                        #endregion
                    }

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
