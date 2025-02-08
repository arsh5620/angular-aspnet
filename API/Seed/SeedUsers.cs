using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API;

public class SeedUsers
{
    public static async Task SeedUsersAsync(DefaultDbContext context, ILogger<SeedUsers> logger)
    {
        if (await context.Users.AnyAsync())
        {
            return;
        }

        var fileContents = File.ReadAllText("Seed/UserSeedData.json");
        if (string.IsNullOrEmpty(fileContents))
        {
            logger.LogError("Could not read the seed json file!");
            return;
        }

        var jsonDeserializeSettings = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var users = JsonSerializer.Deserialize<AppUser[]>(fileContents, jsonDeserializeSettings);

        if (users == null || users.Length == 0)
        {
            logger.LogError("Deserialized Json has null or empty users list");
            return;
        }

        foreach (var user in users)
        {
            user.UserName = user.UserName.ToLower();

            var hmac = new HMACSHA512();
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));

            user.PasswordHash = hash;
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);
        }

        await context.SaveChangesAsync();
    }
}
