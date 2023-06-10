using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

public class HomeController : Controller
{
        [Route("/")]
        public IActionResult Index()
        {
        var info = new 
        {
            OS = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
            ProcessorCount = Environment.ProcessorCount,
            WorkingSet = Environment.WorkingSet,
            MachineName = Environment.MachineName,
            SystemPageSize = Environment.SystemPageSize,
            Version = Environment.Version.ToString(), 
            NewLine = Environment.NewLine 
        };
            return Ok(info);
        }
}
