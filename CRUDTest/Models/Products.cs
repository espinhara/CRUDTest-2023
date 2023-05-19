using System.ComponentModel.DataAnnotations;

namespace CRUDTest.Models
{
    public class Products
    {
        public Products()
        {
        }
        [Key]
        public int Id { get; set; }
        [Range(0, 9999999999999999.99)]
        public double Value { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        [StringLength(500)]
        public string Image { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public DateTime Created { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Updated { get; set; }
        
    }
}
