using Microsoft.EntityFrameworkCore;
using TinyTitan.Habits.API.Models;

namespace TinyTitan.Habits.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Habit> Habits { get; set; }
}