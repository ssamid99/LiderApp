using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiderApp.Domain.Business.SliderImagesModule
{
    public class SliderImagesRemoveCommand : IRequest<SliderImages>
    {
        public string Id { get; set; }
        public class SliderImagesRemoveHandler : IRequestHandler<SliderImagesRemoveCommand, SliderImages>
        {
            private readonly LiderAppDbContext db;

            public SliderImagesRemoveHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<SliderImages> Handle(SliderImagesRemoveCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.SliderImages.FirstOrDefaultAsync(s => s.Id == request.Id && s.DeletedDate == null, cancellationToken);
                if (entity == null)
                {
                    return null;
                }
                
                entity.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
