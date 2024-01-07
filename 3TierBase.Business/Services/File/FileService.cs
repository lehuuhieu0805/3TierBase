using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace _3TierBase.Business.Services.File
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            var bucket = _configuration["FireBase:bucket"];

            string fileName = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
            var stream = file.OpenReadStream();

            var task = new FirebaseStorage(bucket)
                .Child("files")
                .Child(fileName)
                .PutAsync(stream);

            string urlImage = await task;
            return urlImage;
        }

        public async Task<IList<string>> UploadFiles(IList<IFormFile> files)
        {
            var bucket = _configuration["FireBase:bucket"];
            List<string> result = [];
            foreach (var file in files)
            {
                string fileName = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
                var stream = file.OpenReadStream();

                var task = new FirebaseStorage(bucket)
                    .Child("files")
                    .Child(fileName)
                    .PutAsync(stream);

                string urlImage = await task;
                result.Add(urlImage);
            }

            return result;
        }
    }
}
