using CarService.DataAccess;
using CarService.DataAccess.Abstract;
using CarService.Entities;
using CarService.Entities.Dto_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace CarService.WebApi.Controllers
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

        [HttpGet("CategoryList")]
        public Task<List<Category>> GetCategoryList()
        {
            var categories = _appRepository.GetAllCategory();

            return categories;
        }

        [HttpGet("Category/{id}")]
        public Task<Category> GetCategory(int id)
        {
            var category = _appRepository.GetCategory(id);

            return category;
        }

        [HttpGet("DetailList")]
        public Task<List<Detail>> GetDetailList()
        {

            var details = _appRepository.GetAllDetails();

            return details;
        }

        [HttpGet("Detail/{id}")]
        public Task<Detail> GetDetail(int id)
        {
            var detail = _appRepository.GetDetail(id);

            return detail;
        }

        [HttpGet("DetailByCategory/{id}")]
        public async Task<List<Detail>> DetailByCategory(int id)
        {
            var detail = await _appRepository.GetAllDetailsByCategory(id);

            return detail;
        }

        [HttpPost("AddDetail")]
        public async Task AddDetail([FromBody] AddDetailDto detailDto)
        {
            var detail = new Detail
            {
                Name = detailDto.Name,
                Price = detailDto.Price,
                ImageUrl = detailDto.ImageUrl,
                Count = detailDto.Count,
                CategoryId = detailDto.CategoryId,
            };

            await _appRepository.AddAsync(detail);

            await _appRepository.SaveAllAsync();
        }

        [HttpDelete("DetailDelete/{id}")]
        public async Task DeleteDetail(int id)
        {
            var detail = await _appRepository.GetDetail(id);

            await _appRepository.DeleteAsync(detail);

            await _appRepository.SaveAllAsync();
        }

        [HttpPost("DetailEdit")]
        public async Task<IActionResult> EditDetail([FromBody] AddDetailDto detailDto, int id)
        {
            var detail = new Detail
            {
                Name = detailDto.Name,
                Price = detailDto.Price,
                ImageUrl = detailDto.ImageUrl,
                Count = detailDto.Count,
                CategoryId = detailDto.CategoryId,
            };

            await _appRepository.EditDetail(detail, id);

            await _appRepository.SaveAllAsync();

            return Ok();
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto addCategory)
        {
            var category = new Category
            {
                Name = addCategory.Name,
            };

            await _appRepository.AddAsync(category);

            await _appRepository.SaveAllAsync();

            return Ok();
        }

        [HttpDelete("CategoryDelete/{id}")]
        public async Task CategoryDelite(int id)
        {
            var category = await _appRepository.GetCategory(id);

            await _appRepository.DeleteAsync(category);

            await _appRepository.SaveAllAsync();
        }

        [HttpPost("EditCategory")]
        public async Task<IActionResult> EditCategory([FromBody] AddCategoryDto addCategory, int id)
        {
            var category = new Category
            {
                Name = addCategory.Name,
            };

            await _appRepository.EditCategory(category, id);

            await _appRepository.SaveAllAsync();

            return Ok();
        }
    }
}
