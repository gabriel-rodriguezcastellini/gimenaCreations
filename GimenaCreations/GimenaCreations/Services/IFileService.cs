namespace GimenaCreations.Services;

public interface IFileService
{
    Task<ICollection<Models.File>> GetFilesToDownloadAsync(int orderId, string userId);
}
