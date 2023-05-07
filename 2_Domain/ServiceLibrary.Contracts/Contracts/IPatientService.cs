using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;

namespace AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Contracts
{
    public interface IPatientService
    {
        bool AddPatientDto(PatientDto patienttDto);
        bool DeletePatientDto(int id);
        PatientDto GetPatientDto(int id);
        List<PatientDto> GetPatientsDto(string param, string order);
        PatientDto UpdatePatientDto(int id, PatientDto patienttDto);
        
        //Appointments
        bool AddAppointment(int id, string speciality);
        List<AppointmentDto> GetAppointmentsDto(int id);
        AppointmentDto GetAppointmentDto(int idPatient, int idAppointment);
        
    }
}
