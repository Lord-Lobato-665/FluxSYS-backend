using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FluxSYS_backend.Application.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Genera un token JWT para el usuario
        public async Task<string> GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear claims con la información del usuario relevante
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id_user.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Mail_user),
                new Claim(ClaimTypes.Role, user.Roles?.Name_role ?? ""), // Asegúrate de no tener nulos
                new Claim("Name", user.Name_user),
                new Claim("Department", user.Departments?.Name_deparment ?? ""),
                new Claim("Position", user.Positions?.Name_position ?? ""),
                new Claim("Company", user.Companies?.Name_company ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // Autentica al usuario y devuelve el objeto Users si las credenciales son válidas
        public async Task<Users> Authenticate(string email, string password)
        {
            // Cargar el usuario con todas las entidades relacionadas
            var user = await _context.Users
                .Include(u => u.Roles)
                .Include(u => u.Positions)
                .Include(u => u.Departments)
                .Include(u => u.Companies)
                .FirstOrDefaultAsync(u => u.Mail_user == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password_user))
                return null;

            return user;
        }

        // Guarda el token en la base de datos
        public async Task SaveToken(int userId, string token, DateTime expirationDate)
        {
            var userToken = new UserToken
            {
                UserId = userId,
                Token = token,
                ExpirationDate = expirationDate
            };

            _context.UserTokens.Add(userToken);
            await _context.SaveChangesAsync();
        }

        // Elimina el token de la base de datos (logout)
        public async Task RemoveToken(string token)
        {
            var userToken = await _context.UserTokens.FirstOrDefaultAsync(ut => ut.Token == token);
            if (userToken != null)
            {
                _context.UserTokens.Remove(userToken);
                await _context.SaveChangesAsync();
            }
        }

        // Valida si el token existe y no ha expirado
        public async Task<UserTokenDto> ValidateToken(string token)
        {
            var userToken = await _context.UserTokens
                .Where(ut => ut.Token == token && ut.ExpirationDate > DateTime.Now)
                .Select(ut => new UserTokenDto
                {
                    Token = ut.Token,
                    ExpirationDate = ut.ExpirationDate,
                    User = new UserDto
                    {
                        Id_user = ut.User.Id_user,
                        Name_user = ut.User.Name_user,
                        Mail_user = ut.User.Mail_user,
                        Phone_user = ut.User.Phone_user,
                        Role = new RoleDto
                        {
                            Id_role = ut.User.Roles.Id_role,
                            Name_role = ut.User.Roles.Name_role
                        },
                        Position = new PositionDto
                        {
                            Id_position = ut.User.Positions.Id_position,
                            Name_position = ut.User.Positions.Name_position
                        },
                        Department = new DepartmentDto
                        {
                            Id_department = ut.User.Departments.Id_department,
                            Name_department = ut.User.Departments.Name_deparment
                        },
                        Company = new CompanyDto
                        {
                            Id_company = ut.User.Companies.Id_company,
                            Name_company = ut.User.Companies.Name_company
                        }
                    }
                })
                .FirstOrDefaultAsync();

            return userToken;
        }
    }
}