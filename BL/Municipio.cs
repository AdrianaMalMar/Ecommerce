using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Municipio
    {
        public static ML.Result MunicipioGetByIdEstado(int idEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Municipios.FromSqlRaw($"MunicipioGetByIdEstado {idEstado}").ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DL.Municipio rowMunicipio in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = rowMunicipio.IdMunicipio;
                            municipio.Nombre = rowMunicipio.Nombre;

                            municipio.Estado = new ML.Estado();
                            municipio.Estado.IdEstado = rowMunicipio.IdEstado;

                            result.Objects.Add(municipio);
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
