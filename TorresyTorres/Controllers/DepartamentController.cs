using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TorresyTorres.Database;
using TorresyTorres.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TorresyTorres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentController : ControllerBase
    {
        Conection con = new Conection();
        DepartamentModel departament = new DepartamentModel();

        // GET: api/<DepartamentController>
        [HttpGet]
        public List<DepartamentModel> Get()
        {
            var db = con.GetSqlConnection();

            var query = @"SELECT * FROM departamentos";
            var command = new MySqlCommand(query, db);
            var reader = command.ExecuteReader();

            List<DepartamentModel> departaments = new List<DepartamentModel>();

            while (reader.Read())
            {
                var departmentModel = new DepartamentModel();
                departmentModel.Id = reader.GetInt32("id");
                departmentModel.Codigo = reader.GetString("codigo");
                departmentModel.Nombre = reader.GetString("nombre");
                departmentModel.Activo = reader.GetBoolean("activo");
                departmentModel.IdUsuarioCreacion = reader.GetInt32("idUsuarioCreacion");

                departaments.Add(departmentModel);

            }
            return departaments;
        }

        // GET api/<DepartamentController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var db = con.GetSqlConnection();

            var query = $"SELECT * FROM departamentos WHERE id = {id}";
            var command = new MySqlCommand(query, db);
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var departmentModel = new DepartamentModel();
                departmentModel.Id = reader.GetInt32("id");
                departmentModel.Codigo = reader.GetString("codigo");
                departmentModel.Nombre = reader.GetString("nombre");
                departmentModel.Activo = reader.GetBoolean("activo");
                departmentModel.IdUsuarioCreacion = reader.GetInt32("idUsuarioCreacion");
                return Ok(new
                {
                    success = 1,
                    message = "Departamento existente",
                    department = departmentModel
                });
            }
            else
            {
                return BadRequest(new
                {
                    success = 0,
                    message = "No se pudo encontrar el departamento, por favor vuelve a intentarlo"
                });
            }
        }

        // POST api/<DepartamentController>
        [HttpPost]
        public ActionResult Post([FromBody] DepartamentModel departament)
        {
            var db = con.GetSqlConnection();

            var query = $"insert into departamentos(codigo, nombre, activo, idUsuarioCreacion) " +
                $"values('{departament.Codigo}', '{departament.Nombre}', '{departament.Activo}', '{departament.IdUsuarioCreacion}')";
            var Command = new MySqlCommand(query, db);
            if (Command.ExecuteNonQuery() > 0)
            {
                return Ok(new
                {
                    success = 1,
                    message = "Departamento creado correctamente"
                });
            }
            else
            {
                return BadRequest(new
                {
                    success = 0,
                    message = "No se pudo guardar el departamento, por favor vuelve a intentarlo"
                });
            }

        }

        // PUT api/<DepartamentController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (ExistDepartament(id))
            {
                var db = con.GetSqlConnection();

                var query = $"UPDATE departamentos SET " +
                    $"codigo = '{departament.Codigo}', " +
                    $"nombre = '{departament.Nombre}', " +
                    $"activo = '{departament.Activo}', " +
                    $"idUsuarioCreacion = '{departament.IdUsuarioCreacion}' " +
                    $"WHERE id = '{id}'";
                var Command = new MySqlCommand(query, db);
                if (Command.ExecuteNonQuery() > 0)
                {
                    return Ok(new
                    {
                        success = 1,
                        message = "Departamento editado correctamente"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = 0,
                        message = "No se pudo editar el departamento, por favor vuelve a intentarlo"
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    success = 0,
                    message = "Departamento no existente, por favor verifique la información"
                });
            }
        }

        // DELETE api/<DepartamentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private bool ExistDepartament(int id)
        {
            var db = con.GetSqlConnection();

            var query = $"SELECT * FROM departamentos WHERE id = {id}";
            var command = new MySqlCommand(query, db);
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return true;
            }
            return false;
        }
    }
}
