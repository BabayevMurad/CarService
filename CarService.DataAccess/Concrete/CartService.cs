using CarService.DataAccess.Abstract;
using CarService.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarService.DataAccess.Concrete
{
    public class CartService : ICartService
    {
        private readonly IAppRepository _appRepository;

        private readonly AppDataContext _context;

        public CartService(IAppRepository appRepository, AppDataContext context)
        {
            _appRepository = appRepository;
            _context = context;
        }

        public async Task DecreaseDetailCount(List<CartDetail> cartDetails, int id)
        {
            foreach (var item in cartDetails)
            {
                var detail = await _context.Details.FirstOrDefaultAsync(d => d.Id == item.DetailId);

                detail.Count -= item.Count;

                if (detail.Count <= 0)
                {
                   await _appRepository.DeleteAsync(detail);

                    await _context.SaveChangesAsync();
                }

                item.CartId = id;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Cart> AddCartName(Cart cart)
        {
            var cartReturn = await _appRepository.AddCartAsync(cart);

            return cartReturn;
        }

        public async Task<Cart> EditCartDetails(int cartId,List<CartDetail> cartDetails)
        {
            var cart = await GetCart(cartId);

            cart.Details = cartDetails;

            await _context.SaveChangesAsync();

            return cart;
        }

        public Task<List<Cart>> GetCartList()
        {
            var cart = _context.Cart
                .Include(d => d.Details)
                .ToListAsync();

            return cart;
        }

        public Task<Cart> GetCart(int id)
        {
            var cart = _context.Cart
                .Include(d => d.Details)
                .FirstOrDefaultAsync(c => c.Id == id);

            #pragma warning disable CS8619
            return cart;
            #pragma warning restore CS8619
        }
    }
}
