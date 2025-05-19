using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Studentgram.Models;
using System.Text.Json;
namespace Studentgram.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Users> User { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; }= null!;
        public DbSet<Commentar> Comments {  get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {//Зв'язки для лайків 
            modelBuilder.Entity<Photo>()
          .Property(p => p.LikedUsers)
          .HasConversion(
              v => string.Join(",", v),  // В базу: "user1,user2,user3"
              v => new HashSet<string>(v.Split(',', StringSplitOptions.RemoveEmptyEntries)) // Повернути в `HashSet`
          )
          .Metadata.SetValueComparer(new ValueComparer<HashSet<string>>(
              (c1, c2) => c1.SetEquals(c2),  // Сравнение списков
              c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Хешування
              c => new HashSet<string>(c) // Клонування
          ));
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }
    }
}
