using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Estado
    {
        public static ML.Result EstadoGetByIdPais(int idPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Estados.FromSqlRaw($"EstadoGetByIdPais {idPais}").ToList();
                    {
                        if (query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DL.Estado rowEstado in query)
                            {
                                ML.Estado estado = new ML.Estado();
                                estado.IdEstado = rowEstado.IdEstado;
                                estado.Nombre = rowEstado.Nombre;

                                estado.Pais = new ML.Pais();
                                estado.Pais.IdPais = rowEstado.IdPais;

                                result.Objects.Add(estado);
                            }
                            result.Correct = true;
                        }
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
