using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            ML.Result result = BL.Usuario.UsernameGetById(Username);
            if (result.Object != null)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (Password == usuario.Contrasena)
                {
                    return RedirectToAction("GetAll", "Usuario");
                }
                else
                {
                    ViewBag.Message = "Contraseña incorrecta";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "Usuario incorrecto";
                return PartialView("Modal");
            }
        }
    }
}
