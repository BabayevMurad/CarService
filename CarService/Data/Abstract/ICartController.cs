using CarService.Dtos;
using CarService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Data.Abstract
{
    public interface ICartController
    {
        void AddCart(List<DetailDto> details);
        Task<Cart> GetCart(int id);
        void AddCartName(Cart cart);
    }
}
