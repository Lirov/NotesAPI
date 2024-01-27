using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NotesAPI.Models
{
    public class NoteContext : IdentityDbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
    }
}
