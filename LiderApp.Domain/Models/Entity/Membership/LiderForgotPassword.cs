using LiderApp.Domain.AppCode.Infrastucture;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiderApp.Domain.Models.Entity.Membership
{
    public class LiderForgotPassword : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
