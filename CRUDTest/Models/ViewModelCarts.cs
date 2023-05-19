namespace CRUDTest.Models
{
    public class ViewModelCarts
    {
        public ViewModelCarts()
        {
        }
        public int Id { get; set; }

        public int IdProduct { get; set; }
        public Products Products { get; set; }
        public int IdUser { get; set; }
        public Users User { get; set; }
    }
}
