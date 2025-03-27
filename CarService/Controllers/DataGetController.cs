using CarService.Entities;
using CarService.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataGetController : ControllerBase
    {
        private readonly IAppRepository _appRepository;

        public DataGetController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet("categorylist")]
        public Task<List<Category>> GetCategoryList()
        {

            var categories =  _appRepository.GetAllCategory();

            return categories;
        }

        [HttpGet("category/{id}")]
        public Task<Category> GetCategory(int id)
        {
            var category = _appRepository.GetCategory(id);

            return category;
        }

        [HttpGet("detailList")]
        public Task<List<Detail>> GetDetailList()
        {

            var details = _appRepository.GetAllDetails();

            return details;
        }

        [HttpGet("detail/{id}")]
        public Task<Detail> GetDetail(int id)
        {
            var detail = _appRepository.GetDetail(id);

            return detail;
        }
    }
}
