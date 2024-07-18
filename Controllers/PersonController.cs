using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using MyApiProject.Models;
using MyApiProject.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository _personRepository;

        public PersonController(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            var persons = await _personRepository.GetAllPersons();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await _personRepository.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Person person)
        {
            await _personRepository.AddPerson(person);
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            await _personRepository.UpdatePerson(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _personRepository.DeletePerson(id);
            return NoContent();
        }
    }
}