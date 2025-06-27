using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiparisYonetimAPI.Data;
using SiparisYonetimAPI.DTOs;
using SiparisYonetimAPI.Models;

namespace SiparisYonetimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SiparisContext _context;

        public OrdersController(SiparisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            foreach (var itemDto in createOrderDto.OrderItems)
            {
                var product = await _context.Products.FindAsync(itemDto.ProductId);
                if (product == null)
                {
                    return BadRequest($"Ürün bulunamadı: ID = {itemDto.ProductId}");
                }

                if (product.Stock < itemDto.Quantity)
                {
                    return BadRequest($"Yetersiz stok: {product.Name}, Kalan Stok: {product.Stock}");
                }
            }

            try
            {
                decimal totalAmount = 0;
                var newOrder = new Order
                {
                    UserId = createOrderDto.UserId,
                    OrderDate = DateTime.UtcNow
                };

                foreach (var itemDto in createOrderDto.OrderItems)
                {
                    var product = await _context.Products.FindAsync(itemDto.ProductId);

                    product.Stock -= itemDto.Quantity;
                    _context.Products.Update(product);

                    var orderItem = new OrderItem
                    {
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        Price = product.Price
                    };
                    newOrder.OrderItems.Add(orderItem);

                    totalAmount += product.Price * itemDto.Quantity;
                }

                newOrder.TotalAmount = totalAmount;

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sipariş oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
