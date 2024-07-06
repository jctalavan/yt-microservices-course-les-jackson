using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository(AppDbContext context) : IPlatformRepository
    {
        public void CreatePlatform(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);

            context.Platforms.Add(platform);
        }

        public Platform? GetPlatform(int id)
        {
            return context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return [.. context.Platforms];
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}