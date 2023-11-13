using LiderApp.Domain.AppCode.Extensions;
using LiderApp.Domain.AppCode.Infrastucture;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace LiderApp.Domain.Business.WarrantyModule
{
    public class WarrantyPostCommand : IRequest<JsonResponse>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public class WarrantyPostHandler : IRequestHandler<WarrantyPostCommand, JsonResponse>
        {
            private readonly LiderAppDbContext db;
            private readonly IHostEnvironment env;

            public WarrantyPostHandler(LiderAppDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<JsonResponse> Handle(WarrantyPostCommand request, CancellationToken cancellationToken)
            {
                var command = new Warranty();

                command.Title = request.Title;
                command.Text = request.Text;

                if(request.Image == null)
                {
                    throw new ArgumentNullException(request.ImagePath, "Şəkil seçilməyib!");
                }

                string extension = Path.GetExtension(request.Image.FileName);

                request.ImagePath = $"warranty-{Guid.NewGuid().ToString().ToLower()}{extension}";

                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                command.ImagePath = request.ImagePath;

                await db.Warranties.AddAsync(command, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                };
            }
        }
    }
}
