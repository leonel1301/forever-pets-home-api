using ForeverPetsHome.Application.Command.AdoptionManagement;
using ForeverPetsHome.Application.Command.AdoptionManagement.Request;
using ForeverPetsHome.Application.Command.AdoptionManagement.Response;
using ForeverPetsHome.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForeverPetsHome.Api.MapControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionController : ControllerBase
    {
        private readonly AdoptionManagement _adoptionManagement;
        public AdoptionController(AdoptionManagement adoptionManagement)
        {
            _adoptionManagement = adoptionManagement;
        }

        [Authorize]
        [HttpPost("createAdoption")]
        public async Task<IActionResult> CreateAdoption(AdoptionRequest adoptionRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _adoptionManagement.CreateAdoption(adoptionRequest);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("updateAdoption/{adoptionId}")]
        public async Task<IActionResult> UpdateAdoption(AdoptionRequest adoptionRequest, int adoptionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AdoptionResponse adoption = await _adoptionManagement.UpdateAdoption(adoptionRequest, adoptionId);
            return Ok(adoption);
        }
    }
}