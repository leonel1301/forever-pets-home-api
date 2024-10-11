using ForeverPetsHome.Application.Command.UserManagement;
using ForeverPetsHome.Application.Command.UserManagement.Request;
using ForeverPetsHome.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForeverPetsHome.Api.MapControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly PetsManagement _petManagement;
        public PetController(PetsManagement petsManagement)
        {
            _petManagement = petsManagement;
        }
        [Authorize]
        [HttpPost("createPet")]
        public async Task<Pet> Register(PetRequest petRequest)
        {
            Pet newPet = await _petManagement.CreatePet(petRequest);
            return newPet;
        }
        [Authorize]
        [HttpPost("updatePet/{petId}")]
        public async Task<Pet> Update(PetRequest petRequest, int petId)
        {
            Pet updatedPet = await _petManagement.UpdatePet(petRequest, petId);
            return updatedPet;
        }
        [Authorize]
        [HttpDelete("deletePet/{petId}")]
        public async Task<IActionResult> Delete(int petId)
        {
            await _petManagement.DeletePet(petId);
            return Ok($"La mascota con ID {petId} ha sido eliminada exitosamente.");
        }
    }
}