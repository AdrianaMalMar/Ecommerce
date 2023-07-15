using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Pais
    {
        public static ML.Result PaisGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Pais.FromSqlRaw("PaisGetAll").ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DL.Pai rowPais in query)
                        {
                            ML.Pais pais = new ML.Pais();

                            pais.IdPais = rowPais.IdPais;
                            pais.Nombre = rowPais.Nombre;

                            result.Objects.Add(pais);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
