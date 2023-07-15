using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Colonia
    {
        public static ML.Result ColoniaGetByIdMunicipio(int idMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio {idMunicipio}").ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DL.Colonium rowColonia in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = rowColonia.IdColonia;
                            colonia.Nombre = rowColonia.Nombre;
                            colonia.CodigoPostal = rowColonia.CodigoPostal;

                            colonia.Municipio = new ML.Municipio();
                            colonia.Municipio.IdMunicipio = rowColonia.IdMunicipio;

                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Ex = ex;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
