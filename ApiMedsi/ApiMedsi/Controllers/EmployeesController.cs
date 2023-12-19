using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMedsi.Models;
using System.Security.Cryptography;
using System.Text;

namespace ApiMedsi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly MedsiContext _context;

        public EmployeesController(MedsiContext context)
        {
            _context = context;
        }


        public static byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.IdEmployee)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            byte[] Salt = GenerateSalt(20);
            employee.Salt = Convert.ToBase64String(Salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(employee.PasswordEmp);
            byte[] hashedBytes = new Rfc2898DeriveBytes(passwordBytes, Salt, 10000).GetBytes(32);
            employee.PasswordEmp = Convert.ToBase64String(hashedBytes);
            if (_context.Employees == null)
          {
              return Problem("Entity set 'MedsiContext.Employees'  is null.");
          }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.IdEmployee }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        // AUTH: api/Users/5
        [HttpGet("{LoginUser}/{Password}")]
        public async Task<ActionResult<string>> Authorization(string LoginUser, string Password)
        {
            var Users = await _context.Employees.Where(u => u.LoginEmp == LoginUser).ToListAsync();

            if (Users.Count == 0)
            {
                // пользователь не найден
                return NotFound();
            }
            else if (Users.Count > 1)
            {
                // обнаружено несколько пользователей с таким именем
                return BadRequest("Multiple usernames detected");
            }

            var user = Users[0];
            // преобразовываем строку Salt в массив байтов
            byte[] saltBytes = Convert.FromBase64String(user.Salt);

            // преобразовываем строку Password в массив байтов
            byte[] passwordBytes = Encoding.UTF8.GetBytes(Password);

            // вычисляем хеш пароля с помощью соли и 10000 итераций
            byte[] hashBytes = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000).GetBytes(32);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            if (hashedPassword == user.PasswordEmp)
            {
                // пароль совпадает, генерируем случайный токен и добавляем его в базу данных
                string token;
                Token existingToken;

                do
                {
                    token = Guid.NewGuid().ToString();
                    existingToken = await _context.Tokens.FirstOrDefaultAsync(t => t.Token1 == token);
                }
                while (existingToken != null);

                // создаем новую запись Token и сохраняем ее в базу данных
                // создаем новую запись Token и сохраняем ее в базу данных
                Token tok = new Token();
                tok.Token1 = token;
                tok.TokenDatetime = DateTime.Now;
                _context.Tokens.Add(tok);
                await _context.SaveChangesAsync();

                return token;
            }
            else
            {
                // пароль не совпадает
                return BadRequest("Неправильный пароль");
            }
        }

        [HttpPut("Password_Change")]
        public async Task<IActionResult> PutUser(int id, string New_password)
        {
            var user = await _context.Employees.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            // хешируем новый пароль
            byte[] Salt = GenerateSalt(20);
            user.Salt = Convert.ToBase64String(Salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(New_password);
            byte[] hashBytes = new Rfc2898DeriveBytes(passwordBytes, Salt, 10000).GetBytes(32);
            user.PasswordEmp = Convert.ToBase64String(hashBytes);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: api/Users
        [HttpGet("auth_key")]
        public async Task<ActionResult<string>> GetAuthKey(string LoginUser)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(u => u.LoginEmp == LoginUser);
            if (user == null)
            {
                return NotFound($"Admin with login '{LoginUser}' was not found.");
            }
            else if (_context.Employees.Count(u => u.LoginEmp == LoginUser) > 1)
            {
                return BadRequest($"Multiple admins with login '{LoginUser}' were found.");
            }

            string salt = user.Salt;
            if (string.IsNullOrEmpty(salt))
            {
                return BadRequest($"Salt for user with login '{LoginUser}' is missing or empty.");
            }

            byte[] saltBytes = Encoding.UTF8.GetBytes(salt.Substring(0, Math.Min(salt.Length, 5)));
            byte[] reverseSalt = saltBytes.Reverse().ToArray();
            string hashedReverse = Convert.ToBase64String(reverseSalt);

            return hashedReverse;
        }


        // GET: api/Users
        [HttpGet("authentication")]
        public async Task<ActionResult<string>> GetAuthentication(string LoginUser, string AuthKey)
        {
            // Retrieve the user's password salt from the database
            var user = await _context.Employees.FirstOrDefaultAsync(u => u.LoginEmp == LoginUser);
            if (user == null)
            {
                return BadRequest("Invalid LoginUser");
            }
            var salt = user.Salt;

            // Compute the AuthKey from the password salt
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt.Substring(0, Math.Min(salt.Length, 5)));
            byte[] reverseSalt = saltBytes.Reverse().ToArray();
            string hashedReverse = Convert.ToBase64String(reverseSalt);

            // Check if the computed AuthKey matches the provided AuthKey
            if (hashedReverse != AuthKey)
            {
                return BadRequest("Invalid AuthKey");
            }

            // Generate a random token and add it to the database
            string token;
            Token existingToken;

            do
            {
                token = Guid.NewGuid().ToString();
                existingToken = await _context.Tokens.FirstOrDefaultAsync(t => t.Token1 == token);
            }
            while (existingToken != null);

            Token tok = new Token();
            tok.Token1 = token;
            tok.TokenDatetime = DateTime.Now;
            _context.Tokens.Add(tok);
            await _context.SaveChangesAsync();

            return token;


        }


        [HttpGet("GetRoleIdByLogin")]
        public async Task<ActionResult<int>> GetRoleIdByLogin(string login)
        {
            var employee = await _context.Employees
                .Where(e => e.LoginEmp == login)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound(); // Или другой статус код, который соответствует ситуации
            }

            return employee.RoleId;
        }


        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.IdEmployee == id)).GetValueOrDefault();
        }
    }
}
