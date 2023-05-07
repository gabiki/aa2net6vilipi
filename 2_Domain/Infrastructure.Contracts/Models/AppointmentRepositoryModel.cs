using System.ComponentModel.DataAnnotations;

namespace AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models
{
    public class AppointmentRepositoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AppointmentCreationDate { get; set; }
        public bool IsCompleted { get; set; } 
        public decimal Price { get; set; }
        public string SpecialistComment { get; set; }
        public SpecialistRepositoryModel Specialist { get; set; }
        public PatientRepositoryModel Patient { get; set; }
    }
}
