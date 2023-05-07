using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;

namespace AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Contracts
{
    public interface ISpecialistService
    {
        bool AddSpecialistDto(SpecialistDto specialistDto);
        bool DeleteSpecialistDto(int id);
        SpecialistDto GetSpecialistDto(int id);
        List<SpecialistBasicInfo> GetSpecialistBasicInfoList(string param, string order);
        SpecialistDto UpdateSpecialistDto(int id, SpecialistDto specialistDto);

        //Appointments
        List<AppointmentDto> GetAppointmentsDto(int id);
        bool DeleteAppointment(int idSpecialist, int idAppointment);

        AppointmentDto UpdateAppointmentDto(int idSpecialist,int idAppointment, AppointmentDto appointmentDto);

    }
}

