﻿using AspNetCoreODataIssues.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreODataIssues.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Person> People { get; set; }
}