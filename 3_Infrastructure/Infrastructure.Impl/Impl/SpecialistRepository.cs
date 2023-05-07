using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Contracts;
using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;
using AA2ApiNET6._3_Infrastructure.Infrastructure.Impl.Data;

namespace AA2ApiNET6._3_Infrastructure.Infrastructure.Impl.Impl
{
    public class SpecialistRepository : ISpecialistRepository
    {
        private readonly ILogger<SpecialistRepository> _logger;

        private readonly IDataBaseService _dataBaseService;

        public SpecialistRepository(ILogger<SpecialistRepository> logger, IDataBaseService dataBaseService)
        {
            _logger = logger;
            _dataBaseService = dataBaseService;
        }

        public bool AddSpecialist(SpecialistRepositoryModel specialist)
        {
            try
            {
                var dbresponse = _dataBaseService.AddSpecialistDb(specialist);
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

        public bool DeleteSpecialist(int id)
        {
            try
            {
                var dbresponse = _dataBaseService.DeleteSpecialistDb(id);
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

        public List<SpecialistRepositoryModel> GetSpecialists()
        {
            try
            {
                var dbresponse = _dataBaseService.GetSPecialistsDb();

                if (dbresponse.Count == 0)
                {
                    return new List<SpecialistRepositoryModel>();
                }
                else
                {
                    return dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new List<SpecialistRepositoryModel>();
            }
        }

        public SpecialistRepositoryModel GetSingleSpecialist(int id)
        {
            try
            {
                var dbresponse = _dataBaseService.GetSingleSpecialistDb(id);

                if (dbresponse == null)
                {
                    return new SpecialistRepositoryModel();
                }
                else
                {
                    return dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new SpecialistRepositoryModel();
            }
        }

        public SpecialistRepositoryModel UpdateSpecialist(int id, SpecialistRepositoryModel specialist)
        {
            try
            {
                var dbresponse = _dataBaseService.UpdateSpecialistDb(id, specialist);
                if (dbresponse.Id < 1)
                {
                    return new SpecialistRepositoryModel();
                }
                else
                {
                    return dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new SpecialistRepositoryModel();

            }
        }

        //Appointments
        public List<AppointmentRepositoryModel> GetAppointmentsRepository(int id)
        {
            try
            {
                var dbresponse = _dataBaseService.GetAppointmentsSpecialistDb(id);

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

        public bool DeleteAppointment(int idSpecialist, int idAppointment)
        {
            try
            {
                var dbresponse = _dataBaseService.DeleteAppointmentDb(idSpecialist, idAppointment);
                if (dbresponse)
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

        public AppointmentRepositoryModel UpdateAppointment(int idSpecialist, int idAppointment, AppointmentRepositoryModel appointmentRepository)
        {
            try
            {
                var dbresponse = _dataBaseService.UpdateAppointmentDb(idSpecialist, idAppointment, appointmentRepository);
                if (dbresponse.Id < 1)
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


