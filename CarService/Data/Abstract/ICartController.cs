using CarService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Data.Abstract
{
    public interface ICartController
    {
        void AddCart(List<DetailDto> details);
    }
}
