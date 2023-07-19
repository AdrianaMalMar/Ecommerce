using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class UsuarioCargaMasivaController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;

        public UsuarioCargaMasivaController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        public IActionResult GetCargaMasiva()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        [HttpPost]
        public IActionResult PostCargarMasiva(IFormFile file)
        {
            //valida que el documento venga nullo o en caso de haber un archivo leera la longitud del archivo
            if (file == null || file.Length <= 0)
            {
                return RedirectToAction("GetCargaMasiva");
            }
            //usamos using para que el metodo que se ejecuta se destruya y no utilice memoria al momento de terminar la ejecucion
            //leera un archivo
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                //leera la primera linea que contenga mi bloc de notas
                string line = reader.ReadLine();
                //en el while leera cada linea que contenga el archivo
                while (!reader.EndOfStream)
                {
                    //leera la linea siguiente
                    line = reader.ReadLine();
                    //el split permite quitar "|" o algun caracter que queramos
                    var values = line.Split('|');
                    //instanciamos
                    ML.Usuario usuario = new ML.Usuario();

                    usuario.UserName = values[0];
                    usuario.Nombre = values[1];
                    usuario.ApellidoPaterno = values[2];
                    usuario.ApellidoMaterno = values[3];
                    usuario.Email = values[4];
                    usuario.Contrasena = values[5];
                    usuario.FechaNacimiento = values[6];
                    usuario.Sexo = values[7];
                    usuario.Telefono = values[8];
                    usuario.Celular = values[9];
                    usuario.Curp = values[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = int.Parse(values[11]);

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = values[12];
                    usuario.Direccion.NumeroInterior = values[13];
                    usuario.Direccion.NumeroExterior = values[14];

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = int.Parse(values[15]);

                    ML.Result result = BL.Usuario.UsuarioAdd(usuario);

                    //validamos si sale bien o mal
                    if (result.Correct)
                    {
                        result.Objects = new List<object>();
                        foreach (ML.Usuario row in result.Objects)
                        {
                            usuario.UserName = row.UserName;
                            usuario.Nombre = row.Nombre;
                            usuario.ApellidoPaterno = row.ApellidoPaterno;
                            usuario.ApellidoMaterno = row.ApellidoMaterno;
                            usuario.Email = row.Email;
                            usuario.Contrasena = row.Contrasena;
                            usuario.FechaNacimiento = row.FechaNacimiento;
                            usuario.Sexo = row.Sexo;
                            usuario.Telefono = row.Telefono;
                            usuario.Celular = row.Celular;
                            usuario.Curp = row.Curp;
                            usuario.Rol.IdRol = row.Rol.IdRol;
                            usuario.Direccion.Calle = row.Direccion.Calle;
                            usuario.Direccion.NumeroInterior = row.Direccion.NumeroInterior;
                            usuario.Direccion.NumeroExterior = row.Direccion.NumeroExterior;
                            usuario.Direccion.Colonia.IdColonia = row.Direccion.Colonia.IdColonia;

                        }
                    }
                    else
                    {

                    }
                }
            }
            return RedirectToAction("GetCargaMasiva");
        }

        [HttpPost]
        public IActionResult GetCargaMasiva(int? valor)
        {
            IFormFile file = Request.Form.Files["file"];
            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extensionAceptada = configuration["TipoExcel"];
                    string folderPath = configuration["PathFolder:ruta"];
                    if (extensionArchivo == extensionAceptada)
                    {
                        string filePath = Path.Combine(environment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            string connString = configuration["ExcelConString:value"] + filePath;

                            ML.Result resultExcelDt = BL.Usuario.ConvertExcelToDataTable(connString);

                            if (resultExcelDt.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultExcelDt.Objects);

                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }
                                return View(resultValidacion);
                            }
                        }
                        else
                        {
                            ViewBag.Message = "El archivo que se intenta procesar no es un excel";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "No se ha insertado un archivo";
                    }
                    return View();
                }
                return View();
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = configuration["ExcelConString:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertExcelToDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();
                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {
                        ML.Result resultAdd = BL.Usuario.UsuarioAdd(usuarioItem);
                        
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se inserto el usuario con username: " + usuarioItem.UserName + "Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {
                        string fileError = Path.Combine(environment.WebRootPath, @"C:\Users\digis\Documents\Adriana_Maldonado_Martinez\AMaldonadoProgramacionNCapasCore\PL\wwwroot\ErroresTXT\Errores_Usuario.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los usuarios no han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los usuarios se registraron correctamente";
                    }
                }
                return View("GetCargaMasiva");
            }
        }
    }
}
