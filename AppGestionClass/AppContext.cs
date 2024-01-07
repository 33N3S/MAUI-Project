using AppGestionClass.Models;
using Microsoft.EntityFrameworkCore;


namespace AppGestionClass
{
    public class AppContext : DbContext
    {
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Filiere> Filieres { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Proff> Proffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = "dbclasses1.db";
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
