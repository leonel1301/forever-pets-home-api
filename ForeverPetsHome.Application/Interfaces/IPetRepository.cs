using ForeverPetsHome.Domain;

namespace ForeverPetsHome.Application.Interfaces
{
    public interface IPetRepository
    {
        Task AddPet(Pet pet);
        Task<Pet> GetPetById(int petId);
        Task UpdatePet(Pet pet);
        Task DeletePet(int PetId);
    }
}