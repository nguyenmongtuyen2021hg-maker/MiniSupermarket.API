using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Lấy chuỗi kết nối từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký DbContext sử dụng SQL Server qua cơ chế Dependency Injection (DI)
builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(connectionString));

// Thêm Controllers và dịch vụ Swagger Gen
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Tự động tạo cơ sở dữ liệu và nạp dữ liệu mẫu ban đầu (EnsureCreated)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SupermarketDbContext>();
    context.Database.EnsureCreated();
}

// Cấu hình sử dụng Swagger ở môi trường phát triển
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
