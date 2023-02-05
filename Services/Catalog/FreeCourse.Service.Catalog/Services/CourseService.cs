using AutoMapper;
using FreeCourse.Service.Catalog.Dtos;
using FreeCourse.Service.Catalog.Models;
using FreeCourse.Service.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Service.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find<Course>(x => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryID).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }


            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIDAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found", 404);

            }
            else
            {
                course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryID).FirstAsync();
                return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
            }
 

        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIDAsync(string userId)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserID == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryID).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);

        }

        public async Task<Response<List<CourseDto>>> GetAllByCategoryAsync(string categoryId)
        {
            var courses = await _courseCollection.Find<Course>(x=>x.CategoryID == categoryId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryID).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);

        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto course)
        {
            var newCourse = _mapper.Map<Course>(course);
            newCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == updateCourse.Id, updateCourse);

            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }
            else
            {
                return Response<NoContent>.Success(204);
            }

        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x=>x.Id == id);
            if (result.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }
        }

       
    }
}
