using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shitou.Framework.Demo.Mvc.Controllers
{
    public class ImageController : Controller
    {
        private ILogger _logger { get; set; }
        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 批量上传图片(入库)，Webupload排量上传，其实是多次调用，每次调用上传一张
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UploadImage(IFormFile file, string imageType)
        {
            string url = string.Empty;
            if (file != null)
            {

                url = UploadPic(file, imageType);
                _logger?.LogInformation("上传图片路径：" + url);

            }
            return url;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="PicType"></param>
        /// <param name="targetID"></param>
        /// <param name="fileName"></param>
        private string UploadPic(IFormFile file, string imageType)
        {
            try
            {
                string rootPath = Environment.CurrentDirectory;
                string uploadDir = Path.Combine(rootPath, "upload/" + imageType);
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string relativePath = string.Format("/upload/{0}/{1}.jpg", imageType, Guid.NewGuid().ToString());
                using (var fileStream = new FileStream(rootPath.TrimEnd('/') + relativePath, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }
                return relativePath;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "UploadPic");
                return "";
            }
        }
    }
}
