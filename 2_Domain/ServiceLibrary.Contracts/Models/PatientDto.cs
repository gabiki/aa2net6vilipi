namespace AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsUnderage { get; set; }
        public bool isActive { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
