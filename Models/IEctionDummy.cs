using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectionDummy.Models;

namespace ElectionDummy.Interface
{
    public interface IElectionDummy
    {
        IEnumerable<Block> GetBlocks();
        IEnumerable<BlockPresident> GetBlockPresidents();
        IEnumerable<BlockVicePresident> GetBlockVicePresidents();
        IEnumerable<Panchayat> GetPanchayats();
        IEnumerable<PanchayatPresident> GetPanchayatPresidents();
        IEnumerable<PanchayatVicePresident> GetPanchayatVicePresidents();
        IEnumerable<Village> GetVillages();
        IEnumerable<VillagePresident> GetVillagePresidents();
        IEnumerable<VillageVicePresident> GetVillageVicePresidents();
        IEnumerable<PollingBooth> GetPollingBooths();
        IEnumerable<PollingBoothAgent> GetPollingBoothAgents();
        Block FindBlockByID(int BlockId);
        BlockPresident FindBlockPresidentByID(int PId);
        BlockVicePresident FindBlockVicePresidentByID(int VPId);
        Panchayat FindPanchayatByID(int PanchayatId);
        PanchayatPresident FindPanchayatPresidentByID(int PId);
        PanchayatVicePresident FindPanchayatVicePresidentByID(int VPId);
        Village FindVillageByID(int VillageId);
        VillagePresident FindVillagePresidentByID(int PId);
        VillageVicePresident FindVillageVicePresidentByID(int VPId);
        PollingBooth FindPollingBoothById(int PollingBoothId);
        PollingBoothAgent FindPollingBoothAgentById(int PollingAgentId);
        void InsertBlockPresident(BlockPresident blockPresident);
        void InsertBlockVicePresident(BlockVicePresident blockVicePresident);
        void UpdateBlockPresident(BlockPresident blockPresident);
        void UpdateBlockVicePresident(BlockVicePresident blockVicePresident);
        void InsertPanchayatPresident(PanchayatPresident panchayatPresident);
        void InsertPanchayatVicePresident(PanchayatVicePresident panchayatVicePresident);
        void UpdatePanchayatPresident(PanchayatPresident panchayatPresident);
        void UpdatePanchayatVicePresident(PanchayatVicePresident panchayatVicePresident);
        void InsertVillagePresident(VillagePresident VillagePresident);
        void InsertVillageVicePresident(VillageVicePresident VillageVicePresident);
        void UpdateVillagePresident(VillagePresident VillagePresident);
        void UpdateVillageVicePresident(VillageVicePresident VillageVicePresident);
        void InsertPollingBoothAgent(PollingBoothAgent PollingBoothAgent);
        void UpdatePollingBoothAgent(PollingBoothAgent PollingBoothAgent);
        void DeleteBlock(int BlockId);
        void DeleteBlockPresident(int PId);
        void DeleteBlockVicePresident(int VPId);
        void DeletePanchayat(int PanchayatId);
        void DeletePanchayatPresident(int PId);
        void DeletePanchayatVicePresident(int VPId);
        void DeleteVillage(int VillageId);
        void DeleteVillagePresident(int PId);
        void DeleteVillageVicePresident(int VPId);
        void DeletePollingBooth(int PollingBoothId);
        void DeletePollingBoothAgent(int PollingBoothAgentId);
        BlockPresident FindBlockPresidentByBlockId(int blockId);
        BlockVicePresident FindBlockVicePresidentByBlockId(int blockId);
        PanchayatPresident FindPanchayatPresidentByPanchayatId(int panchayatId);
        PanchayatVicePresident FindPanchayatVicePresidentByPanchayatId(int panchayatId);
        VillagePresident FindVillagePresidentByVillageId(int villageId);
        VillageVicePresident FindVillageVicePresidentByVillageId(int villageId);
        List<PollingBoothAgent> FindPollingBoothAgentsByPollingBoothId(int pollingBoothId);
        IEnumerable<BlockDetailsViewModel> FindBlockPanchayatListBlockPresidentBlockVicePresidentByBlockId(int blockId);
        IEnumerable<PanchayatDetailsViewModel> FindPanchayatVillageListPanchayatPresidentPanchayatVicePresidentByPanchayatId(int panchayatId);
        IEnumerable<VillageDetailsViewModel> FindVillagePollingBoothListVillagePresidentVillageVicePresientByVillageId(int villageId);
        IEnumerable<PollingBoothDetailsViewModel> FindPollingBoothPollingBoothAgentByPollingBoothId(int pollingBoothId);
        void InsertBlock(Block block);
        void InsertPanchayat(Panchayat panchayat);
        void InsertVillage(Village village);
        void InsertPollingBooth(PollingBooth pollingBooth);
        IEnumerable<Panchayat> FindPanchayatByBlockId(int blockId);
        IEnumerable<Village> FindVillageByPanchayatId(int panchayatId);
        IEnumerable<Village> FindVillageByBlockAndPanchayatId(int blockId, int panchayatId);
    }
}
