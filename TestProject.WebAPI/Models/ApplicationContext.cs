using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.WebAPI.Models
{
  public class ApplicationContext : DbContext
  {
    public ApplicationContext()
    {

    }
    public ApplicationContext( DbContextOptions<ApplicationContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
  }
}
