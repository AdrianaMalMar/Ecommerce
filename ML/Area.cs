using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Area
    {
        public int IdArea { get; set; }
        public string? Nombre { get; set; }

        public Area(int idArea, string Nombre)
        {
            IdArea = idArea;
            Nombre = Nombre;
        }
        public Area()
        {

        }
        public List<object>? Areas { get; set; }
    }
}
