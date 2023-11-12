using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3
{
    public class Rate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public string? Type { get; set; } //MarkaType
        [Required]
        public string? Genre { get; set; }  //ComplectGenre
        [Required]
        public decimal? DayPrice { get; set; }
    }
}
