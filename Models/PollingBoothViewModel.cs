using System.Collections.Generic;

namespace ElectionDummy.Models
{
    public class PollingBoothDetailsViewModel
    {
        public int PollingBoothId { get; set; }
        public string PollingBoothName { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }

        public List<PollingBoothAgentViewModelForPollingBooth> PollingBoothAgent { get; set; }
    }

    public class PollingBoothAgentViewModelForPollingBooth
    {
        
        public int PollingAgentId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }

        public int PollingBoothId { get; set; }

    }

}