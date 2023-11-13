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
    public class SliderImagesGetAllQuery : IRequest<List<SliderImages>>
    {
        public class SliderImagesGetAllHandler : IRequestHandler<SliderImagesGetAllQuery, List<SliderImages>>
        {
            private readonly LiderAppDbContext db;

            public SliderImagesGetAllHandler(LiderAppDbContext db)
            {
                this.db = db;
            }
            public async Task<List<SliderImages>> Handle(SliderImagesGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await db.SliderImages.Where(s => s.DeletedDate != null).ToListAsync(cancellationToken);
                
                return query;
            }
        }
    }
}
