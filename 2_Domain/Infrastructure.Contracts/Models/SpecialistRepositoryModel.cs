using System.ComponentModel.DataAnnotations;

namespace AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models
{
    public class SpecialistRepositoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsRetired { get; set; }
        public decimal Rating { get; set; }
        public DateTime BirthDate { get; set; }
        public string Speciality { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
