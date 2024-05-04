using _3TierBase.Business.Services.File;
using _3TierBase.Business.ViewModels;
using _3TierBase.Business.ViewModels.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3TierBase.API.Controllers
{
    public class FileController : BaseApiController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Endpoint for upload file
        /// </summary>
        /// <returns>Url of file</returns>
        /// <response code="200">Returns a url of file</response>
        [HttpPost()]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<FileModel>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Create(IFormFile file)
        {
            return Ok(new BaseResponse<FileModel>(
                msg: "Upload file successfully",
                data: await _fileService.UploadFile(file)
            ));
        }

        /// <summary>
        /// Endpoint for upload multiple file
        /// </summary>
        /// <returns>List url of list file</returns>
        /// <response code="200">Returns list url of list file</response>
        [HttpPost("multiples")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<FileModel>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> CreateMultiple(IList<IFormFile> files)
        {
            return Ok(new BaseResponse<IList<FileModel>>(
                msg: "Upload files successfully",
                data: await _fileService.UploadFiles(files)
            ));
        }
    }
}
