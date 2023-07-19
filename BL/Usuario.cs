using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.OleDb;
using System.Data;

namespace BL
{
    public class Usuario
    {
        public static ML.Result UsuarioAdd(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}','{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}','{usuario.Email}','{usuario.Contrasena}','{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.Telefono}','{usuario.Celular}','{usuario.Curp}','{usuario.Imagen}',{usuario.Rol.IdRol},'{usuario.Direccion.Calle}',{usuario.Direccion.NumeroInterior},{usuario.Direccion.NumeroExterior},{usuario.Direccion.Colonia.IdColonia}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Message = "Se registro correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error" + ex.Message;
            }
            return result;
        }

        public static ML.Result UsuarioUpdate(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.UserName}','{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}','{usuario.Email}','{usuario.Contrasena}','{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.Telefono}','{usuario.Celular}','{usuario.Curp}',{usuario.Rol.IdRol}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Message = "Se actualizo correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error" + ex.Message;
            }
            return result;
        }

        public static ML.Result UsuarioDelete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var queryResult = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");
                    if (queryResult >= 1)
                    {
                        result.Correct = true;
                        result.Message = "El registro " + usuario.IdUsuario + " se elimino correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar" + ex;
            }
            return result;
        }

        public static ML.Result UsuarioGetAll(ML.Usuario usuarios)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuarios.Nombre}','{usuarios.ApellidoPaterno}'").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (DL.Usuario row in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = row.IdUsuario;
                            usuario.UserName = row.UserName;
                            usuario.Nombre = row.Nombre;
                            usuario.ApellidoPaterno = row.ApellidoPaterno;
                            usuario.ApellidoMaterno = row.ApellidoMaterno;
                            usuario.Email = row.Email;
                            usuario.Contrasena = row.Contrasena;
                            usuario.FechaNacimiento = row.FechaNacimiento.ToString("dd/MM/yyyy");
                            usuario.Sexo = row.Sexo;
                            usuario.Telefono = row.Telefono;
                            usuario.Celular = row.Celular;
                            usuario.Curp = row.Curp;
                            usuario.Imagen = row.Imagen;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = row.IdRol.Value;
                            usuario.Rol.Nombre = row.NombreRol;
                            usuario.Status = row.Status.Value;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error" + ex.Message;
            }
            return result;
        }

        public static ML.Result ConvertExcelToDataTable(string connString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Hoja1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();
                        da.Fill(tableUsuario);

                        if(tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach(DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Contrasena = row[5].ToString();
                                usuario.FechaNacimiento = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.Curp = row[10].ToString();
                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = int.Parse(row[11].ToString());
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();
                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = int.Parse(row[15].ToString());

                                result.Objects.Add(usuario);
                            }
                            result.Correct=true;
                        }
                        result.Object = tableUsuario;

                        if(tableUsuario.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct=false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result ValidarExcel(List<object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {
                result.Objects = new List<object>();
                int i =1;
                foreach(ML.Usuario usuario in Objects)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    //creamos un operador ternario
                    usuario.UserName = (usuario.UserName == "") ? error.Message += "Ingresar el username " : usuario.UserName;
                    usuario.Nombre = (usuario.Nombre == "") ? error.Message += "Ingrese el nombre " : usuario.Nombre;
                    usuario.ApellidoPaterno = (usuario.ApellidoPaterno == "") ? error.Message += "Ingrese el apellido paterno: " : usuario.ApellidoPaterno;
                    usuario.ApellidoMaterno = (usuario.ApellidoMaterno == "") ? error.Message += "Ingrese el apellido materno: " : usuario.ApellidoMaterno;
                    usuario.Email = (usuario.Email == "") ? error.Message += "Ingrese el email: " : usuario.Email;
                    usuario.Contrasena = (usuario.Contrasena == "") ? error.Message += "Ingrese la contraseña: " : usuario.Contrasena;
                    usuario.FechaNacimiento = (usuario.FechaNacimiento == "") ? error.Message += "Ingrese la fecha de nacimiento: " : usuario.FechaNacimiento;
                    usuario.Sexo = (usuario.Sexo == "") ? error.Message += "Ingrese el género" : usuario.Sexo;
                    usuario.Telefono = (usuario.Telefono == "") ? error.Message += "Ingrese el teléfono: " : usuario.Telefono;
                    usuario.Celular = (usuario.Celular == "") ? error.Message += "Ingrese el celular: " : usuario.Celular;
                    usuario.Curp = (usuario.Curp == "") ? error.Message += "Ingrese un CURP: " : usuario.Curp;
                    //usuario.Rol = new ML.Rol();
                    //usuario.Rol.IdRol = (usuario.Rol.IdRol == "") ? error.Message += "Ingrese id de rol: " : usuario.Rol.IdRol;

                    if(error.Message != null)
                    {
                        result.Objects.Add(error);
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }
            return result;
        }


        public static ML.Result UsuarioGetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Contrasena = query.Contrasena;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString();
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.Curp = query.Curp;
                        usuario.Imagen = query.Imagen;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        usuario.Rol.Nombre = query.NombreRol;
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Calle = usuario.Direccion.Calle;
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Nombre = query.NombreColonia;
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.NombrePais;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error" + ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateStatus(bool status, int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioUpdateStatus {status}, {idUsuario}");

                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error" + ex.Message;
            }
            return result;
        }
    }
}
