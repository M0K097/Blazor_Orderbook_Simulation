using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<LimitOrder> LimitOrders => Set<LimitOrder>();
    public DbSet<MarketOrder> MarketOrders => Set<MarketOrder>();

    public DbSet<Tradelog> TradeLogs => Set<Tradelog>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasDiscriminator<string>("OrderType")
            .HasValue<LimitOrder>("Limit")
            .HasValue<MarketOrder>("Market");

        modelBuilder.Entity<Tradelog>()
            .HasOne(t => t.Taker)
            .WithMany(o => o.TakerTrades)
            .HasForeignKey(t => t.taker_id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tradelog>()
            .HasOne(t => t.Maker)
            .WithMany(o => o.MakerTrades)
            .HasForeignKey(t => t.maker_id)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
