using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Models
{
    public class Advertisement
    {
        public int ID { get; set; }
        public string BrokerageID { get; set; }
        public Brokerage Brokerage { get; set; }
        public string fileName { get; set; }
        public string Image { get; set; }
        
        [NotMapped]
        public string BrokerTitle { get; set; }
    }
}
