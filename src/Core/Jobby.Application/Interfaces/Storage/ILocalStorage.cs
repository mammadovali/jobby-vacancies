using Microsoft.AspNetCore.Http;

namespace Jobby.Application.Interfaces.Storage
{
    public interface ILocalStorage
    {
        Task<List<string>> UploadAsync(string path, IFormFileCollection files);
        Task<string> Upload(string path, IFormFile file);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);
    }
}
