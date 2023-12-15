using ElectionDummy.Interface;
using ElectionDummy.Models;
using ElectionDummy.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionDummy.Controllers
{
    public class ElectionController : Controller
    {
        private readonly IElectionDummy _electionDummyRepositary;

        public ElectionController(IElectionDummy electionDummyRepositary)
        {
            _electionDummyRepositary = electionDummyRepositary;
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Honourable xxx, from xxx , xxx";

            var viewModel = new IndexViewModel
            {
                Block = (List<Block>)_electionDummyRepositary.GetBlocks(), // Implement GetBlocks() in your service
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult CreateBlock()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBlock(Block block)
        {
            _electionDummyRepositary.InsertBlock(block);
            return RedirectToAction("Index");
        }

        // Create Panchayat
        [HttpGet]
        public IActionResult CreatePanchayat()
        {
            ViewBag.Blocks = _electionDummyRepositary.GetBlocks();
            return View();
        }

        [HttpPost]
        public IActionResult CreatePanchayat(Panchayat panchayat)
        {
            _electionDummyRepositary.InsertPanchayat(panchayat);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateVillage()
        {
            ViewBag.Blocks = _electionDummyRepositary.GetBlocks();
            ViewBag.Panchayats = new List<Panchayat>();
            return View();
        }

        [HttpPost]
        public IActionResult CreateVillage(Village village)
        {
            _electionDummyRepositary.InsertVillage(village);
            return RedirectToAction("Index");
        }

        // AJAX action to get Panchayats based on selected Block

        // Create PollingBooth
        [HttpGet]
        public IActionResult CreatePollingBooth()
        {
            ViewBag.Blocks = _electionDummyRepositary.GetBlocks();
            ViewBag.Panchayats = new List<Panchayat>(); // Empty list for initial load
            ViewBag.Villages = new List<Village>(); // Empty list for initial load
            return View();
        }
        [HttpGet]
        public IActionResult GetPanchayats(int blockId)
        {
            var panchayats = _electionDummyRepositary.FindPanchayatByBlockId(blockId);
            var selectList = new SelectList(panchayats, nameof(Panchayat.PanchayatId), nameof(Panchayat.PanchayatName));
            return Json(selectList);
        }

        [HttpPost]
        public IActionResult CreatePollingBooth(PollingBooth pollingBooth, int blockId, int panchayatId, int villageId)
        {
            // Set the VillageId in the pollingBooth model
            pollingBooth.VillageId = villageId;

            // Now, you can proceed with saving the PollingBooth
            _electionDummyRepositary.InsertPollingBooth(pollingBooth);

            return RedirectToAction("Index");
        }

        // AJAX actions to get Panchayats and Villages based on selected Block and Panchayat
        [HttpGet]
        public IActionResult GetPanchayatsForPollingBooth(int blockId)
        {
            var panchayats = _electionDummyRepositary.FindPanchayatByBlockId(blockId);
            var selectList = new SelectList(panchayats, nameof(Panchayat.PanchayatId), nameof(Panchayat.PanchayatName));
            return Json(selectList);
        }
        [HttpGet]
        public IActionResult GetVillages(int panchayatId)
        {
            var villages = _electionDummyRepositary.FindVillageByPanchayatId(panchayatId);
            var selectList = new SelectList(villages, nameof(Village.VillageId), nameof(Village.VillageName));
            return Json(selectList);
        }
        [HttpGet]
        public IActionResult GetVillagesForPollingBooth(int blockId, int panchayatId)
        {
            var villages = _electionDummyRepositary.FindVillageByBlockAndPanchayatId(blockId, panchayatId);
            var selectList = new SelectList(villages, nameof(Village.VillageId), nameof(Village.VillageName));
            return Json(selectList);
        }


        // Edit Block, Panchayat, Village, PollingBooth (similar logic for other entities)
        [HttpGet]
        public IActionResult EditBlock(int blockId)
        {
            var block = _electionDummyRepositary.FindBlockByID(blockId);
            return View(block);
        }

        [HttpPost]
        public IActionResult EditBlock(Block block)
        {
            _electionDummyRepositary.UpsertBlock(block);
            return RedirectToAction("Index");
        }

        // Other actions for updating and deleting records (similar logic)
        [HttpGet]
        public IActionResult DeleteBlock(int blockId)
        {
            _electionDummyRepositary.DeleteBlock(blockId);
            return RedirectToAction("Index");
        }

        [HttpGet("BlockDetails")]
        public IActionResult BlockDetails(int blockId)
        {
            var blockDetails = _electionDummyRepositary.FindBlockPanchayatListBlockPresidentBlockVicePresidentByBlockId(blockId);

            if (blockDetails == null)
            {
                // Handle the case where no data is retrieved
                return NotFound(); // Or return a specific view or message
            }

            return View(blockDetails.FirstOrDefault());
        }

        [HttpGet]
        public IActionResult UpdateBlockPresident(int PId)
        {
            var blockPresident = new BlockPresident();

            if (PId > 0)
            {
                // Retrieve existing BlockPresident data for editing
                blockPresident = _electionDummyRepositary.FindBlockPresidentByID(PId);
            }

            return View("UpdateBlockPresident", blockPresident);
        }

        [HttpGet]
        public IActionResult InsertBlockPresident(int BlockId)
        {
            var blockPresident = new BlockPresident
            {
                // Set the BlockId for the view
                BlockId = BlockId
            };

            return View("InsertBlockPresident", blockPresident);
        }

        [HttpPost]
        public IActionResult UpdateBlockPresident(BlockPresident blockPresident)
        {
            if (ModelState.IsValid)
            {
                if (blockPresident.PId > 0)
                {
                    // Update existing record
                    _electionDummyRepositary.UpdateBlockPresident(blockPresident);
                }
                int blockId = blockPresident.BlockId;

                return RedirectToAction("BlockDetails", new { blockId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int blockId = blockPresident.BlockId;

                return RedirectToAction("BlockDetails", new { blockId });
            }
        }

        [HttpPost]
        public IActionResult InsertBlockPresident(BlockPresident blockPresident)
        {
            if (ModelState.IsValid)
            {
                // Insert new record
                _electionDummyRepositary.InsertBlockPresident(blockPresident);

                int blockId = blockPresident.BlockId;

                return RedirectToAction("BlockDetails", new { blockId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int blockId = blockPresident.BlockId;

                return RedirectToAction("BlockDetails", new { blockId });
            }
        }

        [HttpGet]
        public IActionResult UpdateBlockVicePresident(int VPId)
        {
            var blockVicePresident = new BlockVicePresident();

            if (VPId > 0)
            {
                // Retrieve existing BlockVicePresident data for editing
                blockVicePresident = _electionDummyRepositary.FindBlockVicePresidentByID(VPId);
            }

            return View("UpdateBlockVicePresident", blockVicePresident);
        }

        [HttpGet]
        public IActionResult InsertBlockVicePresident(int BlockId)
        {
            var blockVicePresident = new BlockVicePresident
            {
                // Set the BlockId for the view
                BlockId = BlockId
            };

            return View("InsertBlockVicePresident", blockVicePresident);
        }

        [HttpPost]
        public IActionResult UpdateBlockVicePresident(BlockVicePresident blockVicePresident)
        {
            if (ModelState.IsValid)
            {
                if (blockVicePresident.VPId > 0)
                {
                    // Update existing record
                    _electionDummyRepositary.UpdateBlockVicePresident(blockVicePresident);
                }

                int blockId = blockVicePresident.BlockId;

                return RedirectToAction("BlockDetails", new { blockId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int blockId = blockVicePresident.BlockId;

                return RedirectToAction("BlockDetails", new { blockId });
            }
        }

        [HttpPost]
        public IActionResult InsertBlockVicePresident(BlockVicePresident blockVicePresident)
        {
            if (ModelState.IsValid)
            {
                // Insert new record
                _electionDummyRepositary.InsertBlockVicePresident(blockVicePresident);

                int blockId = blockVicePresident.BlockId;

                return RedirectToAction("BlockDetails", new { blockId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int blockId = blockVicePresident.BlockId;

                return RedirectToAction("BlockDetails", new { blockId });
            }
        }



        [HttpGet]
        public IActionResult DeleteBlockPresident(int PId)
        {
            var blockPresident = _electionDummyRepositary.FindBlockPresidentByID(PId);

            if (blockPresident == null)
            {
                // Handle the case where the BlockPresident with the given PId is not found
                return NotFound();
            }

            return View(blockPresident);
        }

        [HttpPost]
        public IActionResult DeleteBlockPresidentConfirmed(int PId)
        {
            _electionDummyRepositary.DeleteBlockPresident(PId);
            return RedirectToAction("Index"); // Redirect to the desired action/page after deletion
        }


        [HttpGet]
        public IActionResult DeleteBlockVicePresident(int VPId)
        {
            var blockVicePresident = _electionDummyRepositary.FindBlockVicePresidentByID(VPId);

            if (blockVicePresident == null)
            {
                // Handle the case where the BlockVicePresident with the given VPId is not found
                return NotFound();
            }

            return View(blockVicePresident);
        }

        [HttpPost]
        public IActionResult DeleteBlockVicePresidentConfirmed(int VPId)
        {
            _electionDummyRepositary.DeleteBlockVicePresident(VPId);
            return RedirectToAction("Index"); // Redirect to the desired action/page after deletion
        }



        [HttpGet("PanchayatDetails")]
        public IActionResult PanchayatDetails(int panchayatId)
        {
            var panchayatDetails = _electionDummyRepositary.FindPanchayatVillageListPanchayatPresidentPanchayatVicePresidentByPanchayatId(panchayatId);

            if (panchayatDetails == null)
            {
                // Handle the case where no data is retrieved
                return NotFound(); // Or return a specific view or message
            }

            return View(panchayatDetails.FirstOrDefault());
        }
        
        [HttpGet]
        public IActionResult DeletePanchayatPresident(int PId)
        {
            var panchayatPresident = _electionDummyRepositary.FindPanchayatPresidentByID(PId);

            if (panchayatPresident == null)
            {
                // Handle the case where the BlockPresident with the given PId is not found
                return NotFound();
            }

            return View(panchayatPresident);
        }

        [HttpPost]
        public IActionResult DeletePanchayatPresidentConfirmed(int PId)
        {
            _electionDummyRepositary.DeletePanchayatPresident(PId);
            return RedirectToAction("Index"); // Redirect to the desired action/page after deletion
        }


        [HttpGet]
        public IActionResult DeletePanchayatVicePresident(int VPId)
        {
            var panchayatVicePresident = _electionDummyRepositary.FindPanchayatVicePresidentByID(VPId);

            if (panchayatVicePresident == null)
            {
                // Handle the case where the PanchayatVicePresident with the given VPId is not found
                return NotFound();
            }

            return View(panchayatVicePresident);
        }

        [HttpPost]
        public IActionResult DeletePanchayatVicePresidentConfirmed(int VPId)
        {
            _electionDummyRepositary.DeletePanchayatVicePresident(VPId);
            return RedirectToAction("Index"); // Redirect to the desired action/page after deletion
        }

        [HttpGet("VillageDetails")]
        public IActionResult VillageDetails(int villageId)
        {
            var villageDetails = _electionDummyRepositary.FindVillagePollingBoothListVillagePresidentVillageVicePresientByVillageId(villageId);

            if (villageDetails == null)
            {
                // Handle the case where no data is retrieved
                return NotFound(); // Or return a specific view or message
            }

            return View(villageDetails.FirstOrDefault());
        }


        

        [HttpGet]
        public IActionResult DeleteVillagePresident(int PId)
        {
            var villagePresident = _electionDummyRepositary.FindVillagePresidentByID(PId);

            if (villagePresident == null)
            {
                // Handle the case where the VillagePresident with the given PId is not found
                return NotFound();
            }

            return View(villagePresident);
        }

        [HttpPost]
        public IActionResult DeleteVillagePresidentConfirmed(int PId)
        {
            _electionDummyRepositary.DeleteVillagePresident(PId);
            return RedirectToAction("Index"); // Redirect to the desired action/page after deletion
        }


        [HttpGet]
        public IActionResult DeleteVillageVicePresident(int VPId)
        {
            var villageVicePresident = _electionDummyRepositary.FindVillageVicePresidentByID(VPId);

            if (villageVicePresident == null)
            {
                // Handle the case where the VillageVicePresident with the given VPId is not found
                return NotFound();
            }

            return View(villageVicePresident);
        }

        [HttpPost]
        public IActionResult DeleteVillageVicePresidentConfirmed(int VPId)
        {
            _electionDummyRepositary.DeleteVillageVicePresident(VPId);
            return RedirectToAction("Index"); // Redirect to the desired action/page after deletion
        }



        [HttpGet("PollingBoothDetails")]
        public IActionResult PollingBoothDetails(int pollingBoothId)
        {
            var pollingBoothDetails = _electionDummyRepositary.FindPollingBoothPollingBoothAgentByPollingBoothId(pollingBoothId);

            if (pollingBoothDetails == null)
            {
                // Handle the case where no data is retrieved
                return NotFound(); // Or return a specific view or message
            }

            return View(pollingBoothDetails.FirstOrDefault());
        }
        [HttpGet]
        public IActionResult UpdatePollingBoothAgent(int pollingAgentId)
        {
            var pollingBoothAgent = new PollingBoothAgent();

            if (pollingAgentId > 0)
            {
                // Retrieve existing PollingBoothAgent data for editing
                pollingBoothAgent = _electionDummyRepositary.FindPollingBoothAgentById(pollingAgentId);
            }

            return View("UpdatePollingBoothAgent", pollingBoothAgent);
        }

        [HttpGet]
        public IActionResult InsertPollingBoothAgent(int pollingBoothId)
        {
            var pollingBoothAgent = new PollingBoothAgent
            {
                // Set the PollingBoothId for the view
                PollingBoothId = pollingBoothId
            };

            return View("InsertPollingBoothAgent", pollingBoothAgent);
        }

        [HttpPost]
        public IActionResult UpdatePollingBoothAgent(PollingBoothAgent pollingBoothAgent)
        {
            if (ModelState.IsValid)
            {
                if (pollingBoothAgent.PollingAgentId > 0)
                {
                    // Update existing record
                    _electionDummyRepositary.UpdatePollingBoothAgent(pollingBoothAgent);
                }

                int pollingBoothId = pollingBoothAgent.PollingBoothId;

                return RedirectToAction("PollingBoothDetails", new { pollingBoothId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                return View("UpdatePollingBoothAgent", pollingBoothAgent);
            }
        }

        [HttpPost]
        public IActionResult InsertPollingBoothAgent(PollingBoothAgent pollingBoothAgent)
        {
            if (ModelState.IsValid)
            {
                // Insert new record
                _electionDummyRepositary.InsertPollingBoothAgent(pollingBoothAgent);

                int pollingBoothId = pollingBoothAgent.PollingBoothId;

                return RedirectToAction("PollingBoothDetails", new { pollingBoothId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                return View("InsertPollingBoothAgent", pollingBoothAgent);
            }
        }


        [HttpGet]
        public IActionResult DeletePollingBoothAgent(int PollingAgentId)
        {
            var pollingBoothAgent = _electionDummyRepositary.FindPollingBoothAgentById(PollingAgentId);

            if (pollingBoothAgent == null)
            {
                // Handle the case where the VillageVicePresident with the given VPId is not found
                return NotFound();
            }

            return View(pollingBoothAgent);
        }

        [HttpPost]
        public IActionResult DeletePollingBoothAgentConfirmed(int PollingAgentId)
        {
            _electionDummyRepositary.DeletePollingBoothAgent(PollingAgentId);
            return RedirectToAction("Index"); // Redirect to the desired action/page after deletion
        }


        [HttpGet]
        public IActionResult UpdateVillagePresident(int PId)
        {
            var villagePresident = new VillagePresident();

            if (PId > 0)
            {
                // Retrieve existing BlockPresident data for editing
                villagePresident = _electionDummyRepositary.FindVillagePresidentByID(PId);
            }

            return View("UpdateVillagePresident", villagePresident);
        }

        [HttpGet]
        public IActionResult InsertVillagePresident(int VillageId)
        {
            var villagePresident = new VillagePresident
            {
                // Set the VillageId for the view
                VillageId = VillageId
            };

            return View("InsertVillagePresident", villagePresident);
        }

        [HttpPost]
        public IActionResult UpdateVillagePresident(VillagePresident villagePresident)
        {
            if (ModelState.IsValid)
            {
                if (villagePresident.PId > 0)
                {
                    // Update existing record
                    _electionDummyRepositary.UpdateVillagePresident(villagePresident);
                }

                int villageId = villagePresident.VillageId;

                return RedirectToAction("VillageDetails", new { villageId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int villageId = villagePresident.VillageId;

                return RedirectToAction("VillageDetails", new { villageId });
            }
        }

        [HttpPost]
        public IActionResult InsertVillagePresident(VillagePresident villagePresident)
        {
            if (ModelState.IsValid)
            {
                // Insert new record
                _electionDummyRepositary.InsertVillagePresident(villagePresident);

                int villageId = villagePresident.VillageId;

                return RedirectToAction("VillageDetails", new { villageId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int villageId = villagePresident.VillageId;

                return RedirectToAction("VillageDetails", new { villageId });
            }
        }

        [HttpGet]
        public IActionResult UpdateVillageVicePresident(int VPId)
        {
            var villageVicePresident = new VillageVicePresident();

            if (VPId > 0)
            {
                // Retrieve existing VillageVicePresident data for editing
                villageVicePresident = _electionDummyRepositary.FindVillageVicePresidentByID(VPId);
            }

            return View("UpdateVillageVicePresident", villageVicePresident);
        }

        [HttpGet]
        public IActionResult InsertVillageVicePresident(int VillageId)
        {
            var villageVicePresident = new VillageVicePresident
            {
                // Set the VillageId for the view
                VillageId = VillageId
            };

            return View("InsertVillageVicePresident", villageVicePresident);
        }

        [HttpPost]
        public IActionResult UpdateVillageVicePresident(VillageVicePresident villageVicePresident)
        {
            if (ModelState.IsValid)
            {
                if (villageVicePresident.VPId > 0)
                {
                    // Update existing record
                    _electionDummyRepositary.UpdateVillageVicePresident(villageVicePresident);
                }

                int villageId = villageVicePresident.VillageId;

                return RedirectToAction("VillageDetails", new { villageId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";

                int villageId = villageVicePresident.VillageId;

                return RedirectToAction("VillageDetails", new { villageId });
            }
        }

        [HttpPost]
        public IActionResult InsertVillageVicePresident(VillageVicePresident villageVicePresident)
        {
            if (ModelState.IsValid)
            {
                // Insert new record
                _electionDummyRepositary.InsertVillageVicePresident(villageVicePresident);

                int villageId = villageVicePresident.VillageId;

                return RedirectToAction("VillageDetails", new { villageId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int villageId = villageVicePresident.VillageId;

                return RedirectToAction("VillageDetails", new { villageId });
            }
        }


        [HttpGet]
        public IActionResult UpdatePanchayatPresident(int PId)
        {
            var panchayatPresident = new PanchayatPresident();

            if (PId > 0)
            {
                // Retrieve existing BlockPresident data for editing
                panchayatPresident = _electionDummyRepositary.FindPanchayatPresidentByID(PId);
            }

            return View("UpdatePanchayatPresident", panchayatPresident);
        }

        [HttpGet]
        public IActionResult InsertPanchayatPresident(int PanchayatId)
        {
            var panchayatPresident = new PanchayatPresident
            {
                // Set the PanchayatId for the view
                PanchayatId = PanchayatId
            };

            return View("InsertPanchayatPresident", panchayatPresident);
        }

        [HttpPost]
        public IActionResult UpdatePanchayatPresident(PanchayatPresident panchayatPresident)
        {
            if (ModelState.IsValid)
            {
                if (panchayatPresident.PId > 0)
                {
                    // Update existing record
                    _electionDummyRepositary.UpdatePanchayatPresident(panchayatPresident);
                }

                int panchayatId = panchayatPresident.PanchayatId;

                return RedirectToAction("PanchayatDetails", new { panchayatId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int panchayatId = panchayatPresident.PanchayatId;

                return RedirectToAction("PanchayatDetails", new { panchayatId });
            }
        }

        [HttpPost]
        public IActionResult InsertPanchayatPresident(PanchayatPresident panchayatPresident)
        {
            if (ModelState.IsValid)
            {
                // Insert new record
                _electionDummyRepositary.InsertPanchayatPresident(panchayatPresident);

                int panchayatId = panchayatPresident.PanchayatId;

                return RedirectToAction("PanchayatDetails", new { panchayatId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int panchayatId = panchayatPresident.PanchayatId;

                return RedirectToAction("PanchayatDetails", new { panchayatId });
            }
        }

        [HttpGet]
        public IActionResult UpdatePanchayatVicePresident(int VPId)
        {
            var panchayatVicePresident = new PanchayatVicePresident();

            if (VPId > 0)
            {
                // Retrieve existing PanchayatVicePresident data for editing
                panchayatVicePresident = _electionDummyRepositary.FindPanchayatVicePresidentByID(VPId);
            }

            return View("UpdatePanchayatVicePresident", panchayatVicePresident);
        }

        [HttpGet]
        public IActionResult InsertPanchayatVicePresident(int PanchayatId)
        {
            var panchayatVicePresident = new PanchayatVicePresident
            {
                // Set the PanchayatId for the view
                PanchayatId = PanchayatId
            };

            return View("InsertPanchayatVicePresident", panchayatVicePresident);
        }

        [HttpPost]
        public IActionResult UpdatePanchayatVicePresident(PanchayatVicePresident panchayatVicePresident)
        {
            if (ModelState.IsValid)
            {
                if (panchayatVicePresident.VPId > 0)
                {
                    // Update existing record
                    _electionDummyRepositary.UpdatePanchayatVicePresident(panchayatVicePresident);
                }

                int panchayatId = panchayatVicePresident.PanchayatId;

                return RedirectToAction("PanchayatDetails", new { panchayatId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int panchayatId = panchayatVicePresident.PanchayatId;

                return RedirectToAction("PanchayatDetails", new { panchayatId });
            }
        }

        [HttpPost]
        public IActionResult InsertPanchayatVicePresident(PanchayatVicePresident panchayatVicePresident)
        {
            if (ModelState.IsValid)
            {
                // Insert new record
                _electionDummyRepositary.InsertPanchayatVicePresident(panchayatVicePresident);

                int panchayatId = panchayatVicePresident.PanchayatId;

                return RedirectToAction("PanchayatDetails", new { panchayatId });
            }
            else
            {
                TempData["WarningMessage"] = "There was a warning message.";
                int panchayatId = panchayatVicePresident.PanchayatId;

                return RedirectToAction("PanchayatDetails", new { panchayatId });
            }
        }
    }










}