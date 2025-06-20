namespace CourseWork_DB_ONU.Data
{
    using CourseWork_DB_ONU.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Windows;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Organization>()
                .HasIndex(o => o.TaxNumber)
                .IsUnique();
            builder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();
            builder.UsePropertyAccessMode(PropertyAccessMode.Property);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Outlet>())
            {
                if (entry.State == EntityState.Added)
                {
                    // автоматично встановити адресу, якщо вона порожня
                    if (string.IsNullOrWhiteSpace(entry.Entity.Address))
                    {
                        entry.Entity.Address = "Адреса не вказана";
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    // логування зміни
                    MessageBox.Show($"Outlet '{entry.Entity.Name}' був змінений.");
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // заборонити видалення
                    if (entry.Entity.Type == Models.Enum.OutletType.OnlineStore)
                    {
                        throw new InvalidOperationException("Онлайн магазин видалити не можна видалити.");
                    }
                }
            }
            var result = base.SaveChanges();
            return result;
        }

    }
}
