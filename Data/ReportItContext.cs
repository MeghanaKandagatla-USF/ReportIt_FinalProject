using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportIt.Models;

namespace ReportIt.Data
{
    public class ReportItContext : DbContext
    {
        public ReportItContext (DbContextOptions<ReportItContext> options)
            : base(options)
        {
        }

        public DbSet<ReportIt.Models.CrimeEvent> CrimeEvent { get; set; } = default!;
        public DbSet<ReportIt.Models.AuditLog> AuditLog { get; set; } = default!;
    }
}
