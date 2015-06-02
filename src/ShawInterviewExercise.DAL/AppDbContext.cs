using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShawInterviewExercise.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("tempdb")
        {

        }

        public DbSet<Show> Shows { get; set; }
    }
}