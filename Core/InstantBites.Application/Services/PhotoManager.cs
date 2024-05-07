using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Services
{
    public class PhotoManager
    {
        private readonly IConfiguration _configuration;
        private readonly CloudinarySettings _cloudinarySettings;
        private readonly Cloudinary _cloudinary;

        public PhotoManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _cloudinarySettings = _configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            Account account = new Account(_cloudinarySettings.CloudName, _cloudinarySettings.ApiKey, _cloudinarySettings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
        public string AddPhoto(IFormFile formFile)
        {
            try
            {
                var ur = new ImageUploadResult();
                if (formFile.Length > 0)
                {
                    using (var stream = formFile.OpenReadStream())
                    {
                        var up = new ImageUploadParams()
                        {
                            File = new FileDescription(formFile.Name, stream)
                        };
                        ur = _cloudinary.Upload(up);
                    }
                }
                return ur.Uri.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
