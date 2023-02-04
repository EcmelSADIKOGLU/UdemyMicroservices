using FreeCourse.Service.Catalog.Models;
using System;

namespace FreeCourse.Service.Catalog.Dtos
{
    internal class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UserID { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryID { get; set; }
        public CategoryDto Category { get; set; }


    }
}
