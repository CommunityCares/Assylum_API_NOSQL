using Api_ComminityCares.Models;
using Api_ComminityCares.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ComminityCares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        
        private readonly DonorService _donorService;

        public DonorController(DonorService donorService)
        {
            _donorService = donorService;
            
        }
            

        [HttpGet]
        public async Task<List<Api_ComminityCares.Models.Donor>> Get() =>
            await _donorService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Api_ComminityCares.Models.Donor>> Get(int id)
        {
            var donor = await _donorService.GetAsync(id);

            if (donor is null)
            {
                return NotFound();
            }

            return donor;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Api_ComminityCares.Models.Donor newDonor)
        {
            await _donorService.CreateAsync(newDonor);

            return CreatedAtAction(nameof(Get), new { id = newDonor.Id }, newDonor);
        }


        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(int id, Api_ComminityCares.Models.Donor updatedDonor)
        {
            var donor = await _donorService.GetAsync(id);

            if (donor is null)
            {
                return NotFound();
            }

            updatedDonor.Id = donor.Id;

            await _donorService.UpdateAsync(id, updatedDonor);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var donor = await _donorService.GetAsync(id);

            if (donor is null)
            {
                return NotFound();
            }

            await _donorService.RemoveAsync(id);

            return NoContent();
        }
    }
}
