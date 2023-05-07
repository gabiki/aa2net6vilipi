using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;
using AA2ApiNet6.Models;
using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;

namespace AA2ApiNET6._2_Domain.ServiceLibrary.Impl.Mapper
{
    public interface ISpecialistRepositoryModelToDto
    {
            SpecialistDto mapSpecialistRepositoryModelToDto(SpecialistRepositoryModel specialist);
            AppointmentDto mapAppointmentRepositoryModelToDto(AppointmentRepositoryModel Appointment);
    }
}
