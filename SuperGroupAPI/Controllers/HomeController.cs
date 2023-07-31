using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperGroupAPI.Data;
using SuperGroupAPI.Models;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SuperGroupAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : Controller
    {
        private readonly StoreDbContext dbContext;
        public HomeController(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await dbContext.Products.ToListAsync());
        }

        [HttpGet("GetProductsByIds")]
        public async Task<IActionResult> GetProductsByIds([FromQuery] Guid[] productIds)
        {
            return Ok(await dbContext.Products.Where(p => productIds.Contains(p.ProductId)).OrderBy(x => x.ProductId).ToListAsync());
        }

        [HttpPut("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            product.ProductId = Guid.NewGuid();

            string startupPath = System.IO.Directory.GetCurrentDirectory();

            byte[] imageArray = System.IO.File.ReadAllBytes(startupPath + "\\Pictures\\selfie.jpg");
            product.Image = Convert.ToBase64String(imageArray);

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet("GetAllOrderHistories")]
        public async Task<IActionResult> GetAllOrderHistories()
        {
            return Ok(await dbContext.OrderHistories.ToListAsync());
        }

        [HttpPost("PostOrder")]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            order.OrderId = Guid.NewGuid();
            order.OrderDate = DateTime.Now;
            await dbContext.OrderHistories.AddAsync(order);
            await dbContext.SaveChangesAsync();

            return Ok(order);
        }
    }
}
