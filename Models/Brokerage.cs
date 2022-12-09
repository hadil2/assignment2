using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Models
{
    public class Brokerage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Registration Number")]
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(50 , MinimumLength = 3)]
        public string Title { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName ="money")]
        public Decimal Fee { get; set; }

        public ICollection<Subscription> subscriptions { get; set; }

        public ICollection<Advertisement> advertisements { get; set;}

    }
}
