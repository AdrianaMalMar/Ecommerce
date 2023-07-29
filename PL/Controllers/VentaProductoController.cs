using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
    {
        public ActionResult GetAll()
        {
            return View();
        }
    }
}
