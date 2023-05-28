using ASP.NETCore_TrainingApp.Models;
using ASP.NETCore_TrainingApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.NETCore_TrainingApp.Controllers
{
    public class CloudDatabaseController : Controller
    {
        private readonly CloudDatabaseService _cloudDatabaseService;

        public CloudDatabaseController(CloudDatabaseService cloudDatabaseService)
        {
            _cloudDatabaseService = cloudDatabaseService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CloudDatabaseModel model)
        {
           // await _cloudDatabaseService.InsertData(model);
            return RedirectToAction("Index");
        }

        public void Connect()
        {
            _cloudDatabaseService.ConnectDB();
            //data = await _cloudDatabaseService.GetData();
            //return Json(data);
            
        }
    }
}
