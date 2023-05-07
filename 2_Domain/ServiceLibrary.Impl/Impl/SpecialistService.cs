using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Contracts;
using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Contracts;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Impl.Mapper;
using AA2ApiNET6._3_Infrastructure.Infrastructure.Impl.Impl;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AA2ApiNET6._2_Domain.ServiceLibrary.Impl.Impl
{
    public class SpecialistService : ISpecialistService
    {
        private readonly ILogger<SpecialistService> _logger;
        private readonly ISpecialistRepository _specialistRepository;
        private readonly ISpecialistRepositoryModelToDto _specialistRepositoryModelToDto;

        public SpecialistService(ILogger<SpecialistService> logger, ISpecialistRepository specialistRepository, ISpecialistRepositoryModelToDto specialistRepositoryModelToDto)
        {
            _logger = logger;
            _specialistRepository = specialistRepository;
            _specialistRepositoryModelToDto = specialistRepositoryModelToDto;
        }

        public bool AddSpecialistDto(SpecialistDto specialistDto)
        {
            SpecialistRepositoryModel specialistRepository = new SpecialistRepositoryModel();
            //specialistRepository.Id = specialistDto.Id;
            specialistRepository.Name = specialistDto.Name;
            specialistRepository.LastName = specialistDto.LastName;
            specialistRepository.IsRetired = specialistDto.IsRetired;
            specialistRepository.Rating = specialistDto.Rating;
            specialistRepository.BirthDate = specialistDto.BirthDate;
            specialistRepository.Speciality = specialistDto.Speciality;
            specialistRepository.Email = specialistDto.Email;
            specialistRepository.Password = specialistDto.Password;

            bool specialistAdded = _specialistRepository.AddSpecialist(specialistRepository);

            if (specialistAdded == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteSpecialistDto(int id)
        {
            try
            {
                bool specialistDeleted = _specialistRepository.DeleteSpecialist(id);
                if (specialistDeleted == true)
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

        public SpecialistDto GetSpecialistDto(int id)
        {
            try
            {
                var singleSpecialistRepository = _specialistRepository.GetSingleSpecialist(id);

                if (singleSpecialistRepository == null)
                {
                    return new SpecialistDto();
                }
                else
                {
                    var SpecialistDto = new SpecialistDto();
                    SpecialistDto.Id = id;
                    SpecialistDto.Name = singleSpecialistRepository.Name;
                    SpecialistDto.LastName = singleSpecialistRepository.LastName;
                    SpecialistDto.IsRetired = singleSpecialistRepository.IsRetired;
                    SpecialistDto.Rating = singleSpecialistRepository.Rating;
                    SpecialistDto.BirthDate = singleSpecialistRepository.BirthDate;
                    SpecialistDto.Speciality = singleSpecialistRepository.Speciality;
                    SpecialistDto.Email = singleSpecialistRepository.Email;
                    SpecialistDto.Password = singleSpecialistRepository.Password;

                    return SpecialistDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new SpecialistDto();
            }
        }

        public List<SpecialistBasicInfo> GetSpecialistBasicInfoList(string param, string order)
        {
            try
            {
                List<SpecialistRepositoryModel> specialistsRepository = _specialistRepository.GetSpecialists();
                List<SpecialistBasicInfo> specialistsBasicInfo = MapSpecialistsRepositoryToSpecialistsBasicInfoList(specialistsRepository);
                if (specialistsBasicInfo.Count == 0)
                {
                    List<SpecialistBasicInfo> emptySpecialistsBasicInfo = new List<SpecialistBasicInfo>();
                    return emptySpecialistsBasicInfo;
                }
                else
                {
                    if (param == null || order == null)
                    {
                        return specialistsBasicInfo;
                    }
                    var prop = typeof(SpecialistBasicInfo).GetProperty(param);
                    if (prop == null) 
                    {
                        return specialistsBasicInfo;
                    }
                    else
                    {
                        if(order == "ASC")
                        {
                            var orderListASC = specialistsBasicInfo.OrderBy(x => x.GetType().GetProperty(param).GetValue(x, null)).ToList();
                            return orderListASC;
                        }
                        else
                        {
                            var orderListDESC = specialistsBasicInfo.OrderByDescending(x => x.GetType().GetProperty(param).GetValue(x, null)).ToList();
                            return orderListDESC;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new List<SpecialistBasicInfo>();
            }
        }

        public SpecialistDto UpdateSpecialistDto(int id, SpecialistDto specialistDto)
        {
            try
            {
                var specialistRepository = new SpecialistRepositoryModel();
                specialistRepository.Name = specialistDto.Name;
                specialistRepository.LastName = specialistDto.LastName;
                specialistRepository.IsRetired = specialistDto.IsRetired;
                specialistRepository.Rating = specialistDto.Rating;
                specialistRepository.BirthDate = specialistDto.BirthDate;
                specialistRepository.Speciality = specialistDto.Speciality;
                specialistRepository.Email = specialistDto.Email;
                specialistRepository.Password = specialistDto.Password;

                var specialistRepos = _specialistRepository.UpdateSpecialist(id, specialistRepository);
                var specilaistDtoChanged = _specialistRepositoryModelToDto.mapSpecialistRepositoryModelToDto(specialistRepos);
                if (specilaistDtoChanged.Id < 1)
                {
                    return new SpecialistDto();
                }
                else
                {
                    return specilaistDtoChanged;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new SpecialistDto();
            }
        }

        private List<SpecialistBasicInfo> MapSpecialistsRepositoryToSpecialistsBasicInfoList(List<SpecialistRepositoryModel> specialistsRepository)
        {
            List<SpecialistBasicInfo> specialistsBasicInfo = new List<SpecialistBasicInfo>();
            specialistsRepository.ForEach(e =>
            {
                var specialistBasicInfo = new SpecialistBasicInfo();
                specialistBasicInfo.Id = e.Id;
                specialistBasicInfo.Name = e.Name;
                specialistBasicInfo.LastName = e.LastName;
                specialistBasicInfo.Speciality = e.Speciality;

                specialistsBasicInfo.Add(specialistBasicInfo);
            });
            return specialistsBasicInfo;
        }

        //Appointments
        public List<AppointmentDto> GetAppointmentsDto(int id)
        {
            try
            {
                var appointmentList = _specialistRepository.GetAppointmentsRepository(id);

                if (appointmentList == null)
                {
                    return new List<AppointmentDto>();
                }
                else
                {
                    var appointmentsDto = new List<AppointmentDto>();
                    appointmentList.ForEach(appointment =>
                    {
                        var appointmentDto = new AppointmentDto()
                        {
                            Id = appointment.Id,
                            Name = appointment.Name,
                            AppointmentCreationDate = appointment.AppointmentCreationDate,
                            IsCompleted = appointment.IsCompleted,
                            Price = appointment.Price,
                            SpecialistComment = appointment.SpecialistComment                          
                        };
                        appointmentsDto.Add(appointmentDto);
                    });
                    return appointmentsDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new List<AppointmentDto>();
            }
        }

        public bool DeleteAppointment(int idSpecialist, int idAppointment)
        {
            try
            {
                bool appointmenttDeleted = _specialistRepository.DeleteAppointment(idSpecialist, idAppointment);
                if (appointmenttDeleted == true)
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

        public AppointmentDto UpdateAppointmentDto(int idSpecialist, int idAppointment, AppointmentDto appointmentDto)
        {
            try
            {
                var appointmentRepository = new AppointmentRepositoryModel();
                appointmentRepository.Price = appointmentDto.Price;
                appointmentRepository.SpecialistComment = appointmentDto.SpecialistComment;

                var appointmentRepos = _specialistRepository.UpdateAppointment(idSpecialist, idAppointment, appointmentRepository);
                var appointmentDtoChanged = _specialistRepositoryModelToDto.mapAppointmentRepositoryModelToDto(appointmentRepos);
                if (appointmentDtoChanged.Id < 1)
                {
                    return new AppointmentDto();
                }
                else
                {
                    return appointmentDtoChanged;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new AppointmentDto();
            }
        }

    }
}

