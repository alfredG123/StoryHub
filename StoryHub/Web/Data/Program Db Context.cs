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
        public DbSet<StoryDataModel> StoryData { get; set; }

        /// <summary>
        /// Set up database table, Region Data
        /// </summary>
        public DbSet<RegionDataModel> RegionData { get; set; }

        /// <summary>
        /// Set up database table, Character Data
        /// </summary>
        public DbSet<CharacterDataModel> CharacterData { get; set; }

        /// <summary>
        /// Set up database table, Character Data
        /// </summary>
        public DbSet<CustomFieldDataModel> CustomFieldData { get; set; }

        /// <summary>
        /// Set up database table, Plot Data
        /// </summary>
        public DbSet<PlotDataModel> PlotData { get; set; }

        /// <summary>
        /// Set up database table, Reference Data
        /// </summary>
        public DbSet<ReferenceDataModel> ReferenceData { get; set; }

        /// <summary>
        ///  Set up database table, Character Reference Link Item
        /// </summary>
        public DbSet<CharacterReferenceLinkItemModel> CharacterReferenceLinkItem { get; set; }

        /// <summary>
        ///  Set up database table, Plot Character Link Item
        /// </summary>
        public DbSet<PlotCharacterLinkItemModel> PlotCharacterLinkItem { get; set; }

        /// <summary>
        ///  Set up database table, Plot Plot Link Item
        /// </summary>
        public DbSet<PlotPlotLinkItemModel> PlotPlotLinkItem { get; set; }

        /// <summary>
        ///  Set up database table, Plot Region Link Item
        /// </summary>
        public DbSet<PlotRegionLinkItemModel> PlotRegionLinkItem { get; set; }

        /// <summary>
        ///  Set up database table, Region Character Link Item
        /// </summary>
        public DbSet<RegionCharacterLinkItemModel> RegionCharacterLinkItem { get; set; }

        /// <summary>
        ///  Set up database table, Region Reference Link Item
        /// </summary>
        public DbSet<RegionReferenceLinkItemModel> RegionReferenceLinkItem { get; set; }

        /// <summary>
        ///  Set up database table, Region Region Link Item
        /// </summary>
        public DbSet<RegionRegionLinkItemModel> RegionRegionLinkItem { get; set; }

        /// <summary>
        ///  Set up database table, Story Character Link Item
        /// </summary>
        public DbSet<StoryCharacterLinkItemModel> StoryCharacterLinkItem { get; set; }

        /// <summary>
        ///  Set up database table, Story Custom Field Link Item
        /// </summary>
        public DbSet<StoryCustomFieldModel> StoryCustomField { get; set; }

        /// <summary>
        ///  Set up database table, Story Plot Link Item
        /// </summary>
        public DbSet<StoryPlotLinkItemModel> StoryPlotLinkItem { get; set; }

        /// <summary>
        ///  Set up database table, Story Region Link Item
        /// </summary>
        public DbSet<StoryRegionLinkItemModel> StoryRegionLinkItem { get; set; }
    }
}
