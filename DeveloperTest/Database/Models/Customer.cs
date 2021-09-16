using System.ComponentModel.DataAnnotations;

namespace DeveloperTest.Database.Models
{
    public class Customer
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("Large|Small")]
        public string Type { get; set; }
    }
}
