﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiderApp.Domain.Models.FormData
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
