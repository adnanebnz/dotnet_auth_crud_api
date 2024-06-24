using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace DotNetApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PersonController : ControllerBase
{
    private readonly MyDbContext _context;

    public PersonController(MyDbContext context)
    {
        _context = context;
    }

    // GET: api/person
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonModel>>> Get()
    {
        return Ok(await _context.People.ToListAsync());
    }

    // GET api/person/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonModel>> Get(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    // POST api/person
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PersonModel person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
    }

    // PUT api/person/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] PersonModel person)
    {
        if (id != person.Id)
        {
            return BadRequest();
        }

        _context.Entry(person).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.People.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE api/person/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}