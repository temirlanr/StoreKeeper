using Microsoft.AspNetCore.Mvc;
using StoreKeeper.Models;
using StoreKeeper.Repositories;
using StoreKeeper.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreKeeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService transferService;

        public TransferController(ITransferService transferService)
        {
            this.transferService = transferService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transfer>> GetTransfers()
        {
            try
            {
                var transfers = transferService.GetTransfers();
                return Ok(transfers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{transferId}")]
        public ActionResult<Transfer> GetTransfer(int transferId)
        {
            try
            {
                var transfer = transferService.GetTransfer(transferId);
                return Ok(transfer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Transfer> AddTransfer(Transfer transfer)
        {
            try
            {
                transferService.AddTransfer(transfer);
                return CreatedAtAction(nameof(GetTransfer), new { transferId = transfer.Id }, transfer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{transferId}")]
        public ActionResult DeleteTransfer(int transferId)
        {
            try
            {
                transferService.DeleteTransfer(transferId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
