using HospitalAssessment.Server.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Xml.Linq;

namespace HospitalAssessment.Server.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext() { }
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientVisit> PatientVisits { get; set; }
        public DbSet<PatientProgressNote> PatientProgressNotes { get; set; }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Patient>(typeBuilder =>
            {
                typeBuilder.HasMany(pp => pp.Visits);
            });

            builder.Entity<PatientVisit>(typeBuilder =>
            {
                typeBuilder.HasKey(pp => pp.VisitId);
                typeBuilder.HasMany(pp => pp.ProgressNotes);
            });

            var baseDir = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            var tenantsData = File.ReadAllLines(Path.Combine(baseDir, "Data\\Seeds\\Tenants.csv"));
            builder.Entity<Tenant>(p =>
            {
                var seedTenants = tenantsData.Select((name, i) => new Tenant
                {
                    TenantId = i + 1,
                    TenantName = name
                });
                p.HasData(seedTenants);
            });

            var patientsData = File.ReadAllLines(Path.Combine(baseDir, "Data\\Seeds\\Patients.csv"));
            builder.Entity<Patient>(p =>
            {
                var seedPatients = patientsData.Select((row, i) =>
                {
                    var cells = row.Split(new[] { ',' }, StringSplitOptions.None);
                    return new Patient
                    {
                        PatientId = i + 1,
                        FirstName = cells[0],
                        LastName = cells[1],
                        State = cells[2],
                        City = cells[3],
                        TenantId = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false
                    };
                });
                
                p.HasData(seedPatients);
            });
        }
    }
}
