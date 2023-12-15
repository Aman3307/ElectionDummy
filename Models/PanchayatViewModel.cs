using System.Collections.Generic;

namespace ElectionDummy.Models
{
    public class PanchayatDetailsViewModel
    {
        public int PanchayatId { get; set; }
        public string PanchayatName { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }

        public List<PanchayatPresidentViewModel> PanchayatPresident { get; set; }
        public List<PanchayatVicePresidentViewModel> PanchayatVicePresident { get; set; }
        public List<VillageViewModelForPanchayat> Village { get; set; }
    }

    public class PanchayatPresidentViewModel
    {
        public int PId { get; set; }
        public string PresidentName { get; set; }
        public string ContactNo { get; set; }
    }

    public class PanchayatVicePresidentViewModel
    {
        public int VPId { get; set; }
        public string VicePresidentName { get; set; }
        public string ContactNo { get; set; }
    }

    public class VillageViewModelForPanchayat
    {
        public int VillageId { get; set; }
        public string VillageName { get; set; }
        public int PanchayatId { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }


    }

}