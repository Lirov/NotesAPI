using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;
using NotesAPI.Repositories.Interfaces;

namespace NotesAPI.Repositories.Services
{
    public class NoteService : INoteService
    {

        private readonly NoteContext _context;

        public NoteService(NoteContext noteContext)
        {
            _context = noteContext;
        }

        public async Task<Note> CreateNoteAsync(Note note, string userId)
        {
            note.UserId = userId;
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task DeleteNoteAsync(int id, string userId)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null) return;

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync(string userId)
        {
            return await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<Note?> GetNoteByIdAsync(int id, string userId)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
        }

        public async Task UpdateNoteAsync(Note note, string userId)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == note.Id && n.UserId == userId);

            if (existingNote == null) return;

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;

            _context.Entry(existingNote).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> NoteExistsAsync(int id, string userId)
        {
            return await _context.Notes.AnyAsync(n => n.Id == id && n.UserId == userId);
        }
    }
}

