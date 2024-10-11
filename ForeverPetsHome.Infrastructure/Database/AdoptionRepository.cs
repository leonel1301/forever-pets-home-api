using ForeverPetsHome.Application.Interfaces;
using ForeverPetsHome.Domain;
using ForeverPetsHome.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForeverPetsHome.Infrastructure.Database
{
    public class AdoptionRepository : IAdoptionRepository
    {
        private readonly ApplicationDbContext _context;
        public AdoptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Adoption> AddAdoption(Adoption adoption)
        {
            _context.Add(adoption);
            await _context.SaveChangesAsync();
            return adoption;
        }

        public async Task DeleteAdoption(int adoptionId)
        {
            Adoption adoptionToRemove = _context.Adoptions.FirstOrDefault(a => a.Id == adoptionId);
            if (adoptionToRemove != null ) _context.Adoptions.Remove(adoptionToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<Adoption> GetAdoptionById(int adoptionId)
        {
            Adoption updatedAdoption = await _context.Adoptions.FirstOrDefaultAsync(a => a.Id == adoptionId);
            return updatedAdoption;
        }

        public async Task UpdateAdoption(Adoption adoption)
        {
            _context.Adoptions.Update(adoption);
            await _context.SaveChangesAsync();
        }
    }
}