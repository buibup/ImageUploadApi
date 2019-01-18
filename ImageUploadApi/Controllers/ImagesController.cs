using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageUploadApi.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageHandler _imageHandler;

        public ImagesController(IImageHandler imageHandler)
        {
            _imageHandler = imageHandler;
        }

        /// <summary>
        /// Uplaods an image to the server.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return await _imageHandler.UploadImage(file);
        }

        [HttpPost("{subPath}")]
        public async Task<IActionResult> UploadImage(IFormFile file, string subPath)
        {
            return await _imageHandler.UploadImage(file, subPath);
        }
    }
}