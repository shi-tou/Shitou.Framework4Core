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
        /// 批量上传图片(入库)，Webupload排量上传，其他也是多少调用，每次调用上传一张
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(IFormFile file)
        {
            if (file != null)
            {

                string url = UploadPic(file, "Test");
                _logger?.LogInformation("上传图片路径：" + url);

            }
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="PicType"></param>
        /// <param name="targetID"></param>
        /// <param name="fileName"></param>
        private string UploadPic(IFormFile file, string fileName)
        {
            try
            {
                string rootPath = Environment.CurrentDirectory;
                string uploadDir = Path.Combine(rootPath, "upload/" + fileName);
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string relativePath = string.Format("/upload/{0}/{1}.jpg", fileName, Guid.NewGuid().ToString());
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
