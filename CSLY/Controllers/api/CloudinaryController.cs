using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSLY.Controllers.api
{
    public class CloudinaryController : ApiController
    {
        public static Cloudinary cloudinary;

        public const string CLOUD_NAME = "";
        public const string API_KEY = "";
        public const string API_SECRET = "";

        public IHttpActionResult UploadImageToCloudinary()
        {
            string imagePath = "";
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath)
                };
                //new image path on cloudinary
               return Ok(cloudinary.Upload(uploadParams).Url);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
