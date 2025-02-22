namespace API;

public class PhotoService
{
    public async Task<PhotoCreated?> AddPhotoAsync(IFormFile file)
    {
        var storePath = PathUtils.GetUserStoragePathForPhotos();
        var randomFileName = Path.GetRandomFileName();

        var combinedPath = Path.Combine(storePath, randomFileName);
        using var fileStream = File.OpenWrite(combinedPath);
        await file.CopyToAsync(fileStream);

        return new PhotoCreated()
        {
            FileName = randomFileName
        };
    }
}

public class PhotoCreated
{
    public required string FileName { get; set; }
}

