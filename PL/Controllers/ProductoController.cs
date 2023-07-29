using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            //ML.Result resultArea = BL.Area.AreaGetAll();
            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();
            ML.Result result = BL.Producto.ProductoGetAll(producto);
            ML.Result resultDepartamento = BL.Departamento.DepartamentoGetAll();
            ML.Result resultArea = BL.Area.AreaGetAll();

            producto.Departamento.Departamentos = resultDepartamento.Objects;
            producto.Departamento.Area.Areas = resultArea.Objects; 

            //producto.Departamento.Area.Nombre = "";

            //producto.Departamento.Nombre = "";


            if (result.Correct)
            {
                producto.Productos = result.Objects;
                return View(producto);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(producto);
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Producto productos)
        {
            ML.Result result = BL.Producto.ProductoGetAll(productos);
            ML.Area areas = new ML.Area();
            ML.Result resultArea = BL.Area.AreaGetAll();

            ML.Producto producto = new ML.Producto();
            productos.Departamento = new ML.Departamento();
            productos.Departamento.Area = new ML.Area();

            productos.Departamento.Area.Areas = resultArea.Objects;

            if (result.Correct)
            {
                productos.Productos = result.Objects;
                return View(productos);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(productos);
            }
        }



        [HttpGet]
        public JsonResult GetDepartamento(int idArea)
        {
            ML.Result result = BL.Departamento.DepartamentoGetByIdArea(idArea);
            return Json(result.Objects);
        }
    }
}
