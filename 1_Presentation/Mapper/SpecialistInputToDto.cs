using AA2ApiNet6.Models;
using AA2ApiNET6._1_Presentation.Controllers;
using AA2ApiNET6._1_Presentation.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Contracts;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace AA2ApiNet6.Mapper
{
    public class SpecialistInputToDto : ISpecialistInputToDto
    {
        private readonly ILogger<SpecialistInputToDto> _logger;
        public SpecialistInputToDto(ILogger<SpecialistInputToDto> logger)
        {
            _logger = logger;
        }

        public SpecialistDto mapSpecialistInputToDto(SpecialistInputModel input)
        {
            try
            {
                if (!new EmailAddressAttribute().IsValid(input.Email))
                {
                    return new SpecialistDto();
                }

                var specialistDto = new SpecialistDto();
                specialistDto.Name = input.Name;
                specialistDto.LastName = input.LastName;
                specialistDto.IsRetired = bool.Parse(input.IsRetired);
                specialistDto.Rating = input.Rating.Contains('.') ? decimal.Parse(input.Rating.Replace('.', ',')) : decimal.Parse(input.Rating);
                specialistDto.BirthDate = DateTime.Parse(input.BirthDate);
                specialistDto.Speciality = input.Speciality;
                specialistDto.Email = input.Email;
                specialistDto.Password = input.Password;

                return specialistDto;
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new SpecialistDto();
            }
        }

        public AppointmentDto mapAppointmentInputToDto(AppointmentInputModel appointmentInputModel)
        {
            try
            {
                var inputDto = new AppointmentDto();
                inputDto.Price = appointmentInputModel.Price.Contains('.') ? decimal.Parse(appointmentInputModel.Price.Replace('.', ',')) : decimal.Parse(appointmentInputModel.Price); ;
                inputDto.SpecialistComment = appointmentInputModel.Comment;
                return inputDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new AppointmentDto();
            }

        }

    }
}
