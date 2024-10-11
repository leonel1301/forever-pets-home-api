using ForeverPetsHome.Application.Command.UserManagement.Request;
using ForeverPetsHome.Application.Interfaces;
using ForeverPetsHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ForeverPetsHome.Application.Command.UserManagement
{
    public class PetsManagement
    {
        private readonly IPetRepository _petRepository;
        public PetsManagement(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<Pet> CreatePet(PetRequest petRequest)
        {
            if (string.IsNullOrEmpty(petRequest.Names))
                throw new ArgumentException("El o los nombres de la mascota son requeridos");
            if (!petRequest.Age.HasValue)
                throw new ArgumentNullException(nameof(petRequest.Age), "La edad de la mascota es requerida");
            if (string.IsNullOrEmpty(petRequest.Breed))
                throw new ArgumentException("La raza de la mascota es requerida");
            if (petRequest.Type == null)
                throw new ArgumentNullException(nameof(petRequest.Type), "El tipo de mascota es requerido");

            Pet newPet = new Pet
            {
                Names = petRequest.Names,
                Age = petRequest.Age.Value,
                Breed = petRequest.Breed,
                Type = petRequest.Type.Value,
            };
            await _petRepository.AddPet(newPet);
            return newPet;
        }

        public async Task<Pet> UpdatePet(PetRequest petRequest, int petId)
        {
            if (string.IsNullOrEmpty(petRequest.Names))
                throw new ArgumentException("El o los nombres de la mascota son requeridos");
            if (!petRequest.Age.HasValue)
                throw new ArgumentNullException(nameof(petRequest.Age), "La edad de la mascota es requerida");
            if (string.IsNullOrEmpty(petRequest.Breed))
                throw new ArgumentException("La raza de la mascota es requerida");
            if (petRequest.Type == null)
                throw new ArgumentNullException(nameof(petRequest.Type), "El tipo de mascota es requerido");
            if (!petRequest.Status.HasValue)
                throw new ArgumentNullException(nameof(petRequest.Status), "El estado de la mascota es requerido");

            Pet petForUpdated = await _petRepository.GetPetById(petId);
            if (petForUpdated == null)
                throw new KeyNotFoundException($"La mascota con ID {petId} no fue encontrada.");

            petForUpdated.Age = petRequest.Age.Value;
            petForUpdated.Names = petRequest.Names;
            petForUpdated.Breed = petRequest.Breed;
            petForUpdated.Type = petRequest.Type.Value;
            petForUpdated.Status = petRequest.Status.Value;

            await _petRepository.UpdatePet(petForUpdated);

            return petForUpdated;
        }

        public async Task DeletePet(int petId)
        {
            Pet petForDeleted = await _petRepository.GetPetById(petId);
            if (petForDeleted == null)
                throw new KeyNotFoundException($"La mascota con ID {petId} no fue encontrada.");
            await _petRepository.DeletePet(petId);
        }
    }
}