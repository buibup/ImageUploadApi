﻿using ImageWriter.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadApi.Handler
{
    public interface IImageHandler
    {
        Task<IActionResult> UploadImage(IFormFile file);
        Task<IActionResult> UploadImage(IFormFile file, string subPath);
    }

    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _imageWriter.UploadImage(file);
            return new ObjectResult(result);
        }

        public async Task<IActionResult> UploadImage(IFormFile file, string subPath)
        {
            var result = await _imageWriter.UploadImage(file, subPath);
            return new ObjectResult(result);
        }
    }
}
