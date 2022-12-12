using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstASPNetCoreAPI.DatabaseAccess;
using MyFirstASPNetCoreAPI.Models;
using MyFirstASPNetCoreAPI.Models.DTO;
using Newtonsoft.Json;

namespace MyFirstASPNetCoreAPI.Controllers
{
    [Route("api/Pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        // GET: api/Pets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDTO>>> GetPets()
        {
            //get pets from Database
            List<Pet> lstPets = await Task.Run( () => PetsDA.GetPets());

            return lstPets.Select(x => PetToDTO(x)).ToList();
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDTO>> GetPet(int id)
        {
            var pet = await Task.Run(() => GetPetById(id));
            //var pet = await _context.Pets.FindAsync(id);

            if (pet == null || pet.Id == 0)
            {
                return NotFound();
            }

            return PetToDTO(pet);
        }

        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> PutPet(int id, PetDTO petDto)
        {
            if (id != petDto.Id)
            {
                return BadRequest();
            }

            //check if pet exists
            Pet pet = await Task.Run( () => GetPetById(id));
            if (pet == null || pet.Id == 0)
            {
                return NotFound();
            }

            pet.Id = petDto.Id;
            pet.Name = petDto.Name;
            pet.Alter = petDto.Alter;
            pet.Art = petDto.Art;
            pet.Rasse = petDto.Rasse;
            pet.Geimpft = petDto.Geimpft;
            pet.Geschlecht = petDto.Geschlecht;

            try
            {
                return await Task.Run(() => PetsDA.UpdatePet(pet));
            }
            catch (DbUpdateConcurrencyException dbuce)
            {
                return dbuce.InnerException.ToString();
            }

            return NoContent();
        }

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> PostPet(PetDTO petDto)
        {
            var pet = new Pet
            {
                Name = petDto.Name,
                Alter = petDto.Alter,
                Art = petDto.Art,
                Rasse = petDto.Rasse,
                Geimpft = petDto.Geimpft,
                Geschlecht = petDto.Geschlecht,
            };

            try
            {
                return await Task.Run(() => PetsDA.InsertPet(pet));
            }
            catch (DbUpdateConcurrencyException dbuce)
            {
                return dbuce.InnerException.ToString();
            }

            return NoContent();
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletePet(int id)
        {
            //check if pet exists
            Pet pet = await Task.Run(() => GetPetById(id));
            if (pet == null || pet.Id == 0)
            {
                return NotFound();
            }

            try
            {
                return await Task.Run(() => PetsDA.DeletePet(id));
            }
            catch (DbUpdateConcurrencyException dbuce)
            {
                return dbuce.InnerException.ToString();
            }

            return NoContent();
        }

        /// <summary>
        /// Converts Object of Pet into PetDTO and returns it.
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        private static PetDTO PetToDTO(Pet pet)
        {
            return new PetDTO ( pet.Id,
                                pet.Name,
                                pet.Alter,
                                pet.Art,
                                pet.Rasse,
                                pet.Geimpft,
                                pet.Geschlecht
                              );
        }

        private Pet GetPetById(int id)
        {
            return PetsDA.GetPetById(id);
        }
    }
}
