namespace Lab4.Models.ViewModels
{
    public class FileInputViewModel
    {
		public string OldBrokerID { get; set; }
		public string BrokerName { get; set; }
		public string BrokerageId { get; set; }
		public string BrokerageTitle { get; set; }
		public IFormFile File { get; set; }


	}
}
