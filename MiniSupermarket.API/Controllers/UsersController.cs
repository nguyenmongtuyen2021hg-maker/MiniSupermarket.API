using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using MiniSupermarket.API.Security;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;
        private readonly SessionTokenStore _tokens;

        public UsersController(SupermarketDbContext context, SessionTokenStore tokens)
        {
            _context = context;
            _tokens = tokens;
        }

        [HttpGet("health")]
        [AllowAnonymous]
        public IActionResult Health() => Ok();

        // 1. GET: Lấy danh sách tài khoản (/api/users)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.AsNoTracking()
                .OrderBy(user => user.Username)
                .Select(user => new UserSummary(user.Id, user.Username, user.FullName, user.Role, user.IsActive))
                .ToListAsync();
            return Ok(users);
        }

        // 2. POST: Thêm mới tài khoản (/api/users)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!new[] { "Admin", "Cashier", "Warehouse" }.Contains(user.Role, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "Vai trò chỉ được là Admin, Cashier hoặc Warehouse." });
            }

            var exists = await _context.Users.AnyAsync(u => u.Username.ToLower() == user.Username.Trim().ToLower());
            if (exists)
            {
                return BadRequest(new { message = "Tên đăng nhập đã tồn tại trong hệ thống!" });
            }

            user.Username = user.Username.Trim();
            user.FullName = user.FullName.Trim();
            user.Role = new[] { "Admin", "Cashier", "Warehouse" }
                .First(role => string.Equals(role, user.Role, StringComparison.OrdinalIgnoreCase));
            user.IsActive = true;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new UserSummary(user.Id, user.Username, user.FullName, user.Role, user.IsActive));
        }

        [HttpPut("{id:int}/password")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Mật khẩu mới không được để trống." });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy tài khoản." });
            }

            user.Password = request.Password.Trim();
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetStatus(int id, [FromBody] SetUserStatusRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy tài khoản." });
            }

            user.IsActive = request.IsActive;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 3. POST: Đăng nhập hệ thống (/api/users/login)
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Vui lòng nhập tên đăng nhập và mật khẩu!" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Username.ToLower() == request.Username.Trim().ToLower() &&
                u.Password == request.Password.Trim());

            if (user == null)
            {
                return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu!" });
            }

            if (!user.IsActive)
            {
                return StatusCode(403, new { message = "Tài khoản của bạn đã bị khóa! Vui lòng liên hệ Admin." });
            }

            return Ok(new
            {
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role,
                Token = _tokens.Create(user.Username, user.Role)
            });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public sealed record UserSummary(int Id, string Username, string FullName, string Role, bool IsActive);
    public sealed record ResetPasswordRequest(string Password);
    public sealed record SetUserStatusRequest(bool IsActive);
}
