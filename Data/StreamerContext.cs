using Microsoft.EntityFrameworkCore;
using SS_API.Model;

namespace SS_API.Data
{
    /// <summary>
    /// Contexto de Banco de dados da Streamer.
    /// </summary>
    public class StreamerContext : DbContext
    {
        public StreamerContext(DbContextOptions<StreamerContext> option) : base(option) { }

        /// <summary>
        /// Conjunto do Banco de dados referente aos <seealso cref="Course"/>.
        /// </summary>
        public DbSet<Course> Courses { get; set; }
        
        /// <summary>
        /// Conjunto do Banco de dados referente aos <seealso cref="Project"/>.
        /// </summary>
        public DbSet<Project> Projects { get; set; }
    }
}