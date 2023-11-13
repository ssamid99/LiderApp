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
    public class SliderImagesGetSingleQuery : IRequest<SliderImages>
    {
        public string Id { get; set; }
        public class SliderImagesGetSingleHandler : IRequestHandler<SliderImagesGetSingleQuery, SliderImages>
        {
            private readonly LiderAppDbContext db;

            public SliderImagesGetSingleHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<SliderImages> Handle(SliderImagesGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = await db.SliderImages.FirstOrDefaultAsync(s=>s.Id == request.Id && s.DeletedDate == null);
                
                return query;
            }
        }
    }
}
