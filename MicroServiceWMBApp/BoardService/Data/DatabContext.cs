using BoardService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.Data
{
    public class DatabContext : DbContext
    {
        public DatabContext()
        {

        }
        public DatabContext(DbContextOptions<DatabContext> options) : base(options)
        {
        }

        public DbSet<oprState> oprStates { get; set; }
        public DbSet<oprLGA> oprLGAs { get; set; }
        public DbSet<oprBoard> oprBoards { get; set; }
        public DbSet<oprBoardTemptbl> oprBoardTemptbls { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("data source=DESKTOP-J5TGKV5; initial catalog=BoardMicroserviceDB;persist security info=True;user id=sa;password=password1;");
        //}
        public async Task<int> SaveChanges()
        {
            int saved = -1;
            try
            {
                foreach (var ent in ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted || p.State == EntityState.Modified).ToList())
                {
                    Console.WriteLine("You are Modifying or deleting");
                }
                saved = await base.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"An error occur {ex.Message}");
            }
            return saved;
        }

    }
}
