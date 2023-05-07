using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;

namespace AA2ApiNET6._2_Domain.Infrastructure.Contracts.Contracts
{
    public interface IPatientRepository
    {
        List<PatientRepositoryModel> GetPatients();
        bool AddPatient(PatientRepositoryModel patient);
        PatientRepositoryModel GetSinglePatient(int id);
        bool DeletePatient(int id);
        PatientRepositoryModel UpdatePatient(int id, PatientRepositoryModel patient);

        //Appointment
        bool AddAppointment(int id, string specility);
        List<AppointmentRepositoryModel> GetAppointmentsRepository(int id);
        AppointmentRepositoryModel GetAppointmentRepository(int idPatient, int idAppointment);
    }
}
