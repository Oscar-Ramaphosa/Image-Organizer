namespace ImageOrganizer.Api.Models
{
    // This class represents image metadata
    // It does NOT store the image itself, only information about it
    public class ImageFile
    {
        // Unique identifier for the image
        public int Id { get; set; }

        // Original file name (e.g. photo1.jpg)
        public string FileName { get; set; } = string.Empty;

        // Path where the image is stored on disk
        public string FilePath { get; set; } = string.Empty;

        // Image format (JPEG, PNG, etc.)
        public string FileType { get; set; } = string.Empty;

        // Portrait or Landscape
        public string Orientation { get; set; } = string.Empty;

        // File size in KB
        public long SizeInKb { get; set; }

        // Date the image was uploaded
        public DateTime UploadedAt { get; set; }
    }
}
