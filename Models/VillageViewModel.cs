using System.Collections.Generic;

namespace ElectionDummy.Models
{
    public class VillageDetailsViewModel
    {
        public int VillageId { get; set; }
        public string VillageName { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }

        public List<VillagePresidentViewModel> VillagePresident { get; set; }
        public List<VillageVicePresidentViewModel> VillageVicePresident { get; set; }
        public List<PollingBoothViewModelForVillage> PollingBooth { get; set; }
    }

    public class VillagePresidentViewModel
    {
        public int PId { get; set; }
        public string PresidentName { get; set; }
        public string ContactNo { get; set; }
    }

    public class VillageVicePresidentViewModel
    {
        public int VPId { get; set; }
        public string VicePresidentName { get; set; }
        public string ContactNo { get; set; }
    }

    public class PollingBoothViewModelForVillage
    {
        public int PollingBoothId { get; set; }
        public string PollingBoothName { get; set; }
        public int VillageId { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }


    }

}