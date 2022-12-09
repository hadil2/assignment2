using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Subscription
    {

        [Key]
        public string ID { get; set; }

        public int ClientId { get; set; }
        public string BrokerageId { get ; set; }
        
    }
}
