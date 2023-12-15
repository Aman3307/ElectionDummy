using System.Collections.Generic;
using ElectionDummy.Models;
using System.Data;
using ElectionDummy.Interface;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System;

namespace ElectionDummy.Repository
{
    public class ElectionDummyRepository : IElectionDummy
    {
        private readonly ElectionDummyDbContext _context;
        private readonly string _connectionString;
        public ElectionDummyRepository(ElectionDummyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration["ConnectionStrings:ElectionDummyDbContext"];
        }

        public void InsertBlock(Block block)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    BlockName = block.BlockName,
                    GeneralPopulation = block.GeneralPopulation,
                    OBCPopulation = block.OBCPopulation,
                    SCPopulation = block.SCPopulation,
                    STPopulation = block.STPopulation,
                    YadavPopulation = block.YadavPopulation,
                    PreviousYearVote = block.PreviousYearVote
                };

                dbConnection.Execute("InsertBlock", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Block> GetBlocks()
        {
            return _context.Block.ToList();
        }


        public void InsertPanchayat(Panchayat panchayat)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    PanchayatName = panchayat.PanchayatName,
                    BlockId = panchayat.BlockId,
                    GeneralPopulation = panchayat.GeneralPopulation,
                    OBCPopulation = panchayat.OBCPopulation,
                    SCPopulation = panchayat.SCPopulation,
                    STPopulation = panchayat.STPopulation,
                    YadavPopulation = panchayat.YadavPopulation,
                    PreviousYearVote = panchayat.PreviousYearVote
                };

                dbConnection.Execute("InsertPanchayat", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Panchayat> FindPanchayatByBlockId(int blockId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.Query<Panchayat>("FindPanchayatByBlockId", new { BlockId = blockId }, commandType: CommandType.StoredProcedure);
            }
        }


