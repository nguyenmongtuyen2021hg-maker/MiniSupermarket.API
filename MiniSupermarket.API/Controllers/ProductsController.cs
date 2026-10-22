using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/products/paged?keyword=Coca&pageNumber=1&pageSize=5&categoryId=2
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] int? categoryId = null)
        {
            pageNumber = Math.Max(1, pageNumber);
            if (pageSize < 1 || pageSize > 50)
            {
                pageSize = 5;
            }

            // Eager-load Category in the database query and keep this read-only query untracked.
            IQueryable<Product> query = _context.Products
                .Include(product => product.Category)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var search = keyword.Trim();
                query = query.Where(product =>
                    product.ProductName.Contains(search) || product.Barcode.Contains(search));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(product => product.CategoryId == categoryId.Value);
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            pageNumber = totalPages == 0 ? 1 : Math.Min(pageNumber, totalPages);

            var products = await query
                .OrderBy(product => product.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = products.Select(product => new ProductItemDto
            {
                ProductId = product.ProductId,
                Barcode = product.Barcode,
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.CategoryName ?? string.Empty
            }).ToList();

            return Ok(new PagedResult<ProductItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            });
        }
        // 1. GET: Lấy toàn bộ danh sách sản phẩm (/api/products)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
            return Ok(list);
        }

        // 2. GET: Lấy chi tiết sản phẩm theo ID (/api/products/{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm trong CSDL!" });
            }
            return Ok(product);
        }

        // Tra cứu sản phẩm theo Barcode phục vụ quét mã quầy POS
        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Barcode == barcode.Trim());

            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm có mã vạch này!" });
            }
            return Ok(product);
        }

        // 3. GET: Tìm kiếm sản phẩm theo tên hoặc mã vạch (/api/products/search?keyword=...)
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm!" });
            }

            var result = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ProductName.Contains(keyword) || p.Barcode.Contains(keyword))
                .ToListAsync();

            return Ok(result);
        }

        // 4. POST: Thêm mới sản phẩm (/api/products)
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
        }

        // 5. PUT: Cập nhật sản phẩm (/api/products/{id})
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updateProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm cần sửa!" });
            }

            product.Barcode = updateProduct.Barcode;
            product.ProductName = updateProduct.ProductName;
            product.Price = updateProduct.Price;
            product.StockQuantity = updateProduct.StockQuantity;
            product.CategoryId = updateProduct.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. DELETE: Xóa sản phẩm (/api/products/{id})
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm cần xóa!" });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

