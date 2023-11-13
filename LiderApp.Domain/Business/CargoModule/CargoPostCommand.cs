﻿using LiderApp.Domain.AppCode.Extensions;
using LiderApp.Domain.AppCode.Infrastucture;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace LiderApp.Domain.Business.CargoModule
{
    public class CargoPostCommand : IRequest<JsonResponse>
    {
        public string Text { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public class CargoPostHandler : IRequestHandler<CargoPostCommand, JsonResponse>
        {
            private readonly LiderAppDbContext db;
            private readonly IHostEnvironment env;

            public CargoPostHandler(LiderAppDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<JsonResponse> Handle(CargoPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new Cargo();

                entity.Text = request.Text;

                if (request.Image == null)
                {
                    throw new ArgumentNullException(request.ImagePath, "Şəkil seçilməyib!");
                }

                string extension = Path.GetExtension(request.Image.FileName);

                request.ImagePath = $"cargo-{Guid.NewGuid().ToString().ToLower()}{extension}";

                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }
                entity.ImagePath = request.ImagePath;
                
                await db.Cargos.AddAsync(entity, cancellationToken);
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
