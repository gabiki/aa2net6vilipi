using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;

namespace AA2ApiNET6._2_Domain.ServiceLibrary.Impl.Mapper
{
    public interface IPatientRepositoryModelToDto
    {
        PatientDto mapPatientRepositoryModelToDto(PatientRepositoryModel repositorymodel);
    }
}
