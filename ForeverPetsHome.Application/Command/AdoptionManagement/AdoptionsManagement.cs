using ForeverPetsHome.Application.Command.AdoptionManagement.Request;
using ForeverPetsHome.Application.Command.AdoptionManagement.Response;
using ForeverPetsHome.Application.Interfaces;
using ForeverPetsHome.Domain;

namespace ForeverPetsHome.Application.Command.AdoptionManagement
{
    public class AdoptionManagement
    {
        private readonly IAdoptionRepository _adoptionRepository;
        private readonly IUserRepository _userRepository;
        public AdoptionManagement(IAdoptionRepository adoptionRepository, IUserRepository userRepository)
        {
            _adoptionRepository = adoptionRepository;
            _userRepository = userRepository;
        }

        public async Task<AdoptionResponse> CreateAdoption(AdoptionRequest adoptionRequest)
        {
            var customer = await _userRepository.GetUserByIdAsync(adoptionRequest.CustomerId, EnumRoles.Customer);
            if (customer == null)
            {
                throw new Exception("El cliente no existe.");
            }
            var volunteer = await _userRepository.GetUserByIdAsync(adoptionRequest.VolunteerId, EnumRoles.Volunteer);
            if (volunteer == null)
            {
                throw new Exception("El voluntario no existe.");
            }
            Adoption newAdoption = new Adoption
            {
                PetId = adoptionRequest.PetId,
                CustomerId = adoptionRequest.CustomerId,
                VolunteerId = adoptionRequest.VolunteerId,
                AdoptionDate = DateTime.UtcNow,
                Customer = customer,
                Volunteer = volunteer
            };
            var adoption = await _adoptionRepository.AddAdoption(newAdoption);
            if (adoption != null)
            {
                AdoptionResponse response = new AdoptionResponse
                {
                    PetId = adoption.PetId,
                    CustomerId = adoption.CustomerId,
                    VolunteerId = adoption.VolunteerId,
                    AdoptionDate = adoption.AdoptionDate
                };
                return response;
            }
            throw new Exception("No se pudo crear la adopcion");
        }

        public async Task<AdoptionResponse> UpdateAdoption(AdoptionRequest adoptionRequest, int adoptionID)
        {
            var customer = await _userRepository.GetUserByIdAsync(adoptionRequest.CustomerId, EnumRoles.Customer);
            if (customer == null)
            {
                throw new Exception("El cliente no existe.");
            }
            var volunteer = await _userRepository.GetUserByIdAsync(adoptionRequest.VolunteerId, EnumRoles.Volunteer);
            if (volunteer == null)
            {
                throw new Exception("El voluntario no existe.");
            }
            var adoptionToUpdate = await _adoptionRepository.GetAdoptionById(adoptionID);
            adoptionToUpdate.PetId = adoptionRequest.PetId;
            adoptionToUpdate.CustomerId = adoptionRequest.CustomerId;
            adoptionToUpdate.VolunteerId = adoptionRequest.VolunteerId;
            adoptionToUpdate.AdoptionDate = DateTime.UtcNow;
            adoptionToUpdate.Customer = customer;
            adoptionToUpdate.Volunteer = volunteer;
            await _adoptionRepository.UpdateAdoption(adoptionToUpdate);
            var updatedAdoption = await _adoptionRepository.GetAdoptionById(adoptionID);
            if (updatedAdoption != null)
            {
                AdoptionResponse response = new AdoptionResponse
                {
                    PetId = updatedAdoption.PetId,
                    CustomerId = updatedAdoption.CustomerId,
                    VolunteerId = updatedAdoption.VolunteerId,
                    AdoptionDate = updatedAdoption.AdoptionDate
                };
                return response;
            }
            throw new Exception("No se pudo actualizar la adopcion");
        }
    }
}