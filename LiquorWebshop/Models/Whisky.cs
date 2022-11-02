using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LiquorWebshop.Data.Enum;

namespace LiquorWebshop.Models
{
    public class Whisky
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public WhiskyType Type { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public WhiskyTaste Taste { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public float Alcohol { get; set; }
        [Required]
        public int Volume { get; set; }

        public string Image { get; set; }

        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
