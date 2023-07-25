using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public Usuario(int idUsuario, string Username, string Password)
        {
            IdUsuario = idUsuario;
            UserName = Username;
            Contrasena = Password;
        }

        public Usuario()
        {

        }

        [Required(ErrorMessage = "No puede ir vacio")]
        [StringLength(50)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        [RegularExpression(@"^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$", ErrorMessage = "Solo letras"), MinLength(3)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        [RegularExpression(@"^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$", ErrorMessage = "Solo letras"), MinLength(3)]
        public string? ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        [RegularExpression(@"^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$", ErrorMessage = "Solo letras"), MinLength(3)]
        public string? ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email incorrecto")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        [RegularExpression(@"(^[A-Za-z\d$@$!%*?&]+$)", ErrorMessage = "Faltan datos"), MinLength(6)]
        public string? Contrasena { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        [RegularExpression(@"^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})$", ErrorMessage = "No esta bien la fecha")]
        public string? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        public string? Sexo { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese solo números")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese solo números")]
        public string? Celular { get; set; }

        [Required(ErrorMessage = "No puede ir vacio")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "Faltan datos")]
        public string? Curp { get; set; }
        public string? Imagen { get; set; }
        public ML.Rol? Rol { get; set; }
        public bool? Status { get; set; }
        public ML.Direccion? Direccion { get; set; }
        public List<object>? Usuarios { get; set; }
        
    }
}
