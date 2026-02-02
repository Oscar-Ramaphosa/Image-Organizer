using ImageOrganizer.Api.Data;
using ImageOrganizer.Api.Models;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageOrganizer.Api.Services
{
    // This service contains the business logic.
    // Controllers should remain thin and delegate work to services.
    public class ImageService
    {
        private readonly string _uploadFolder;
        private readonly AppDbContext _context;

        public ImageService(AppDbContext context)
        {
            _context = context;

            // Define where uploaded images will be stored on the server
            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            // Ensure the upload directory exists
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        // Saves the uploaded image to disk and database
        public async Task<ImageEntity> SaveImageAsync(IFormFile file)
        {
            // Generate a unique file name to prevent overwriting
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(_uploadFolder, uniqueFileName);

            // Save file to disk asynchronously
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Determine image orientation using ImageSharp
            string orientation;
            using (var image = Image.Load<Rgba32>(filePath))
            {
                orientation = image.Width >= image.Height
                    ? "Landscape"
                    : "Portrait";
            }

            // Create image metadata entity
            var imageEntity = new ImageEntity
            {
                FileName = file.FileName,
                FilePath = filePath,
                FileType = Path.GetExtension(file.FileName),
                Orientation = orientation,
                SizeInKb = file.Length / 1024,
                UploadedAt = DateTime.UtcNow
            };

            // Save metadata to database
            _context.Images.Add(imageEntity);
            await _context.SaveChangesAsync();

            return imageEntity;
        }


    }
}
