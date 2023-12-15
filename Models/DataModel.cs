using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElectionDummy.Models
{
    public class Block
    {
        [Key]
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }
        public List<BlockPresident> BlockPresident { get; set; }
        public List<BlockVicePresident> BlockVicePresident { get; set; }
        public List<Panchayat> Panchayat { get; set; }
    }

    public class BlockPresident
    {
        [Key]
        public int PId { get; set; }
        public string PresidentName { get; set; }
        public string ContactNo { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("BlockId")]
        public Block Block { get; set; }

        public int BlockId { get; set; }
    }

    public class BlockVicePresident
    {
        [Key]
        public int VPId { get; set; }
        public string VicePresidentName { get; set; }
        public string ContactNo { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("BlockId")]
        public Block Block { get; set; }
        public int BlockId { get; set; }
    }

    public class Panchayat
    {
        [Key]
        public int PanchayatId { get; set; }
        public string PanchayatName { get; set; }
        public int BlockId { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("BlockId")]
        public Block Block { get; set; }
        public List<PanchayatPresident> PanchayatPresident { get; set; }
        public List<PanchayatVicePresident> PanchayatVicePresident { get; set; }
        public List<Village> Village { get; set; }
    }


    public class PanchayatPresident
    {
        [Key]
        public int PId { get; set; }
        public string PresidentName { get; set; }
        public string ContactNo { get; set; }

        public int PanchayatId { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("PanchayatId")]
        public Panchayat Panchayat { get; set; }
    }

    public class PanchayatVicePresident
    {
        [Key]
        public int VPId { get; set; }
        public string VicePresidentName { get; set; }
        public string ContactNo { get; set; }

        public int PanchayatId { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("PanchayatId")]
        public Panchayat Panchayat { get; set; }
    }

    public class Village
    {
        [Key]
        public int VillageId { get; set; }
        public string VillageName { get; set; }
        public int PanchayatId { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("PanchayatId")]
        public Panchayat Panchayat { get; set; }
        public List<VillagePresident> VillagePresident { get; set; }
        public List<VillageVicePresident> VillageVicePresident { get; set; }
        public List<PollingBooth> PollingBooth { get; set; }
    }

    public class VillagePresident
    {
        [Key]
        public int PId { get; set; }
        public string PresidentName { get; set; }
        public string ContactNo { get; set; }

        public int VillageId { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("VillageId")]
        public Village Village { get; set; }
    }

    public class VillageVicePresident
    {
        [Key]
        public int VPId { get; set; }
        public string VicePresidentName { get; set; }
        public string ContactNo { get; set; }

        public int VillageId { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("VillageId")]
        public Village Village { get; set; }
    }

    public class PollingBooth
    {
        [Key]
        public int PollingBoothId { get; set; }
        public string PollingBoothName { get; set; }
        public int GeneralPopulation { get; set; }
        public int OBCPopulation { get; set; }
        public int SCPopulation { get; set; }
        public int STPopulation { get; set; }
        public int YadavPopulation { get; set; }
        public int PreviousYearVote { get; set; }

        public int VillageId { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("VillageId")]
        public Village Village { get; set; }
        public List<PollingBoothAgent> PollingBoothAgent { get; set; }
    }

    public class PollingBoothAgent
    {
        [Key]
        public int PollingAgentId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }

        public int PollingBoothId { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey("PollingBoothId")]
        public PollingBooth PollingBooth { get; set; }
    }
}
