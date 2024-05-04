using _3TierBase.Business.ViewModels.Files;
using Microsoft.AspNetCore.Http;

namespace _3TierBase.Business.Services.File
{
    public interface IFileService
    {
        public Task<FileModel> UploadFile(IFormFile file);
        public Task<IList<FileModel>> UploadFiles(IList<IFormFile> files);
    }
}
