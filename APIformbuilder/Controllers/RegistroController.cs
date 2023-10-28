using APIformbuilder.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
namespace APIformbuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly string cadenaSQL;
        public RegistroController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }


        [HttpPost]
        [Route("nuevoUsuario")]
        public IActionResult Registro([FromBody] registroUsuario nuevoUsuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Establece el rol como "Administrador"
                    nuevoUsuario.RoleID = (Roles) 1;

                    using (SqlConnection connection = new SqlConnection(cadenaSQL))
                    {
                        connection.Open();

                        // Verificar si el nombre de usuario ya existe en la base de datos
                        string userCheckQuery = "SELECT COUNT(*) FROM Usuarios WHERE Username = @Username";
                        SqlCommand userCheckCommand = new SqlCommand(userCheckQuery, connection);
                        userCheckCommand.Parameters.AddWithValue("@Username", nuevoUsuario.Username);

                        int existingUsers = (int)userCheckCommand.ExecuteScalar();
                        if (existingUsers > 0)
                        {
                            return BadRequest("El nombre de usuario ya está en uso.");
                        }

                        // Hash y salting de la contraseña (debes implementarlo de manera segura)
                        // Aquí asumimos que tienes una función de hash y salting llamada HashPassword
                        nuevoUsuario.Password = HashPassword(nuevoUsuario.Password);

                        // Insertar el nuevo usuario en la base de datos
                        string insertQuery = "INSERT INTO Usuarios (Username, Password, RoleID) " +
                                "VALUES (@Username, @Password, @RoleID)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@Username", nuevoUsuario.Username);
                        insertCommand.Parameters.AddWithValue("@Password", nuevoUsuario.Password);
                        insertCommand.Parameters.AddWithValue("@RoleID", nuevoUsuario.RoleID);

                        insertCommand.ExecuteNonQuery();

                        return Ok(new { mensaje = "Registro exitoso" });
                    }
                }
                else
                {
                    // Si la validación falla, se devuelven los errores de validación
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Función de hash y salting de la contraseña (debes implementarla de manera segura)
        private string HashPassword(string password)
        {
            // Implementa la lógica para hash y salting de la contraseña aquí
            // No almacenes contraseñas en texto plano
            throw new NotImplementedException();
        }
    }
}
