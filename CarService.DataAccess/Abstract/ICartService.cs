using CarService.Entities;

namespace CarService.DataAccess.Abstract
{
    public interface ICartService
    {
        Task<Cart> GetCart(int id);
        Task<Cart> AddCartName(Cart cart);
        Task<List<Cart>> GetCartList();
        Task DecreaseDetailCount(List<CartDetail> cartDetails, int id);
        Task<Cart> EditCartDetails(int cartId, List<CartDetail> cartDetails);
    }
}
