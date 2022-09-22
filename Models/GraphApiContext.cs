using Microsoft.EntityFrameworkCore;

namespace GraphApi.Models
{
    public class GraphApiContext : DbContext
    {
        public GraphApiContext(DbContextOptions<GraphApiContext> options)
            : base(options)
        {
        }

        public DbSet<Node> Nodes { get; set; } = null!;
        public DbSet<Edge> Edges { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Edge>()
                    .HasOne(m => m.Src)
                    .WithMany(t => t.SrcEdges)
                    .HasForeignKey(m => m.SrcID)
                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Edge>()
                    .HasOne(m => m.Dest)
                    .WithMany(t => t.DestEdges)
                    .HasForeignKey(m => m.DestID)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}