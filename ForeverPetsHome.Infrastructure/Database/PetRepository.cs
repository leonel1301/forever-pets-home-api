using ForeverPetsHome.Application.Interfaces;
using ForeverPetsHome.Domain;
using ForeverPetsHome.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForeverPetsHome.Infrastructure.Database
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _context;
        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPet(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePet(int PetId)
        {
            Pet petToRemove = _context.Pets.FirstOrDefault(p => p.Id == PetId);
            if (petToRemove != null ) _context.Pets.Remove(petToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<Pet> GetPetById(int petId)
        {
            Pet updatedPet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == petId);
            return updatedPet;
        }

        public async Task UpdatePet(Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }
    }
}