using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(ZacharyKniebel.Project.Demo.RazorGeneratorMvcStart), "Start")]

namespace ZacharyKniebel.Project.Demo {
    public static class RazorGeneratorMvcStart {
        public static void Start() {
            var engine = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly) {
                UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
            };

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}
