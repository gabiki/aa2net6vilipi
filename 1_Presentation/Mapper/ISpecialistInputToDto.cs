using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;
using AA2ApiNet6.Models;
using AA2ApiNET6._1_Presentation.Models;

namespace AA2ApiNet6.Mapper
{
    public interface ISpecialistInputToDto
    {
        SpecialistDto mapSpecialistInputToDto(SpecialistInputModel input);
        AppointmentDto mapAppointmentInputToDto(AppointmentInputModel appointmentInputModel);
    }
}
