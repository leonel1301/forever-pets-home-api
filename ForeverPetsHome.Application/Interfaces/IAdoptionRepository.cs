using ForeverPetsHome.Domain;

namespace ForeverPetsHome.Application.Interfaces
{
    public interface IAdoptionRepository
    {
        Task<Adoption> AddAdoption(Adoption adoption);
        Task<Adoption> GetAdoptionById(int adoptionId);
        Task UpdateAdoption(Adoption adoption);
        Task DeleteAdoption(int adoptionId);
    }
}