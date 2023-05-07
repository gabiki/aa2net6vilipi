using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;

namespace AA2ApiNET6._2_Domain.ServiceLibrary.Impl.Mapper
{
    public class PatientRepositoryModelToDto : IPatientRepositoryModelToDto
    {
        private readonly ILogger<PatientRepositoryModelToDto> _logger;
        public PatientRepositoryModelToDto(ILogger<PatientRepositoryModelToDto> logger)
        {
            _logger = logger;
        }

        public PatientDto mapPatientRepositoryModelToDto(PatientRepositoryModel repositoryModel)
        {
            try
            {
                var patientDto = new PatientDto();
                patientDto.Id = repositoryModel.Id;
                patientDto.Name = repositoryModel.Name;
                patientDto.LastName = repositoryModel.LastName;
                patientDto.Gender = repositoryModel.Gender;
                patientDto.BirthDate = repositoryModel.BirthDate;
                patientDto.IsUnderage = repositoryModel.IsUnderage;
                patientDto.isActive = repositoryModel.isActive;
                patientDto.Password = repositoryModel.Password;
                patientDto.Email = repositoryModel.Email;

                return patientDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new PatientDto();
            }
        }
    }
}
