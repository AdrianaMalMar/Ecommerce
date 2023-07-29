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

        //[HttpGet]
        //public ActionResult Form(int? IdProducto)
        //{
        //    ML.Result resultArea = BL.Area.AreaGetAll();

        //    ML.Producto producto = new ML.Producto();
        //    producto.Departamento = new ML.Departamento();
        //    producto.Area = new ML.Area();

        //    producto.Departamento.Area.Areas = resultArea.Objects;

        //    if (IdProducto == null)
        //    {
        //        ViewBag.Titulo = "Agregar nuevo producto";
        //        ViewBag.Accion = "Agregar";

        //        ML.Result result = ;

        //        producto.Productos = result.Objects;
        //        return View(producto);
        //    }
        //    else
        //    {
        //        ML.Result result = BL.Usuario.UsuarioGetById(IdProducto.Value);

        //        if (result.Correct)
        //        {
        //            usuario = (ML.Usuario)result.Object;
        //            usuario.Rol.Roles = resultRol.Objects;
        //            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

        //            ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
        //            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
        //            ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
        //            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
        //            ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
        //            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

        //            ViewBag.Titulo = "Actualizar";
        //            ViewBag.Accion = "Actualizar";
        //            return View(usuario);
        //        }
        //        else
        //        {
        //            ViewBag.Titulo = "Error";
        //            ViewBag.Message = result.Message;
        //            return PartialView("Modal");
        //        }
        //    }
        //}



        [HttpGet]
        public JsonResult GetDepartamento(int idArea)
        {
            ML.Result result = BL.Departamento.DepartamentoGetByIdArea(idArea);
            return Json(result.Objects);
        }
    }
}
