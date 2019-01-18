using ImageWriter.Helper;
using ImageWriter.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageWriter.Classes
{
    public class ImageWriter : IImageWriter
    {
        public async Task<string> UploadImage(IFormFile file)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            return "Invalid image file";
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> WriteFile(IFormFile file, string subPath = null)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString() + extension; //Create a new Name for the file due to security reasons.
                string path = "";

                if(subPath != null)
                {
                    path = HandlerPath($@"wwwroot\images\{subPath}", fileName);
                    fileName = $@"{subPath}/{fileName}";
                }
                else
                {
                    path = HandlerPath($@"wwwroot\images", fileName);
                }

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }

        public async Task<string> UploadImage(IFormFile file, string rootPath)
        {
            var result = await WriteFile(file, rootPath);

            return result;
        }

        private string HandlerPath(string path, string fileName)
        {
            try
            {
                string result = "";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    result = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
                }
                else
                {
                    result = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
                }
                Directory.CreateDirectory(path);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
