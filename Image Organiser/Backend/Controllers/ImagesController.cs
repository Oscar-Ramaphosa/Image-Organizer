using ImageOrganizer.Api.Data;
using ImageOrganizer.Api.DTOs;
using ImageOrganizer.Api.Models;
using ImageOrganizer.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ImageService _imageService;

        public ImageController(AppDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var image = await _imageService.SaveImageAsync(file);
            return Ok(image);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var images = _context.Images
         .OrderByDescending(i => i.UploadedAt)
         .Select(i => new ImageResponseDto
         {
             Id = i.Id,
             FileName = i.FileName,
             FileType = i.FileType,
             Orientation = i.Orientation,
             SizeInKb = i.SizeInKb,
             UploadedAt = i.UploadedAt,
             Url = $"https://localhost:7179/uploads/{Path.GetFileName(i.FilePath)}"
         })
         .ToList();

            return Ok(images);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Find the image by ID
            var image = _context.Images.FirstOrDefault(i => i.Id == id);
            if (image == null)
                return NotFound("Image not found");

            // Delete the file from disk
            if (System.IO.File.Exists(image.FilePath))
            {
                System.IO.File.Delete(image.FilePath);
            }

            // Remove from database
            _context.Images.Remove(image);
            _context.SaveChanges();

            return Ok(new { message = "Image deleted successfully" });
        }
   
    }

}
