using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
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

        [HttpGet]
        public ActionResult CartPost(ML.Producto producto)
        {
            bool existe = false;
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ventaProducto.VentaProductos = new List<object>();

            if (HttpContext.Session.GetString("Producto") == null)
            {
                ventaProducto.Cantidad = ventaProducto.Cantidad = 1;
                //ventaProducto.Subtotal = producto.Costo * producto.Cantidad;
                ventaProducto.VentaProductos.Add(producto);
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentaProductos));
                var session = HttpContext.Session.GetString("Producto");
            }
            else
            {
                GetCarrito(ventaProducto);
                foreach (ML.Producto venta in ventaProducto.VentaProductos.ToList())
                {
                    if (producto.IdProducto == venta.IdProducto)
                    {
                        ventaProducto.Cantidad = ventaProducto.Cantidad + 1;   //True
                        //venta.Subtotal = venta.Costo * venta.Cantidad;
                        existe = true;
                    }
                    else
                    {
                        existe = false;
                    }
                    if (existe == true)
                    {
                        break;
                    }
                }
                if (existe == false)
                {
                    ventaProducto.Cantidad = ventaProducto.Cantidad = 1;
                    //producto.Subtotal = producto.Cantidad * producto.Costo;
                    ventaProducto.VentaProductos.Add(producto);
                }
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentaProductos));
            }
            if (HttpContext.Session.GetString("Producto") != null)
            {
                ViewBag.Message = "Se ha agregado el producto a tu carrito!";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se pudo agregar tu producto ):";
                return PartialView("Modal");
            }

        }

        [HttpGet]
        public ActionResult ResumenCompra(ML.VentaProducto ventaProducto)
        {
            decimal costoTotal = 0;
            if (HttpContext.Session.GetString("Producto") == null)
            {
                return View();
            }
            else
            {
                ventaProducto.VentaProductos = new List<object>();
                GetCarrito(ventaProducto);
                //ventaMateria.Total = costoTotal;
            }

            return View(ventaProducto);
        }
        public ML.VentaProducto GetCarrito(ML.VentaProducto ventaProducto)
        {
            var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));

            foreach (var obj in ventaSession)
            {
                ML.Producto objMateria = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                ventaProducto.VentaProductos.Add(objMateria);
            }
            return ventaProducto;
        }
    }
}
