using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }

        public Departamento(int idDepartamento, string nombre, int idArea, string nombreArea)
        {
            IdDepartamento = idDepartamento;
            Nombre = nombre;
        }

        public Departamento()
        {

        }

        public string? Nombre { get; set; }
        public ML.Area? Area { get; set; }
        public List<object>? Departamentos { get; set; }
    }
}
