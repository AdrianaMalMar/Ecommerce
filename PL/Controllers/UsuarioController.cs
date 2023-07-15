using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            ML.Result result = BL.Usuario.UsuarioGetAll(usuario);
            
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(usuario);
            }
        }

        [HttpPost]
        public IActionResult GetAll(ML.Usuario usuarios)
        {
            ML.Result result = BL.Usuario.UsuarioGetAll(usuarios);
            ML.Result resultRol = BL.Rol.RolGetAll();
            ML.Result resultPais = BL.Pais.PaisGetAll();

            ML.Usuario usuario = new ML.Usuario();
            usuarios.Rol = new ML.Rol();
            usuarios.Direccion = new ML.Direccion();
            usuarios.Direccion.Colonia = new ML.Colonia();
            usuarios.Direccion.Colonia.Municipio = new ML.Municipio();
            usuarios.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuarios.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            usuarios.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
            usuarios.Rol.Roles = resultRol.Objects;

            if (result.Correct)
            {
                usuarios.Usuarios = result.Objects;
                return View(usuarios);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(usuarios);
            }
        }

        [HttpGet]
        public IActionResult Form(int? IdUsuario)
        {
            ML.Result resultRol = BL.Rol.RolGetAll();
            ML.Result resultPais = BL.Pais.PaisGetAll();

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
            usuario.Rol.Roles = resultRol.Objects;

            if (IdUsuario == null)
            {
                ViewBag.Titulo = "Agregar nuevo usuario";
                ViewBag.Accion = "Agregar";

                ML.Result result = BL.Usuario.UsuarioGetAll(usuario);

                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ML.Result result = BL.Usuario.UsuarioGetById(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultRol.Objects;
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                    ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultEstado.Objects;

                    ViewBag.Titulo = "Actualizar";
                    ViewBag.Accion = "Actualizar";
                    return View(usuario);
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Message = result.Message;
                    return PartialView("Modal");
                }
            }
        }

        [HttpPost]
        public IActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid) 
            {
                IFormFile image = Request.Form.Files["fileImage"];

                //validamos que no este nulo
                if (image != null)
                {
                    byte[] ImagenBytes = ConvertToBytes(image);

                    usuario.Imagen = Convert.ToBase64String(ImagenBytes);
                }
                
                if (usuario.IdUsuario == 0)
                {
                    var result = BL.Usuario.UsuarioAdd(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Message = "El usuario se agrego correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ML.Result resultRol = BL.Rol.RolGetAll();
                        ML.Result resultPais = BL.Pais.PaisGetAll();

                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                        usuario.Rol.Roles = resultRol.Objects;
                        ViewBag.Message = result.Message;
                        return View(usuario);
                    }
                }
                else
                {
                    var result = BL.Usuario.UsuarioUpdate(usuario);
                    ML.Result resultRol = BL.Rol.RolGetAll();
                    ML.Result resultPais = BL.Pais.PaisGetAll();

                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                    usuario.Rol.Roles = resultRol.Objects;

                    if (result.Correct)
                    {
                        ViewBag.Message = "El usuario se actualizo correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = result.Message;
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                ML.Result resultRol = BL.Rol.RolGetAll();
                ML.Result resultPais = BL.Pais.PaisGetAll();

                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                usuario.Rol.Roles = resultRol.Objects;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = Convert.ToInt32(usuario.IdUsuario);
            var result = BL.Usuario.UsuarioDelete(usuario);

            if (result.Correct)
            {
                ViewBag.Message = "El usuario se elimino correctamente";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "Error al eliminar";
                return PartialView("Modal");
            }
        }


        [HttpGet]
        public JsonResult GetEstado(int idPais)
        {
            ML.Result result = BL.Estado.EstadoGetByIdPais(idPais);
            return Json(result.Objects);
        }

        [HttpGet]
        public JsonResult GetMunicipio(int idEstado)
        {
            ML.Result result = BL.Municipio.MunicipioGetByIdEstado(idEstado);
            return Json(result.Objects);
        }

        [HttpGet]
        public JsonResult GetColonia(int idMunicipio)
        {
            ML.Result result = BL.Colonia.ColoniaGetByIdMunicipio(idMunicipio);
            return Json(result.Objects);
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        [HttpPost]
        public JsonResult CambiarStatus(bool status, int idUsuario)
        {
            ML.Result result = BL.Usuario.UpdateStatus(status, idUsuario);
            return Json(result);
        }

    }
}
