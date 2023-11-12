using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3
{
    public class Cassette
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public string? Type { get; set; } 
        [Required]
        public string? Genre { get; set; }
        [Required]
        public string Film { get; set; }
        [Required]
        public DateTime DateCassette { get; set; }
        [Required]
        public string? FIO { get; set; }
        [Required]
        public int Term { get; set; }  //Срок
        [Required]
        public decimal? TotalPrice { get; set; }
    }
}
