namespace API;

public class PathUtils
{
    public static string GetUserStoragePathForPhotos()
    {
        var userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return Path.Join(userProfilePath, "Aspnet-Data");
    }

    public static Uri GetBaseUriForPhotos()
    {
        return new Uri("http://localhost:4123/");
    }
}
