using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;

namespace AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AppointmentCreationDate { get; set; }
        public bool IsCompleted { get; set; }
        public decimal Price { get; set; }
        public string SpecialistComment { get; set; }
    }
}
