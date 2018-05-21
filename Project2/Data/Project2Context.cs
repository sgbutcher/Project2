using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project2.Models
{
    public class Project2Context : DbContext
    {
        public Project2Context (DbContextOptions<Project2Context> options)
            : base(options)
        {
        }

        public DbSet<Project2.Models.Blurb> Blurb { get; set; }
    }
}
