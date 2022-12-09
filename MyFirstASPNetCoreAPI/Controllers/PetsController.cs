using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstASPNetCoreAPI.Models;
using MyFirstASPNetCoreAPI.Models.DTO;

namespace MyFirstASPNetCoreAPI.Controllers
{
    [Route("api/Pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly PetContext _context;

        public PetsController(PetContext context)
        {
            _context = context;
        }

        // GET: api/Pets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDTO>>> GetPets()
        {
            //return await _context.Pets.ToListAsync();
            return await _context.Pets
                .Select(x => PetToDTO(x))
                .ToListAsync();
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDTO>> GetPet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return PetToDTO(pet);
        }

        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, PetDTO petDto)
        {
            if (id != petDto.Id)
            {
                return BadRequest();
            }

            //check if pet exists
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
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

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NoContent();
            }

            return NoContent();
        }

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PetDTO>> PostPet(PetDTO petDto)
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

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPet", new { id = pet.Id }, pet);
            return CreatedAtAction(
                nameof(GetPet),
                new { Id = pet.Id },
                PetToDTO(pet));
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.Id == id);
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
    }
}
