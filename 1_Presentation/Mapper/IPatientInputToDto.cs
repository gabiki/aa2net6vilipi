using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;
using AA2ApiNET6._1_Presentation.Models;

namespace AA2ApiNet6.Mapper
{
    public interface IPatientInputToDto
    {
        PatientDto mapPatientInputToDto(PatientInputModel input);
    }
}
