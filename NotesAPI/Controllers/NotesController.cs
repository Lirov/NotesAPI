using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;
using NotesAPI.Repositories.Interfaces;
using System.Security.Claims;

namespace NotesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notes = await _noteService.GetAllNotesAsync(userId);
            return Ok(notes);

        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var note = await _noteService.GetNoteByIdAsync(id, userId);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _noteService.UpdateNoteAsync(note, userId);

            return NoContent();
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote(Note note)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            note.UserId = userId;
            var newNote = await _noteService.CreateNoteAsync(note, userId);

            return CreatedAtAction(nameof(GetNote), new { id = newNote.Id }, newNote);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _noteService.DeleteNoteAsync(id, userId);

            return NoContent();
        }

        private async Task<bool> NoteExists(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _noteService.NoteExistsAsync(id, userId);
        }
    }
}
