using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace AA2ApiNET6._3_Infrastructure.Infrastructure.Impl.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseInMemoryDatabase("TestDb");
        }

        public DbSet<SpecialistRepositoryModel>? Specialists { get; set; }
        public DbSet<PatientRepositoryModel>? Patients { get; set; }
        public DbSet<AppointmentRepositoryModel>? Appointments { get; set; }
    }
}
