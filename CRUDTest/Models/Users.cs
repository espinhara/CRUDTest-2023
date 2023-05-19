using System.ComponentModel.DataAnnotations;

namespace CRUDTest.Models
{
    public class Users
    {
        public Users()
        {
            
        }
        [Key] 
        public int Id { get; set; }
        [StringLength(100)] 
        public string Name { get; set; }
        [Required][StringLength(100)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required][StringLength(255)]
        public string Password { get; set; }

    }
}
