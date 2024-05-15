using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Projekt_Avancerad_.NET.DTO;
using Projekt_Avancerad_.NET.Interfaces;
using System.Security.Claims;

namespace Projekt_Avancerad_.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private ICustomer _customerRepository;
        private IAppointmentHistory _historyRepository;
        private ILogin _loginInfoRepository;
        private IAppointment _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentController(ICustomer customerRepo,  IMapper mapper, IAppointmentHistory appointmentHistory, ILogin login, IAppointment appointment)
        {
            _customerRepository = customerRepo;
            _mapper = mapper;            
            _historyRepository = appointmentHistory;
            _loginInfoRepository = login;
            _appointmentRepository = appointment;
        }

        [Authorize(Roles = "admin, customer, company")]
        [HttpPost("AddAppointment")]
        public async Task<IActionResult> AddAppointment(int customerID, int companyID, DateTime date)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authenticated.");
                }

                var loggedInId = await _loginInfoRepository.GetCustomerIdByEmail(email);
                var loggedInCompanyId = await _loginInfoRepository.GetCompanyIdByEmail(email);
                var isAdmin = User.IsInRole("admin");
                var isCompany = User.IsInRole("company");

                if (!isAdmin && !isCompany)
                {
                    if (loggedInId != customerID)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "You don't have permission to add appointment to other customers.");
                    }
                }

                if (isCompany && loggedInCompanyId != companyID)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You don't have permission to add appointment for another company.");
                }

                AppointmentDTO newAppointment = new AppointmentDTO
                {
                    Date = date,
                    CustomerId = customerID,
                    CompanyId = companyID
                };

                if (newAppointment == null)
                {
                    return BadRequest();
                }
                var appointment = _mapper.Map<Appointment>(newAppointment);
                var result = await _appointmentRepository.Add(appointment);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to put data to database.");
            }
        }

        [Authorize(Roles = "admin, customer, company")]
        [HttpPut("EditAppointment")]
        public async Task<IActionResult> EditAppointment(int appointmentId, DateTime date, int customerId, int companyId)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authenticated.");
                }

                var loggedInId = await _loginInfoRepository.GetCustomerIdByEmail(email);
                var loggedInCompanyId = await _loginInfoRepository.GetCompanyIdByEmail(email);
                var isAdmin = User.IsInRole("admin");
                var isCompany = User.IsInRole("company");

                if (!isAdmin && !isCompany)
                {
                    if (loggedInId != customerId)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "You don't have permission to edit appointments of other customers.");
                    }
                }

                if (isCompany && loggedInCompanyId != companyId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You don't have permission to edit appointments for another company.");
                }

                if (isCompany && loggedInCompanyId != companyId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You don't have permission to change the companyId.");
                }

                var result = await _appointmentRepository.GetById(appointmentId);
                if (result != null)
                {
                    Appointment updatedAppointment = new Appointment
                    {
                        AppointmentId = appointmentId,
                        Date = date,
                        CustomerId = customerId,
                        CompanyId = companyId
                    };

                    await _appointmentRepository.Update(updatedAppointment);
                    return Ok(updatedAppointment);
                }
                return NotFound($"Appointment with ID: {appointmentId} where not found.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to update data");
            }
        }

        [Authorize(Roles = "admin, customer, company")]
        [HttpDelete("DeleteAppointment/{appointmentId}")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int appointmentId)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(email))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authenticated.");
                }

                var loggedInId = await _loginInfoRepository.GetCustomerIdByEmail(email);
                var loggedInCompanyId = await _loginInfoRepository.GetCompanyIdByEmail(email);
                var isAdmin = User.IsInRole("admin");
                var isCompany = User.IsInRole("company");

                var appointmentToDelete = await _appointmentRepository.GetById(appointmentId);
                if (appointmentToDelete == null)
                {
                    return NotFound($"Appointment with ID: {appointmentId} where not found.");
                }

                if (!isAdmin && !isCompany)
                {
                    if (loggedInId != appointmentToDelete.CustomerId)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "You don't have permission to delete another customer's appointment.");
                    }
                }

                if (isCompany && loggedInCompanyId != appointmentToDelete.CompanyId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You don't have permission to delete appointments for another company.");
                }

                return await _appointmentRepository.Delete(appointmentId);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to delete data from database.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("ThisWeek")]
        public async Task<IActionResult> GetCustomersWithAppointmentsThisWeek()
        {
            try
            {
                var customers = await _customerRepository.WeeklyAppointment();
                var customersDTO = (customers.Select(c => _mapper.Map<CustomerDTO>(c)));
                if (customersDTO != null)
                {
                    return Ok(customersDTO);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database.");
            }
            
        }

        [Authorize(Roles = "admin")]
        [HttpGet("AppointmentCount/{customerId}/year/{year}/week/{weekNumber}")]
        public async Task<IActionResult> GetAppointmentsCountForCustomer(int customerId, int weekNumber, int year)
        {
            try
            {
                var count = await _customerRepository.AppointmentCount(customerId, weekNumber, year);
                return Ok(count);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("History")]
        public async Task<IActionResult> GetAllAppointmentHistory()
        {
            try
            {
                var allHistory = await _historyRepository.GetAllHistory();
                return Ok(allHistory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database.");
            }
            
        }
    }
}
