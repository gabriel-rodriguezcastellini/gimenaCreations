namespace GimenaCreations.Helpers;

public class FileHelper : IFileHelper
{
    public async Task CreateFileAsync(string fileName, IFormFile formFile)
    {
        using var stream = File.Create(fileName);
        await formFile.CopyToAsync(stream);
    }
}
