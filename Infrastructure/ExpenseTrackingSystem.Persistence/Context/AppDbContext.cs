using ExpenseTrackingSystem.Domain.Entities;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Context
{
	public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Expense> Expenses { get; set; }
		public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
		public DbSet<PaymentSimulation> PaymentSimulations { get; set; }
		public DbSet<AuditLog> AuditLogs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
