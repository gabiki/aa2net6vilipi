using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Contracts;
using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;
using AA2ApiNET6._3_Infrastructure.Infrastructure.Impl.Data;

namespace AA2ApiNET6._3_Infrastructure.Infrastructure.Impl.Impl
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ILogger<PatientRepository> _logger;

        private readonly IDataBaseService _dataBaseService;

        public PatientRepository(ILogger<PatientRepository> logger, IDataBaseService dataBaseService)
        {
            _logger = logger;
            _dataBaseService = dataBaseService;
        }

        public bool AddPatient(PatientRepositoryModel patient)
        {
            try
            {
                var dbresponse = _dataBaseService.AddPatienttDb(patient);
                if (dbresponse == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return false;
            }
        }

        public bool DeletePatient(int id)
        {
            try
            {
                var dbresponse = _dataBaseService.DeletePatientDb(id);
                if (dbresponse == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return false;
            }
        }

        public List<PatientRepositoryModel> GetPatients()
        {
            try
            {
                var dbresponse = _dataBaseService.GetPatientsDb();

                if (dbresponse.Count == 0)
                {
                    return new List<PatientRepositoryModel>();
                }
                else
                {
                    return dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new List<PatientRepositoryModel>();
            }
        }

        public PatientRepositoryModel GetSinglePatient(int id)
        {
            try
            {
                var dbresponse = _dataBaseService.GetSinglePatientDb(id);

                if (dbresponse == null)
                {
                    return new PatientRepositoryModel();
                }
                else
                {
                    return dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new PatientRepositoryModel();
            }
        }

        public PatientRepositoryModel UpdatePatient(int id, PatientRepositoryModel patient)
        {
            try
            {
                var dbresponse = _dataBaseService.UpdatePatientDb(id, patient);
                if (dbresponse.Id < 1)
                {
                    return new PatientRepositoryModel();
                }
                else
                {
                    return dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new PatientRepositoryModel();

            }
        }

        
        //Appointments
        public bool AddAppointment(int id, string specility)
        {
            try
            {
                var dbresponse = _dataBaseService.AddAppointmentDb(id, specility);
                if (dbresponse == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return false;
            }
        }

        public List<AppointmentRepositoryModel> GetAppointmentsRepository(int id)
        {
            try
            {
                var dbresponse = _dataBaseService.GetAppointmentsPatientDb(id);

                if (dbresponse.Count == 0)
                {
                    return new List<AppointmentRepositoryModel>();
                }
                else
                {
                    return dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new List<AppointmentRepositoryModel>();
            }
        }

        public AppointmentRepositoryModel GetAppointmentRepository(int idPatient, int idAppointment)
        {
            try
            {
                var dbresponse = _dataBaseService.GetSingleAppointmentPatientDb(idPatient, idAppointment);

                if (dbresponse.Id == 0)
                {
                    return new AppointmentRepositoryModel();
                }
                else
                {
                    return dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new AppointmentRepositoryModel();
            }
        }
    }
}
