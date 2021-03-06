using CreatureCreator.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.MySqlProvider.EntityFrameworkCore.Contexts
{
    public class HotfixesDbContext : DbContext
    {
        string _connectionString;
        public HotfixesDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Pomelo MySQL
            // optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));

            // Oracle MySQL
            optionsBuilder.UseMySQL(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NpcModelItemSlotDisplayInfo>(entity =>
            {
                entity.ToTable("npc_model_item_slot_display_info");
            });
            modelBuilder.Entity<CreatureDisplayInfoExtra>(entity =>
            {
                entity.ToTable("creature_display_info_extra");
            });
            modelBuilder.Entity<CreatureDisplayInfoOption>(entity =>
            {
                entity.ToTable("creature_display_info_option");
            });
            modelBuilder.Entity<HotfixData>(entity =>
            {
                entity.ToTable("hotfix_data");
            });
            modelBuilder.Entity<CreatureDisplayInfo>(entity =>
            {
                entity.ToTable("creature_display_info");
            });
        }
    }
}
