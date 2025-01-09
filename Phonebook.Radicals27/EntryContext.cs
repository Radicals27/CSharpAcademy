using Microsoft.EntityFrameworkCore;
using System;

namespace PhoneBook
{
	internal class EntryContext : DbContext
	{
		public DbSet<PhoneBookEntry> Entries { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
			optionsBuilder.UseSqlite($"Data Source = phoneBook.db");
	}
}