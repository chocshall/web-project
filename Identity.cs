using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
	// pass down the options to the base class
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
