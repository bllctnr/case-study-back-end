using Business.Abstract;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;
        IProductService _productService;
        public OrdersController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{orderId:int}")]
        public IActionResult Get(int orderId)
        {
            var result = _orderService.GetById(orderId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{orderId:int}/products")]
        public IActionResult GetProductsOfOrder(int orderId)
        {
            var result = _orderService.GetProductsOfOrder(orderId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] OrderWithProductsForAddNewOrderDto orderWithProducts)
        {
            var result = _orderService.Add(orderWithProducts);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("{orderId:int}")]
        public IActionResult Update(int orderId, [FromBody] Order order)
        {
            var result = _orderService.Update(orderId, order);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{orderId:int}")]
        public IActionResult Delete(int orderId)
        {
            var result = _orderService.Delete(orderId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    
    }
}
