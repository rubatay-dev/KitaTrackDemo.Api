using Microsoft.EntityFrameworkCore;
using KitaTrackDemo.Api.Models.Entities;


namespace KitaTrackDemo.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public DbSet<TransactionType> TransactionTypes => Set<TransactionType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User FK
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany() 
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Delete transactions if User is deleted

        // TransactionType FK
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.TransactionType)
            .WithMany() 
            .HasForeignKey(t => t.TransactionTypeId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a Type if Transactions use it    

        // --------------------------------------------------------------------------------------------

        // Seed TransactionType data
        var cashInId = Guid.Parse("7f6e5d4c-3b2a-1a0b-9c8d-7e6f5a4b3c2d");
        var cashOutId = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d");

        modelBuilder.Entity<TransactionType>().HasData(
            new TransactionType { Id = cashInId, Name = "Cash In" },
            new TransactionType { Id = cashOutId, Name = "Cash Out" }
        );
    }
}
