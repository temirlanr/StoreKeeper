using Microsoft.AspNetCore.Mvc;
using StoreKeeper.Models;
using StoreKeeper.Repositories;
using StoreKeeper.Services;
using System;
using System.Collections.Generic;

namespace StoreKeeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService storageService;

        public StorageController(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Storage>> GetStorages()
        {
            try
            {
                var storages = storageService.GetStorages();

                return Ok(storages);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{storageId}")]
        public ActionResult<Storage> GetStorage(int storageId)
        {
            try
            {
                var storage = storageService.GetStorage(storageId);

                return Ok(storage);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Storage> AddStorage(Storage storage)
        {
            try
            {
                storageService.AddStorage(storage);
                return CreatedAtAction(nameof(GetStorage), new { storageId = storage.Id }, storage);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{storageId}")]
        public ActionResult DeleteStorage(int storageId)
        {
            try
            {
                storageService.DeleteStorage(storageId);

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
