using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpleWebAPI.Models;

namespace simpleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              /*this method belongs to ControllerBase, 
              it creates an NotFoundResult that produces a Status404NotFound response.
              NotFoundResult is a sub-class of ActionResult
              */
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User User)
        {
            if (id != User.Id)
            {
                return BadRequest();
            }
            /*inform the DbContext that the User entity has been modified and needs to be updated in the database during the next call to SaveChangesAsync() or SaveChanges().*/
            _context.Entry(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'TodoContext.Users'  is null.");
          }
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = User.Id }, User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
