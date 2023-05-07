using AA2ApiNet6.Mapper;
using AA2ApiNet6.Models;
using AA2ApiNET6._1_Presentation.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Contracts;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Impl.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace AA2ApiNET6._1_Presentation.Controllers
{

    //add-migration migration1
    //update-database

    //USE master;
    //ALTER DATABASE[aa2PRE] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    //DROP DATABASE[aa2PRE];

    [Route("aa2")]
    [ApiController]
    public class SpecialistController : ControllerBase
    {
        private readonly ILogger<SpecialistController> _logger;
        private readonly ISpecialistService _specialistService;
        private readonly ISpecialistInputToDto _specialistInputToDto;

        public SpecialistController(ILogger<SpecialistController> logger, ISpecialistService specialistService, ISpecialistInputToDto specialistInputToDto)
        {
            _logger = logger;
            _specialistService = specialistService;
            _specialistInputToDto = specialistInputToDto;
        }

        [HttpGet("Specialists/{param}/{order}")]
        public ActionResult<List<SpecialistBasicInfo>> GetSpecialists(string? param, string? order)
        {
            try
            {
                List<SpecialistBasicInfo> specialists = _specialistService.GetSpecialistBasicInfoList(param, order);
                if (specialists.Count > 0)
                {
                    _logger.LogWarning("Method GetSpecialists invoked.");
                    return Ok(specialists);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest();
            }
        }

        [Authorize] // admin and specialist  => Basic ZGVtbzFAZGVtby5jb206ZGVtbzE=   email:password
        [HttpGet("Specialist/{id}")]
        public ActionResult<SpecialistDto> GetSpecialist(int id)
        {
            try
            {
                string specialistdValidated = HttpContext.User.Identity.Name;
                int intSpecialistValidated = 0;
                if (specialistdValidated != "admin")
                {
                    intSpecialistValidated = Int32.Parse(specialistdValidated);
                }

                if (id == intSpecialistValidated || specialistdValidated == "admin")
                {
                    SpecialistDto specialistDto = _specialistService.GetSpecialistDto(id);
                    if (specialistDto == null || specialistDto.Name == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(specialistDto);
                    }
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("/Specialist/AUTH/REGISTER")]
        public ActionResult AddSpecialist(SpecialistInputModel specialistInput)
        {
            try
            {
                _logger.LogWarning($"Method AddSpecialist invoked.");
                var specialistDto = _specialistInputToDto.mapSpecialistInputToDto(specialistInput);
                bool SpecialistStatus = _specialistService.AddSpecialistDto(specialistDto);
                if (SpecialistStatus)
                {
                    return Ok("Specialist added");
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest();
            }
        }

        [Authorize] // admin and specialist
        [HttpDelete("Specialist/{id}")]
        public ActionResult DeleteSpecialist(int id)
        {
            try
            {
                _logger.LogWarning($"Method deleteSpecialist invoked.");

                string specialistdValidated = HttpContext.User.Identity.Name;
                int intSpecialistValidated = 0;
                if (specialistdValidated != "admin")
                {
                    intSpecialistValidated = Int32.Parse(specialistdValidated);
                }

                if (id == intSpecialistValidated || specialistdValidated == "admin")
                {
                    var deletedSpecialist = _specialistService.DeleteSpecialistDto(id);
                    if (deletedSpecialist)
                    {
                        return Ok("Specialist removed");
                    }
                    else
                    {
                        return NotFound("This Specialist does not exist");
                    }
                } else
                {
                    return BadRequest("Error");
                } 
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest();
            }
        }

        [Authorize] // specialist
        [HttpPut("Specialist/{id}")]
        public ActionResult UpdateSpecialist(int id, SpecialistInputModel specialistInput) 
        {
            try
            {
                _logger.LogWarning("Method UpdateSpecialist invoked.");

                string specialistIdValidated = HttpContext.User.Identity.Name;
                if (id == Int32.Parse(specialistIdValidated))
                {
                    var specialistDto = _specialistInputToDto.mapSpecialistInputToDto(specialistInput);

                    var specilaistUpdatedto = _specialistService.UpdateSpecialistDto(id, specialistDto);
                    if (specilaistUpdatedto.Id >= 1)
                    {
                        return Ok("Specialist updated.");

                    }
                    else
                    {
                        return BadRequest("Error");
                    }
                } else 
                {
                    return BadRequest("Error");
                }
            } 
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest();
            }   
        }

        //Appointments
        [Authorize] //admin and specialist
        [HttpGet("Specialist/{id}/Appointments")]
        public ActionResult<List<AppointmentDto>> GetAppointments(int id)
        {
            try
            {
                _logger.LogWarning("Method GetAppointments invoked.");

                string patientIdValidated = HttpContext.User.Identity.Name;
                int intPatientIdValidated = 0;
                if (patientIdValidated != "admin")
                {
                    intPatientIdValidated = Int32.Parse(patientIdValidated);
                }

                if (id == intPatientIdValidated || patientIdValidated == "admin")
                {
                    List<AppointmentDto> appointments = _specialistService.GetAppointmentsDto(id);
                    if (appointments.Count > 0)
                    {
                        return Ok(appointments);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest();
            }
        }

        [Authorize] // admin and specialist
        [HttpDelete("Specialist/{idSpecialist}/Appointment/{idAppointment}")]
        public ActionResult DeleteAppointment(int idSpecialist, int idAppointment)
        {
            try
            {
                _logger.LogWarning($"Method DeleteAppointment invoked.");

                string specialistdValidated = HttpContext.User.Identity.Name;
                int intSpecialistValidated = 0;
                if (specialistdValidated != "admin")
                {
                    intSpecialistValidated = Int32.Parse(specialistdValidated);
                }

                if (idSpecialist == intSpecialistValidated || specialistdValidated == "admin")
                {
                    var deletedAppoinment = _specialistService.DeleteAppointment(idSpecialist, idAppointment);
                    if (deletedAppoinment)
                    {
                        return Ok("Appoinment removed");
                    }
                    else
                    {
                        return NotFound("This Appoinment does not exist");
                    }
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest();
            }
        }

        [Authorize] // specialist
        [HttpPut("Specialist/{idSpecialist}/Appointment/{idAppointment}")]
        public ActionResult UpdateAppointment(int idSpecialist, int idAppointment, AppointmentInputModel specialistInput)
        {
            try
            {
                _logger.LogWarning("Method UpdateAppointment invoked.");

                string specialistIdValidated = HttpContext.User.Identity.Name;
                if (idSpecialist == Int32.Parse(specialistIdValidated))
                {
                    var appointmentDTO = _specialistInputToDto.mapAppointmentInputToDto(specialistInput);
                    var appointmentUpdateDto = _specialistService.UpdateAppointmentDto(idSpecialist, idAppointment, appointmentDTO);

                    
                    if (appointmentUpdateDto.Id > 1)
                    {
                        return Ok("Appointment updated.");

                    }
                    else
                    {
                        return BadRequest("Error");
                    }
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest();
            }
        }
    }
}

