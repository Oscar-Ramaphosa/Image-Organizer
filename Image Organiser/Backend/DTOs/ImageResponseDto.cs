namespace ImageOrganizer.Api.DTOs
{
    public class ImageResponseDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Orientation { get; set; }
        public long SizeInKb { get; set; }
        public DateTime UploadedAt { get; set; }

        public string Url { get; set; }
    }
}
