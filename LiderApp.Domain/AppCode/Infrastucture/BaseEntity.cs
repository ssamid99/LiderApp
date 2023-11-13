using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiderApp.Domain.AppCode.Infrastucture
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id =  Guid.NewGuid().ToString().ToLower();
        }
        public string Id { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now.AddHours(4);
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? CreatedbyUserId { get; set; }

    }
}
