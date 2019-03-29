using System.Web.Mvc;
using ZacharyKniebel.Project.Demo.Models.Demo;

namespace ZacharyKniebel.Project.Demo.Controllers
{
    public class DemoController : Controller
    {
        [HttpGet]
        public ActionResult DemoRendering()
        {
            // yes, this should be retrieved from a model generator factory and I should be injecting my Sitecore Context and so on...
            // ... this is just a demo
            var model = new DemoRenderingModel()
            {
                Title = Sitecore.Context.Item["Title"],
                Text = Sitecore.Context.Item["Text"]
            };

            return View("~/Views/Demo/DemoRendering.cshtml", model);
        }
    }
}