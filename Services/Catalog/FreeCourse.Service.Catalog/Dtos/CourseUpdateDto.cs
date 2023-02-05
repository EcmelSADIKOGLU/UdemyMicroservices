namespace FreeCourse.Service.Catalog.Dtos
{
    public class CourseUpdateDto
    {
        public string Id { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UserID { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryID { get; set; }
    }
}
