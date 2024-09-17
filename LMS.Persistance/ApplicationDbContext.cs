﻿using Microsoft.EntityFrameworkCore;

namespace LMS.API
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
	}
}