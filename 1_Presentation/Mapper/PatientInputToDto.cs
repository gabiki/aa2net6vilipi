using AA2ApiNet6.Mapper;
using AA2ApiNET6._1_Presentation.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace AA2ApiNET6._1_Presentation.Mapper
{
    public class PatientInputToDto : IPatientInputToDto
    {
        private readonly ILogger<PatientInputToDto> _logger;
        public PatientInputToDto(ILogger<PatientInputToDto> logger)
        {
            _logger = logger;
        }

        public PatientDto mapPatientInputToDto(PatientInputModel input)
        {
            try
            {
                if (!new EmailAddressAttribute().IsValid(input.Email))
                {
                    return new PatientDto();
                }

                var patientDto = new PatientDto();
                patientDto.Name = input.Name;
                patientDto.LastName = input.LastName;
                patientDto.BirthDate = DateTime.Parse(input.BirthDate);
                patientDto.IsUnderage = checkUnderAge(patientDto.BirthDate);
                patientDto.Gender= input.Gender;
                patientDto.isActive = true;
                patientDto.Email = input.Email;
                patientDto.Password = input.Password;

                return patientDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new PatientDto();

            }
        }

        private bool checkUnderAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
                age--;

            if (age < 18)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
