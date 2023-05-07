using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Contracts;
using AA2ApiNet6.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AA2ApiNET6._1_Presentation.Mapper;
using AA2ApiNET6._2_Domain.ServiceLibrary.Contracts.Models;
using AA2ApiNET6._2_Domain.ServiceLibrary.Impl.Impl;
using AA2ApiNet6.Models;
using AA2ApiNET6._1_Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using AA2ApiNET6._2_Domain.Infrastructure.Contracts.Models;

namespace AA2ApiNET6._1_Presentation.Controllers
{
    [Route("aa2")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientService _patientService;
        private readonly IPatientInputToDto _patientInputToDto;

        public PatientController(ILogger<PatientController> logger, IPatientService patientService, IPatientInputToDto patientInputToDto)
        {
            _logger = logger;
            _patientService = patientService;
            _patientInputToDto = patientInputToDto;
        }
        
        [Authorize] //admin
        [HttpGet("Patients/{param}/{order}")]
        public ActionResult<List<PatientDto>> GetPatients(string param, string order)
        {
            try
            {
                string admin = HttpContext.User.Identity.Name;
                if (admin == "admin")
                {
                    List<PatientDto> patients = _patientService.GetPatientsDto(param, order);
                    if (patients.Count > 0)
                    {
                        _logger.LogWarning("Method GetPatients invoked.");
                        return Ok(patients);
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
       
        [Authorize] //admin and patient
        [HttpGet("Patient/{id}")] 
        public ActionResult<PatientDto> GetPatient(int id)
        {
            try
            {
                string patientIdValidated = HttpContext.User.Identity.Name;
                int intPatientIdValidated = 0;
                if (patientIdValidated != "admin") 
                {
                    intPatientIdValidated = Int32.Parse(patientIdValidated);
                }

                if (id == intPatientIdValidated || patientIdValidated == "admin")
                {
                    PatientDto patientDto = _patientService.GetPatientDto(id);
                    if (patientDto == null || patientDto.Name == null)
                    {
                        return BadRequest("Error");
                    }
                    else
                    {
                        return Ok(patientDto);
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

        [HttpPost("/Patient/AUTH/REGISTER")]
        public ActionResult AddPatient(PatientInputModel patientInputModel)
        {
            try
            {
                _logger.LogWarning($"Method AddSpecialist invoked.");
                var patientDto = _patientInputToDto.mapPatientInputToDto(patientInputModel);
                bool patientStatus = _patientService.AddPatientDto(patientDto);
                if (patientStatus)
                {
                    return Ok("Patient added");
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

        [Authorize] //admin
        [HttpDelete("Patient/{id}")]
        public ActionResult DeletePatient(int id)
        {
            try
            {
                _logger.LogWarning($"Method DeletePatient invoked.");

                string admin = HttpContext.User.Identity.Name;
                if (admin == "admin")
                {
                    var deletedPatient = _patientService.DeletePatientDto(id);
                    if (deletedPatient)
                    {
                        return Ok("Patient removed");
                    }
                    else
                    {
                        return NotFound("This Patient does not exist");
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

        [Authorize] //admin and patient
        [HttpPut("Patient/{id}")]
        public ActionResult UpdatePatient(int id, PatientInputModel patientInput)
        {
            try
            {
                _logger.LogWarning("Method UpdatePatient invoked.");

                string patientIdValidated = HttpContext.User.Identity.Name;
                int intPatientIdValidated = 0;
                if (patientIdValidated != "admin")
                {
                    intPatientIdValidated = Int32.Parse(patientIdValidated);
                }

                if (id == intPatientIdValidated || patientIdValidated == "admin")
                {
                    {
                        var patientDto = _patientInputToDto.mapPatientInputToDto(patientInput);

                        var patientUpdatedto = _patientService.UpdatePatientDto(id, patientDto);
                        if (patientUpdatedto.Id >= 1)
                        {
                            return Ok("Patient updated.");

                        }
                        else
                        {
                            return BadRequest("Error");
                        }
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

         
        //Appointments
        [Authorize] //admin and patient
        [HttpPost("/Patient/{id}/Appointment/{speciality}")]
        public ActionResult AddAppointment(int id, string speciality)
        {
            try
            {
                _logger.LogWarning($"Method AddAppointment invoked.");

                string patientIdValidated = HttpContext.User.Identity.Name;
                int intPatientIdValidated = 0;
                if (patientIdValidated != "admin")
                {
                    intPatientIdValidated = Int32.Parse(patientIdValidated);
                }

                if (id == intPatientIdValidated || patientIdValidated == "admin")
                {
                    bool patientStatus = _patientService.AddAppointment(id, speciality);
                    if (patientStatus)
                    {
                        return Ok("Appointment created");
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


        [Authorize] //admin and patient
        [HttpGet("Patient/{id}/Appointments")]
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
                    List<AppointmentDto> appointments = _patientService.GetAppointmentsDto(id);
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

        [Authorize] //admin and patient
        [HttpGet("Patient/{idPatient}/Appointment/{idAppointment}")]
        public ActionResult<AppointmentDto> GetAppointment(int idPatient, int idAppointment)
        {
            try
            {
                string patientIdValidated = HttpContext.User.Identity.Name;
                int intPatientIdValidated = 0;
                if (patientIdValidated != "admin")
                {
                    intPatientIdValidated = Int32.Parse(patientIdValidated);
                }

                if (idPatient == intPatientIdValidated || patientIdValidated == "admin")
                {
                    AppointmentDto appointment = _patientService.GetAppointmentDto(idPatient, idAppointment);
                    if (appointment.Name != null)
                    {
                        _logger.LogWarning("Method GetPatients invoked.");
                        return Ok(appointment);
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
    }
}
