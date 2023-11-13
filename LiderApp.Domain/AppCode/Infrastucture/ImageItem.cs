﻿using Microsoft.AspNetCore.Http;

namespace LiderApp.Domain.AppCode.Infrastucture
{
    public class ImageItem
    {
        public int? Id { get; set; }
        public bool IsMain { get; set; }
        public string TempPath { get; set; }
        public IFormFile File { get; set; }
    }
}
