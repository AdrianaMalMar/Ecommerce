using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Area
    {
        public static ML.Result AreaGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Areas.FromSqlRaw("AreaGetAll").ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var rowArea in query)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = rowArea.IdArea;
                            area.Nombre = rowArea.Nombre;

                            result.Objects.Add(area);
                        }
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error" + ex.Message;
            }
            return result;
        }
    }
}
