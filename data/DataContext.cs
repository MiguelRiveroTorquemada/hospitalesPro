using Microsoft.EntityFrameworkCore;
using Clases;

namespace Data 

{
public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Pacientes>? Paciente { get; set; }
        public DbSet<Especialidades>? Especialidad { get; set; }
        public DbSet<Trabajadores>? Trabajadores { get; set; }
    }
}