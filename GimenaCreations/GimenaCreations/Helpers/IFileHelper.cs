namespace GimenaCreations.Helpers;

public interface IFileHelper
{
    Task CreateFileAsync(string fileName, IFormFile formFile);
}
