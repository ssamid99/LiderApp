using Microsoft.Extensions.Hosting;

namespace LiderApp.Application.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string GetImagePhysicalPath(this IHostEnvironment env, string fileName)
        {
            return Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", fileName);
        }

        public static void ArchivePath(this IHostEnvironment env, string fileName)
        {
            var imageActualPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", fileName);

            if (File.Exists(imageActualPath))
            {
                var imageNewPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", $"archive-{DateTime.UtcNow.AddHours(4)}-{fileName}");
                File.Move(imageActualPath, imageNewPath);
            }
        }
    }
}
