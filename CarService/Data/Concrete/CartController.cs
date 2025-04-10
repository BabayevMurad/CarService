using CarService.Data.Abstract;
using CarService.Dtos;
using CarService.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.Data.Concrete
{
    public class CartController : ICartController
    {
        private readonly IAppRepository _appRepository;

        private readonly AppDataContext _context;

        public CartController(IAppRepository appRepository, AppDataContext context)
        {
            _appRepository = appRepository;
            _context = context;
        }

        public async void AddCart(List<DetailDto> details)
        {
            foreach (var item in details) 
            { 
                await _appRepository.AddAsync(details);
            }

            _context.SaveChanges();
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
