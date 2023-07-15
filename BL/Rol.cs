using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Rol
    {
        public static ML.Result RolGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Rols.FromSqlRaw("RolGetAll").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var rowRol in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = rowRol.IdRol;
                            rol.Nombre = rowRol.Nombre;

                            result.Objects.Add(rol);
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
    }
}
