using InventoryManagementNewVision.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementNewVision.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            Database.SetCommandTimeout(300); // Set timeout to 5 minutes
        }

        #region Settings Configs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductInfo>()
                .HasOne(e => e.catagory)
                .WithMany()
                .HasForeignKey(e => e.catagoryId)
                .OnDelete(DeleteBehavior.Restrict);            

           
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).createdAt = DateTime.UtcNow;
                    ((Base)entity.Entity).createdBy = currentUsername;
                }
                else
                {
                    entity.Property("createdAt").IsModified = false;
                    entity.Property("createdBy").IsModified = false;
                    ((Base)entity.Entity).updatedAt = DateTime.UtcNow;
                    ((Base)entity.Entity).updatedBy = currentUsername;
                }
            }
        }
        #endregion

        public DbSet<ProductInfo> productInfos { get; set; }
        public DbSet<Catagory> catagories { get; set; }
    }

}
