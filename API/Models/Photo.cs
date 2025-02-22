namespace API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public bool IsMain { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
