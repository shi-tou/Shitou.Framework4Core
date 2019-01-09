using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shitou.Framework.Demo.Model;
using Shitou.Framework.Demo.Service;
using Shitou.Framework.Demo.WebApp.Models;

namespace Shitou.Framework.Demo.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IGoodsService GoodsService { get; set; }
        public IConfiguration Configuration { get; }
        public HomeController(IGoodsService goodsService, IConfiguration configuration)
        {
            GoodsService = goodsService;
            Configuration = configuration;
        }


        public IActionResult Index()
        {
            List<GoodsInfo> goodList = GoodsService.GetList<GoodsInfo>();
            ViewBag.ImageServerUrl = Configuration.GetValue<string>("ImageServerUrl");
            return View(goodList);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
