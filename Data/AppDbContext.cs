using Microsoft.EntityFrameworkCore;
using TinyTitanHabits.Models;

namespace TinyTitanHabits.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Habit> Habits { get; set; }
}