using CarService.DataAccess;
using CarService.Entities;
using Microsoft.AspNetCore.Mvc;
using CarService.DataAccess.Abstract;
using CarService.Entities.Dto_s;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartController;

        public CartController(ICartService cartController)
        {
            _cartController = cartController;
        }

        [HttpPost("AddCart")]
        public async Task<ActionResult> AddCart([FromBody] BuyCartDto buycart)
        {
            var cartAdd = await _cartController.AddCartName(buycart.Cart);

            cartAdd.Time = DateTime.Now;

            await _cartController.DecreaseDetailCount(buycart.Cart.Details, cartAdd.Id);

            await _cartController.EditCartDetails(cartAdd.Id, buycart.Cart.Details);

            return Ok();
        }

        [HttpGet("GetCart/{id}")]
        public Task<Cart> GetCart(int id)
        {
            return _cartController.GetCart(id);
        }

        [HttpGet("GetAllCarts")]
        public async Task<ActionResult<List<Cart>>> GetAllCarts()
        {
            var carts = await _cartController.GetCartList();
            return Ok(carts);
        }
    }
}
