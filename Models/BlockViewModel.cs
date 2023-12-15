using System.Collections.Generic;

namespace ElectionDummy.Models
{
    public class BlockDetailsViewModel
    {
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }

        public List<BlockPresidentViewModel> BlockPresident { get; set; }
        public List<BlockVicePresidentViewModel> BlockVicePresident { get; set; }
        public List<PanchayatViewModelForBlock> Panchayat { get; set; }
    }

    public class BlockPresidentViewModel
    {
        public int PId { get; set; }
        public string PresidentName { get; set; }
        public string ContactNo { get; set; }
        public int BlockId { get; set; }
    }

    public class BlockVicePresidentViewModel
    {
        public int VPId { get; set; }
        public string VicePresidentName { get; set; }
        public string ContactNo { get; set; }
        public int BlockId { get; set; }
    }

    public class PanchayatViewModelForBlock
    {
        public int PanchayatId { get; set; }
        public string PanchayatName { get; set; }
        public int BlockId { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }

        
    }

}