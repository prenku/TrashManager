using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrashManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrashManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
           public DbSet<Appointment> Appointments  { get; set; }
           public DbSet<Appointment> Users     { get; set; }
    }
}
