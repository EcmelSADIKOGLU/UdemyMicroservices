using FreeCourse.Service.Catalog.Dtos;
using FreeCourse.Service.Catalog.Services;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    internal class CourseController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        internal CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();

            return CreateActionResultInstance(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIDAsync(id);

            return CreateActionResultInstance(response);

        }

        [Route("api/[controller]/GetAllByUserID/{userId}")]
        public async Task<IActionResult> GetAllByUserID(string userId)
        {
            var response = await _courseService.GetAllByUserIDAsync(userId);

            return CreateActionResultInstance(response);

        }

        [Route("api/[controller]/GetAllByCategoryID/{categoryID}")]
        public async Task<IActionResult> GetAllByCategoryID(string categoryID)
        {
            var response = await _courseService.GetAllByCategoryAsync(categoryID);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto  courseCreateDto)
        {
            var response = await _courseService.CreateAsync(courseCreateDto);

            return CreateActionResultInstance(response);

        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseService.UpdateAsync(courseUpdateDto);

            return CreateActionResultInstance(response);

        }

        [HttpDelete("{id")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }


    }
}
