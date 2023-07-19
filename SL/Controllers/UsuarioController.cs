using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            ML.Result result = BL.Usuario.UsuarioGetAll(usuario);

            if(result.Correct)
            {
                return Ok(result); //HTTP 200
            }
            else
            {
                return NotFound(); //HTTP 404
            }

        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost]
        [Route("UsuarioAdd")]
        public IActionResult UsuarioAdd([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UsuarioAdd(usuario);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
