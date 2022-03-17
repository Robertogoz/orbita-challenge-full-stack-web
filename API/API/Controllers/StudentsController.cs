#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _context.Student.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.RA)
            {
                return BadRequest();
            }
            _context.Entry(student).State = EntityState.Modified;

            try
            {
            if (student.Email is "" or null)
            {
                return BadRequest("Invalid email: NULL");
            }

            bool emailValidator = student.Email.Contains("@hotmail.com") || student.Email.Contains("@outlook.com") || student.Email.Contains("@gmail.com");
            if (emailValidator == false) { return BadRequest("Invalid email:incorrect format"); }

            if (student.Name is "" or null)
            {
                return BadRequest("Invalid Name: NULL");
            }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Student.Add(student);
            try
            {
                if (StudentExists(student.RA))
                {
                    return BadRequest("Student already registered in this RA");
                }
                if (student.RA == 0 || student.RA > 9999999) //limit ra to 7 decimal places
                {
                    return BadRequest("Invalid RA: NULL or incorrect format");
                }

                if (student.Name is "" or null)
                {
                    return BadRequest("Invalid Name: NULL");
                }

                bool emailValidator = student.Email.Contains("@hotmail.com") || student.Email.Contains("@outlook.com") || student.Email.Contains("@gmail.com");
                if (emailValidator == false) { return BadRequest("Invalid email: Incorrect format"); }

                const int cpfMax = 14;
                if(student.CPF.Length > cpfMax)
                {
                    student.CPF = student.CPF.Substring(0, cpfMax);
                }

                if (student.CPF is "" or null || student.CPF.Length < cpfMax)
                {
                    return BadRequest("Invalid CPF: NULL or incorrect format");
                }
                if (StudentExistsByCPF(student.CPF))
                {
                    return BadRequest("Student already registered with that CPF");
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentExists(student.RA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudent", new { id = student.RA }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.RA == id);
        }

        private bool StudentExistsByCPF(string cpf) { return _context.Student.Any(e => e.CPF == cpf); }
    }     
}