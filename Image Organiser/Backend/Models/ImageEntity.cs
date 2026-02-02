namespace ImageOrganizer.Api.Models
{
    // This class represents the Images table in SQL Server
    public class ImageEntity
    {
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string FileType { get; set; } = string.Empty;

        public string Orientation { get; set; } = string.Empty;

        public long SizeInKb { get; set; }

        public DateTime UploadedAt { get; set; }


    }
}
