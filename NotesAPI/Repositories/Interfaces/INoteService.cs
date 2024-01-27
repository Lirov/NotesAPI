using NotesAPI.Models;

namespace NotesAPI.Repositories.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetAllNotesAsync(string userId);
        Task<Note> GetNoteByIdAsync(int id, string userId);
        Task<Note> CreateNoteAsync(Note note, string userId);
        Task UpdateNoteAsync(Note note, string userId);
        Task DeleteNoteAsync(int id, string userId);
        Task<bool> NoteExistsAsync(int id, string userId);
    }
}
