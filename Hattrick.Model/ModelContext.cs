using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace Hattrick.Model
{
    public class ModelContext : DbContext
    {
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Position> Position { get; set; }

        private IConfiguration _configuration;
        private readonly string connectionString;

        public ModelContext(IConfiguration configuration)
            : base()
        {
            this._configuration = configuration;
        }

        public ModelContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Player>().HasMany(s => s.Positions).WithOne(s => s.Player)
                .HasForeignKey(s => s.PlayerId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._configuration.GetSection("ConnectionString")["SqlDb"]);
        }
    }
}