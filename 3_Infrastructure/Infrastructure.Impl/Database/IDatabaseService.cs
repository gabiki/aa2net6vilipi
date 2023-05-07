using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;

namespace AA2ApiNET6._3_Infrastructure.Infrastructure.Impl.Data
{
    public interface IDataBaseService
    {
        //Specialist
        List<SpecialistRepositoryModel> GetSPecialistsDb();
        bool AddSpecialistDb(SpecialistRepositoryModel specialist);
        SpecialistRepositoryModel GetSingleSpecialistDb(int id);
        SpecialistRepositoryModel UpdateSpecialistDb(int id, SpecialistRepositoryModel specialist);
        bool DeleteSpecialistDb(int id);

        //Patient
        List<PatientRepositoryModel> GetPatientsDb();
        bool AddPatienttDb(PatientRepositoryModel patient);
        PatientRepositoryModel GetSinglePatientDb(int id);
        PatientRepositoryModel UpdatePatientDb(int id, PatientRepositoryModel patient);
        bool DeletePatientDb(int id);

        //Appointment
        bool AddAppointmentDb(int id, string speciality);
        List<AppointmentRepositoryModel> GetAppointmentsPatientDb(int id);
        AppointmentRepositoryModel GetSingleAppointmentPatientDb(int idPatient, int idAppointment);
        
        List<AppointmentRepositoryModel> GetAppointmentsSpecialistDb(int id);

        bool DeleteAppointmentDb(int idSpecilaist, int idAppointment);

        AppointmentRepositoryModel UpdateAppointmentDb(int idSpecialist, int idAppointment, AppointmentRepositoryModel appointment);

    }
}
