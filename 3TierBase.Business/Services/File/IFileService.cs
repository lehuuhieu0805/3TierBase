using Microsoft.AspNetCore.Http;

namespace _3TierBase.Business.Services.File
{
    public interface IFileService
    {
        public Task<string> UploadFile(IFormFile file);
        public Task<IList<string>> UploadFiles(IList<IFormFile> files);
    }
}
