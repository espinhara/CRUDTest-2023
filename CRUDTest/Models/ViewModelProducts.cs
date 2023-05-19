namespace CRUDTest.Models
{
    public class ViewModelProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int Amount { get; set; }
        public string Value { get; set; }

        public List<Products> ProductsList { get; set; }
    }
}
