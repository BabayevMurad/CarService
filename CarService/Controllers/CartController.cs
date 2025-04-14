using CarService.Data;
using CarService.Data.Abstract;
using CarService.Data.Concrete;
using CarService.Dtos;
using CarService.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartController _cartController;

        private readonly AppDataContext _appDataContext;

        public CartController(ICartController cartController, AppDataContext appDataContext)
        {
            _cartController = cartController;
            _appDataContext = appDataContext;
        }

        [HttpPost]
        public void AddCart([FromBody] CartAddDto cartadd)
        {

            var lastname = _appDataContext.Cart.Last().Name;
            var user = _appDataContext.Users.FirstOrDefault(u => u.Id == cartadd.UserId);

            if (lastname is null)
            {
                var cartNew = new Cart { Name = $"{user!.Username}.1", UserId = user.Id, Details = cartadd.Details };
                _cartController.AddCartName(cartNew);
            }
            else 
            {
                //_appDataContext.Cart.Where(c => c.Name.Split('.')[0] == user.Username);
                //_cartController.AddCartName(cartNew);
                var listnames = _appDataContext.Cart
                    .Where(c => c.Name != null && c.Name.StartsWith(user!.Username))
                    .ToList();

                var biggestnumber = 0;
                foreach (var carts in listnames) 
                {
                    int numberbig = int.Parse(carts.Name.Split(".")[1]);

                    if (numberbig > biggestnumber)
                    {
                        biggestnumber = numberbig;
                    }
                }

                var cartNew = new Cart { Name = $"{user!.Username}.{biggestnumber}", UserId = user.Id, Details = cartadd.Details };
                _cartController.AddCartName(cartNew);
            }

            _cartController.AddCart(cartadd.Details);

            _appDataContext.SaveChanges();
        }

        [HttpGet]
        public void GetCart()
        {

        }
    }
}
