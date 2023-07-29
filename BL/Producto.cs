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
    public class Producto
    {
       public static ML.Result ProductoGetAll(ML.Producto productos)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.AmaldonadoProgramacionNcapasContext context = new DL.AmaldonadoProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetAll '{productos.Departamento.Area.Nombre}', '{productos.Departamento.Nombre}'").ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (DL.Producto row in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = row.IdProducto;
                            producto.Nombre = row.Nombre;
                            producto.PrecioUnitario = row.PrecioUnitario;
                            producto.Stock = row.Stock;
                            producto.Descripcion = row.Descripcion;
                            
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = row.IdDepartamento;
                            producto.Departamento.Nombre = row.NombreDepartamento;
                            producto.Imagen = row.Imagen;

                            producto.Departamento.Area = new ML.Area();
                            producto.Departamento.Area.IdArea = row.IdArea;
                            producto.Departamento.Area.Nombre = row.NombreArea;
                            //producto.Proveedor = new ML.Proveedor();
                            //producto.Proveedor.IdProveedor = row.IdProveedor;
                            //producto.Proveedor.Telefono = row.Telefono;

                            result.Objects.Add(producto);
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
