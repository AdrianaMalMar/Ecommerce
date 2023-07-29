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
    public class Departamento
    {
        public static ML.Result DepartamentoGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw("DepartamentoGetAll").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var row in query)
                        {
                            //ML.Departamento departamento = new ML.Departamento(row.IdDepartamento, row.Nombre, row.IdArea, row.NombreArea);
                            ML.Departamento departamento = new ML.Departamento(row.IdDepartamento, row.Nombre, row.IdArea);
                            //departamento.Area = new ML.Area(row.IdArea, row.NombreArea);
                            //ML.Departamento departamento = new ML.Departamento();
                            //departamento.IdDepartamento = row.IdDepartamento;
                            //departamento.Nombre = row.Nombre;
                            //departamento.Area.IdArea = row.IdArea;
                            //departamento.Area.Nombre = row.NombreArea;

                            result.Objects.Add(departamento);
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


        public static ML.Result DepartamentoGetByIdArea(int IdArea)
        {
            ML.Result result= new ML.Result();
            try
            {
                using(DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetByIdArea {IdArea}").ToList();
                    if( query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach(DL.Departamento row in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = row.IdDepartamento;
                            departamento.Nombre = row.Nombre;

                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = row.IdArea;
                            //departamento.Area.Nombre = row.NombreArea;

                            result.Objects.Add(departamento);

                        }
                        result.Correct = true;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex=ex;
                result.Message = "Ocurrio un error" + ex.Message;
            }
            return result;
        }
    }
}
