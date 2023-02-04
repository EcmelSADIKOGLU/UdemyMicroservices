using System;

namespace FreeCourse.Service.Catalog.Dtos
{
    internal class CourseCreateDto
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UserID { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryID { get; set; }

    }
}
