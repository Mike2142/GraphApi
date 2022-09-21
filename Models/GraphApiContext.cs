using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
            modelBuilder.Entity<Node>().ToTable("Node");
            modelBuilder.Entity<Edge>().ToTable("Edge");
        }
    }
}