        public void InsertVillage(Village village)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    VillageName = village.VillageName,
                    PanchayatId = village.PanchayatId,
                    GeneralPopulation = village.GeneralPopulation,
                    OBCPopulation = village.OBCPopulation,
                    SCPopulation = village.SCPopulation,
                    STPopulation = village.STPopulation,
                    YadavPopulation = village.YadavPopulation,
                    PreviousYearVote = village.PreviousYearVote
                };

                dbConnection.Execute("InsertVillage", parameters, commandType: CommandType.StoredProcedure);
            }
        }



        public IEnumerable<Village> FindVillageByPanchayatId(int panchayatId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.Query<Village>("GetVillagesByPanchayat", new { PanchayatId = panchayatId }, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Village> FindVillageByBlockAndPanchayatId(int blockId, int panchayatId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    var villages = dbConnection.Query<Village>("GetVillagesByBlockAndPanchayat", new { BlockId = blockId, PanchayatId = panchayatId }, commandType: CommandType.StoredProcedure);
                    return villages;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public void InsertPollingBooth(PollingBooth pollingBooth)
        {
            try
            {
                // Log VillageId value before calling the stored procedure
                Console.WriteLine("VillageId before InsertPollingBooth: " + pollingBooth.VillageId);

                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var parameters = new
                    {
                        PollingBoothName = pollingBooth.PollingBoothName,
                        VillageId = pollingBooth.VillageId,
                        GeneralPopulation = pollingBooth.GeneralPopulation,
                        OBCPopulation = pollingBooth.OBCPopulation,
                        SCPopulation = pollingBooth.SCPopulation,
                        STPopulation = pollingBooth.STPopulation,
                        YadavPopulation = pollingBooth.YadavPopulation,
                        PreviousYearVote = pollingBooth.PreviousYearVote
                    };

                    dbConnection.Execute("InsertPollingBooth", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine("Exception Message: " + ex.Message);
                Console.WriteLine("Exception Stack Trace: " + ex.StackTrace);
                // Add more logging as needed

                // Optionally, rethrow the exception to propagate it
                throw;
            }
        }





        public IEnumerable<Village> GetVillages()
        {
            return _context.Village.ToList();
        }

        public IEnumerable<PollingBooth> GetPollingBooths()
        {
            return _context.PollingBooth.ToList();
        }

        public IEnumerable<BlockPresident> GetBlockPresidents()
        {
            return _context.BlockPresident.ToList();
        }

        public IEnumerable<BlockVicePresident> GetBlockVicePresidents()
        {
            return _context.BlockVicePresident.ToList();
        }


        public IEnumerable<VillagePresident> GetVillagePresidents()
        {
            return _context.VillagePresident.ToList();
        }

        public IEnumerable<VillageVicePresident> GetVillageVicePresidents()
        {
            return _context.VillageVicePresident.ToList();
        }
        public IEnumerable<PollingBoothAgent> GetPollingBoothAgents()
        {
            return _context.PollingBoothAgent.ToList();
        }
        public Block FindBlockByID(int BlockId)
        {
            return _context.Block.Find(BlockId);
        }

        public BlockPresident FindBlockPresidentByID(int PId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<BlockPresident>("FindBlockPresidentById", new { PId }, commandType: CommandType.StoredProcedure);
            }
        }

        public BlockVicePresident FindBlockVicePresidentByID(int VPId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<BlockVicePresident>("FindBlockVicePresidentById", new { VPId }, commandType: CommandType.StoredProcedure);
            }
        }


        public Panchayat FindPanchayatByID(int PanchayatId)
        {
            return _context.Panchayat.Find(PanchayatId);
        }

        public PanchayatPresident FindPanchayatPresidentByID(int PId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<PanchayatPresident>("FindPanchayatPresidentById", new { PId }, commandType: CommandType.StoredProcedure);
            }
        }

        public PanchayatVicePresident FindPanchayatVicePresidentByID(int VPId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<PanchayatVicePresident>("FindPanchayatVicePresidentById", new { VPId }, commandType: CommandType.StoredProcedure);
            }
        }

        public Village FindVillageByID(int VillageId)
        {
            return _context.Village.Find(VillageId);
        }

        public VillagePresident FindVillagePresidentByID(int PId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<VillagePresident>("FindVillagePresidentById", new { PId }, commandType: CommandType.StoredProcedure);
            }
        }

        public VillageVicePresident FindVillageVicePresidentByID(int VPId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<VillageVicePresident>("FindVillageVicePresidentById", new { VPId }, commandType: CommandType.StoredProcedure);
            }
        }

        public PollingBooth FindPollingBoothById(int PollingBoothId)
        {
            return _context.PollingBooth.Find(PollingBoothId);
        }

        public PollingBoothAgent FindPollingBoothAgentById(int PollingAgentId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<PollingBoothAgent>("FindPollingBoothAgentById", new { PollingAgentId }, commandType: CommandType.StoredProcedure);
            }
        }



       
        public void InsertBlockPresident(BlockPresident blockPresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    PresidentName = blockPresident.PresidentName,
                    ContactNo = blockPresident.ContactNo,
                    BlockId = blockPresident.BlockId
                };

                dbConnection.Execute("InsertBlockPresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }



        public void UpdateBlockPresident(BlockPresident blockPresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    PId = blockPresident.PId,
                    Name = blockPresident.PresidentName,
                    ContactNo = blockPresident.ContactNo
                };

                dbConnection.Execute("UpdateBlockPresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }



        public void InsertBlockVicePresident(BlockVicePresident blockVicePresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    blockVicePresident.VicePresidentName,
                    blockVicePresident.ContactNo,
                    blockVicePresident.BlockId
                };

                dbConnection.Execute("InsertBlockVicePresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateBlockVicePresident(BlockVicePresident blockVicePresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    VPId = blockVicePresident.VPId,
                    blockVicePresident.VicePresidentName,
                    blockVicePresident.ContactNo
                };

                dbConnection.Execute("UpdateBlockVicePresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }



       
        public void InsertPanchayatPresident(PanchayatPresident panchayatPresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    PresidentName = panchayatPresident.PresidentName,
                    ContactNo = panchayatPresident.ContactNo,
                    PanchayatId = panchayatPresident.PanchayatId
                };

                dbConnection.Execute("InsertPanchayatPresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }



        public void UpdatePanchayatPresident(PanchayatPresident panchayatPresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    PId = panchayatPresident.PId,
                    Name = panchayatPresident.PresidentName,
                    ContactNo = panchayatPresident.ContactNo
                };

                dbConnection.Execute("UpdatePanchayatPresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }



        public void InsertPanchayatVicePresident(PanchayatVicePresident panchayatVicePresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    VicePresidentName = panchayatVicePresident.VicePresidentName,
                    ContactNo = panchayatVicePresident.ContactNo,
                    PanchayatId = panchayatVicePresident.PanchayatId
                };

                dbConnection.Execute("InsertPanchayatVicePresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdatePanchayatVicePresident(PanchayatVicePresident blockVicePresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    VPId = blockVicePresident.VPId,
                    blockVicePresident.VicePresidentName,
                    blockVicePresident.ContactNo
                };

                dbConnection.Execute("UpdatePanchayatVicePresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }
       


        public void InsertVillagePresident(VillagePresident villagePresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    PresidentName = villagePresident.PresidentName,
                    ContactNo = villagePresident.ContactNo,
                    VillageId = villagePresident.VillageId
                };

                dbConnection.Execute("InsertVillagePresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }



        public void UpdateVillagePresident(VillagePresident villagePresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    PId = villagePresident.PId,
                    Name = villagePresident.PresidentName,
                    ContactNo = villagePresident.ContactNo
                };

                dbConnection.Execute("UpdateVillagePresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }



        public void InsertVillageVicePresident(VillageVicePresident villageVicePresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    villageVicePresident.VicePresidentName,
                    villageVicePresident.ContactNo,
                    villageVicePresident.VillageId
                };

                dbConnection.Execute("InsertVillageVicePresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateVillageVicePresident(VillageVicePresident villageVicePresident)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    VPId = villageVicePresident.VPId,
                    villageVicePresident.VicePresidentName,
                    villageVicePresident.ContactNo
                };

                dbConnection.Execute("UpdateVillageVicePresident", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        

       

        public void UpsertPollingBoothAgent(PollingBoothAgent pollingBoothAgent)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    pollingBoothAgent.Name,
                    pollingBoothAgent.ContactNo,
                    pollingBoothAgent.PollingBoothId
                    // Add other properties as needed
                };

                dbConnection.Execute("UpsertPollingBoothAgent", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public void InsertPollingBoothAgent(PollingBoothAgent pollingBoothAgent)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    pollingBoothAgent.Name,
                    pollingBoothAgent.ContactNo,
                    pollingBoothAgent.PollingBoothId
                };

                dbConnection.Execute("InsertPollingBoothAgent", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdatePollingBoothAgent(PollingBoothAgent pollingBoothAgent)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    PollingAgentId = pollingBoothAgent.PollingAgentId,
                    Name = pollingBoothAgent.Name,
                    ContactNo = pollingBoothAgent.ContactNo
                };

                dbConnection.Execute("UpdatePollingBoothAgent", parameters, commandType: CommandType.StoredProcedure);
            }
        }


        public void DeleteBlock(int blockId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeleteBlock", new { BlockId = blockId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteBlockPresident(int PId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeleteBlockPresident", new { PId = PId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteBlockVicePresident(int VPId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeleteBlockVicePresident", new { VPId = VPId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeletePanchayat(int panchayatId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeletePanchayat", new { PanchayatId = panchayatId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeletePanchayatPresident(int PId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeletePanchayatPresident", new { PId = PId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeletePanchayatVicePresident(int VPId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeletePanchayatVicePresident", new { VPId = VPId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteVillage(int villageId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeleteVillage", new { VillageId = villageId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteVillagePresident(int PId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeleteVillagePresident", new { PId = PId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteVillageVicePresident(int VPId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Execute("DeleteVillageVicePresident", new { VPId = VPId }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error deleting Village Vice President with VPId {VPId}: {ex.Message}");
                throw; // Rethrow the exception if needed
            }
        }


        public void DeletePollingBooth(int pollingBoothId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeletePollingBooth", new { PollingBoothId = pollingBoothId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeletePollingBoothAgent(int PollingAgentId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Execute("DeletePollingBoothAgent", new { PollingAgentId = PollingAgentId }, commandType: CommandType.StoredProcedure);
            }
        }

        public BlockPresident FindBlockPresidentByBlockId(int blockId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<BlockPresident>("FindBlockPresidentByBlockId", new { BlockId = blockId }, commandType: CommandType.StoredProcedure);
            }
        }

        public BlockVicePresident FindBlockVicePresidentByBlockId(int blockId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<BlockVicePresident>("FindBlockVicePresidentByBlockId", new { BlockId = blockId }, commandType: CommandType.StoredProcedure);
            }
        }

        public PanchayatPresident FindPanchayatPresidentByPanchayatId(int panchayatId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<PanchayatPresident>("FindPanchayatPresidentByPanchayatId", new { PanchayatId = panchayatId }, commandType: CommandType.StoredProcedure);
            }
        }

        public PanchayatVicePresident FindPanchayatVicePresidentByPanchayatId(int panchayatId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<PanchayatVicePresident>("FindPanchayatVicePresidentByPanchayatId", new { PanchayatId = panchayatId }, commandType: CommandType.StoredProcedure);
            }
        }

        public VillagePresident FindVillagePresidentByVillageId(int villageId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<VillagePresident>("FindVillagePresidentByVillageId", new { VillageId = villageId }, commandType: CommandType.StoredProcedure);
            }
        }

        public VillageVicePresident FindVillageVicePresidentByVillageId(int villageId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.QueryFirstOrDefault<VillageVicePresident>("FindVillageVicePresidentByVillageId", new { VillageId = villageId }, commandType: CommandType.StoredProcedure);
            }
        }

        public List<PollingBoothAgent> FindPollingBoothAgentsByPollingBoothId(int pollingBoothId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.Query<PollingBoothAgent>("FindPollingBoothAgentsByPollingBoothId", new { PollingBoothId = pollingBoothId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<BlockDetailsViewModel> FindBlockPanchayatListBlockPresidentBlockVicePresidentByBlockId(int blockId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var multi = connection.QueryMultiple("FindBlockPanchayatListBlockPresidentBlockVicePresidentByBlockId", new { BlockId = blockId },commandType: CommandType.StoredProcedure))
                {
                    //DataSet dataSet = new DataSet();
                    //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    //sqlDataAdapter.Fill(dataSet);
                    // Retrieve data from the first result set (Block table)
                    var blockData = multi.Read<Block>().SingleOrDefault();
                   

                    // Retrieve data from the second result set (BlockPresident table)
                    var blockPresidentData = multi.Read<BlockPresident>().ToList();

                    // Retrieve data from the third result set (BlockVicePresident table)
                    var blockVicePresidentData = multi.Read<BlockVicePresident>().ToList();

                    // Retrieve data from the fourth result set (Panchayat table)
                    var panchayatData = multi.Read<Panchayat>().ToList();

                    // Create a BlockDetailsViewModel and populate it with the retrieved data
                    var blockDetailsViewModel = new BlockDetailsViewModel
                    {
                        BlockId = blockData?.BlockId ?? 0,
                        BlockName = blockData?.BlockName,
                        GeneralPopulation = blockData?.GeneralPopulation ?? 0,
                        OBCPopulation = blockData?.OBCPopulation ?? 0,
                        SCPopulation = blockData?.SCPopulation ?? 0,
                        STPopulation = blockData?.STPopulation ?? 0,
                        YadavPopulation = blockData?.YadavPopulation ?? 0,
                        PreviousYearVote = blockData?.PreviousYearVote ?? 0,
                        BlockPresident = blockPresidentData.Select(president => new BlockPresidentViewModel
                        {
                            PId = president.PId,
                            PresidentName = president.PresidentName,
                            ContactNo = president.ContactNo
                        }).ToList(),
                        BlockVicePresident = blockVicePresidentData.Select(vicePresident => new BlockVicePresidentViewModel
                        {
                            VPId = vicePresident.VPId,
                            VicePresidentName = vicePresident.VicePresidentName,
                            ContactNo = vicePresident.ContactNo
                        }).ToList(),
                        Panchayat = panchayatData.Select(panchayat => new PanchayatViewModelForBlock
                        {
                            PanchayatId = panchayat.PanchayatId,
                            PanchayatName = panchayat.PanchayatName,
                            GeneralPopulation = panchayat.GeneralPopulation,
                            OBCPopulation = panchayat.OBCPopulation,
                            SCPopulation = panchayat.SCPopulation,
                            STPopulation = panchayat.STPopulation,
                            YadavPopulation = panchayat.YadavPopulation,
                            PreviousYearVote = panchayat.PreviousYearVote
                        }).ToList()
                    };

                    // Return the BlockDetailsViewModel as a collection
                    return new List<BlockDetailsViewModel> { blockDetailsViewModel };
                }
            }
        }


        public IEnumerable<PanchayatDetailsViewModel> FindPanchayatVillageListPanchayatPresidentPanchayatVicePresidentByPanchayatId(int panchayatId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var multi = connection.QueryMultiple(
                    "FindPanchayatVillageListPanchayatPresidentPanchayatVicePresidentByPanchayatId",
                    new { PanchayatId = panchayatId },
                    commandType: CommandType.StoredProcedure))
                {
                    // Retrieve data from the first result set (Panchayat table)
                    var panchayatData = multi.Read<Panchayat>().SingleOrDefault();

                    // Retrieve data from the second result set (PanchayatPresident table)
                    var panchayatPresidentData = multi.Read<PanchayatPresident>().ToList();

                    // Retrieve data from the third result set (PanchayatVicePresident table)
                    var panchayatVicePresidentData = multi.Read<PanchayatVicePresident>().ToList();

                    // Retrieve data from the fourth result set (Village table)
                    var villageData = multi.Read<Village>().ToList();

                    // Create a PanchayatDetailsViewModel and populate it with the retrieved data
                    var panchayatDetailsViewModel = new PanchayatDetailsViewModel
                    {
                        PanchayatId = panchayatData?.PanchayatId ?? 0,
                        PanchayatName = panchayatData?.PanchayatName,
                        GeneralPopulation = panchayatData?.GeneralPopulation ?? 0,
                        OBCPopulation = panchayatData?.OBCPopulation ?? 0,
                        SCPopulation = panchayatData?.SCPopulation ?? 0,
                        STPopulation = panchayatData?.STPopulation ?? 0,
                        YadavPopulation = panchayatData?.YadavPopulation ?? 0,
                        PreviousYearVote = panchayatData?.PreviousYearVote ?? 0,
                        PanchayatPresident = panchayatPresidentData.Select(president => new PanchayatPresidentViewModel
                        {
                            PId = president.PId,
                            PresidentName = president.PresidentName,
                            ContactNo = president.ContactNo
                        }).ToList(),
                        PanchayatVicePresident = panchayatVicePresidentData.Select(vicePresident => new PanchayatVicePresidentViewModel
                        {
                            VPId = vicePresident.VPId,
                            VicePresidentName = vicePresident.VicePresidentName,
                            ContactNo = vicePresident.ContactNo
                        }).ToList(),
                        Village = villageData.Select(village => new VillageViewModelForPanchayat
                        {
                            VillageId = village.VillageId,
                            VillageName = village.VillageName,
                            PanchayatId = village.PanchayatId,
                            GeneralPopulation = village.GeneralPopulation,
                            OBCPopulation = village.OBCPopulation,
                            SCPopulation = village.SCPopulation,
                            STPopulation = village.STPopulation,
                            YadavPopulation = village.YadavPopulation,
                            PreviousYearVote = village.PreviousYearVote
                        }).ToList()
                    };

                    // Return the PanchayatDetailsViewModel as a collection
                    return new List<PanchayatDetailsViewModel> { panchayatDetailsViewModel };
                }
            }
        }


        public IEnumerable<VillageDetailsViewModel> FindVillagePollingBoothListVillagePresidentVillageVicePresientByVillageId(int villageId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var multi = connection.QueryMultiple(
                    "FindVillagePollingBoothListVillagePresidentVillageVicePresientByVillageId",
                    new { VillageId = villageId },
                    commandType: CommandType.StoredProcedure))
                {
                    // Retrieve data from the first result set (Village table)
                    var villageData = multi.Read<Village>().SingleOrDefault();

                    // Retrieve data from the second result set (VillagePresident table)
                    var villagePresidentData = multi.Read<VillagePresident>().ToList();

                    // Retrieve data from the third result set (VillageVicePresident table)
                    var villageVicePresidentData = multi.Read<VillageVicePresident>().ToList();

                    // Retrieve data from the fourth result set (PollingBooth table)
                    var pollingBoothData = multi.Read<PollingBooth>().ToList();

                    // Create a VillageDetailsViewModel and populate it with the retrieved data
                    var villageDetailsViewModel = new VillageDetailsViewModel
                    {
                        VillageId = villageData?.VillageId ?? 0,
                        VillageName = villageData?.VillageName,
                        GeneralPopulation = villageData?.GeneralPopulation ?? 0,
                        OBCPopulation = villageData?.OBCPopulation ?? 0,
                        SCPopulation = villageData?.SCPopulation ?? 0,
                        STPopulation = villageData?.STPopulation ?? 0,
                        YadavPopulation = villageData?.YadavPopulation ?? 0,
                        PreviousYearVote = villageData?.PreviousYearVote ?? 0,
                        VillagePresident = villagePresidentData.Select(president => new VillagePresidentViewModel
                        {
                            PId = president.PId,
                            PresidentName = president.PresidentName,
                            ContactNo = president.ContactNo
                        }).ToList(),
                        VillageVicePresident = villageVicePresidentData.Select(vicePresident => new VillageVicePresidentViewModel
                        {
                            VPId = vicePresident.VPId,
                            VicePresidentName = vicePresident.VicePresidentName,
                            ContactNo = vicePresident.ContactNo
                        }).ToList(),
                        PollingBooth = pollingBoothData.Select(pollingBooth => new PollingBoothViewModelForVillage
                        {
                            PollingBoothId = pollingBooth.PollingBoothId,
                            PollingBoothName = pollingBooth.PollingBoothName,
                            VillageId = pollingBooth.VillageId,
                            GeneralPopulation = pollingBooth.GeneralPopulation,
                            OBCPopulation = pollingBooth.OBCPopulation,
                            SCPopulation = pollingBooth.SCPopulation,
                            STPopulation = pollingBooth.STPopulation,
                            YadavPopulation = pollingBooth.YadavPopulation,
                            PreviousYearVote = pollingBooth.PreviousYearVote
                        }).ToList()
                    };

                    // Return the VillageDetailsViewModel as a collection
                    return new List<VillageDetailsViewModel> { villageDetailsViewModel };
                }
            }
        }

        public IEnumerable<PollingBoothDetailsViewModel> FindPollingBoothPollingBoothAgentByPollingBoothId(int pollingBoothId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var multi = connection.QueryMultiple(
                    "FindPollingBoothPollingBoothAgentByPollingBoothId",
                    new { PollingBoothId = pollingBoothId },
                    commandType: CommandType.StoredProcedure))
                {
                    // Retrieve data from the first result set (PollingBooth table)
                    var pollingBoothData = multi.Read<PollingBoothDetailsViewModel>().SingleOrDefault();

                    // Retrieve data from the second result set (PollingBoothAgent table)
                    var pollingBoothAgentData = multi.Read<PollingBoothAgentViewModelForPollingBooth>().ToList();

                    // Create a PollingBoothDetailsViewModel and populate it with the retrieved data
                    var pollingBoothDetailsViewModel = new PollingBoothDetailsViewModel
                    {
                        PollingBoothId = pollingBoothData?.PollingBoothId ?? 0,
                        PollingBoothName = pollingBoothData?.PollingBoothName,
                        GeneralPopulation = pollingBoothData?.GeneralPopulation ?? 0,
                        OBCPopulation = pollingBoothData?.OBCPopulation ?? 0,
                        SCPopulation = pollingBoothData?.SCPopulation ?? 0,
                        STPopulation = pollingBoothData?.STPopulation ?? 0,
                        YadavPopulation = pollingBoothData?.YadavPopulation ?? 0,
                        PreviousYearVote = pollingBoothData?.PreviousYearVote ?? 0,
                        PollingBoothAgent = pollingBoothAgentData
                    };

                    // Return the PollingBoothDetailsViewModel as a collection
                    return new List<PollingBoothDetailsViewModel> { pollingBoothDetailsViewModel };
                }
            }
        }





        public IEnumerable<Panchayat> GetPanchayats()
        {
            return _context.Panchayat.ToList();
        }

        public IEnumerable<PanchayatPresident> GetPanchayatPresidents()
        {
            return _context.PanchayatPresident.ToList();
        }

        public IEnumerable<PanchayatVicePresident> GetPanchayatVicePresidents()
        {
            return _context.PanchayatVicePresident.ToList();
        }
    }
}