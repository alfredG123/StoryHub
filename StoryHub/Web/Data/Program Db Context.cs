using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class ProgramDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>
        /// Create an object to communicate with the database
        /// </summary>
        /// <param name="options"></param>
        public ProgramDbContext(DbContextOptions<ProgramDbContext> options)
            : base(options)
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Set up database table, Story Data
        /// </summary>
        public DbSet<StoryModel> StoryData { get; set; }

        /// <summary>
        /// Set up database table, Character Data
        /// </summary>
        public DbSet<CharacterModel> CharacterData { get; set; }

        /// <summary>
        /// Set up database table, Character Data
        /// </summary>
        public DbSet<CustomFieldModel> CustomFieldData { get; set; }

        /// <summary>
        /// Set up database table, Plot Data
        /// </summary>
        public DbSet<PlotModel> PlotData { get; set; }

        /// <summary>
        /// Set up database table, Reference Data
        /// </summary>
        public DbSet<PlotModel> ReferenceData { get; set; }
    }
}
