using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_Avancerad_.NET.Interfaces;
using System.Security.Claims;

namespace Projekt_Avancerad_.NET.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private IAppointment _appointmentRepository;       
        private ILogin _login;

        public CompanyController(IAppointment appointmentRepository, ILogin login)
        {                      
            _appointmentRepository = appointmentRepository;                        
            _login = login;
        }
                        
        [Authorize(Roles = "admin, company")]
        [HttpGet("Appointment/Month/{year}/{month}/{companyId}")]
        public async Task<IActionResult> GetAppointmentsForMonth(int year, int month, int companyId)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authenticated.");
                }

                var loggedInCompanyId = await _login.GetCompanyIdByEmail(email);
                var isAdmin = User.IsInRole("admin");

                if (!isAdmin && loggedInCompanyId != companyId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You don't have permission to view appointments for another company.");
                }

                var appointments = await _appointmentRepository.GetAppointmentsForMonth(year, month, companyId);
                return Ok(appointments);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to get data from database.");
            }
        }

        [Authorize(Roles = "admin, company")]
        [HttpGet("Appointment/Week/{year}/{week}/{companyId}")]
        public async Task<IActionResult> GetAppointmentsForWeek(int year, int week, int companyId)
        {
            try
            {   
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authenticated.");
                }

                var loggedInCompanyId = await _login.GetCompanyIdByEmail(email);
                var isAdmin = User.IsInRole("admin");

                if(!isAdmin && loggedInCompanyId != companyId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You don't have permission to view appointments for another company.");
                }

                var appointments = await _appointmentRepository.GetAppointmentsForWeek(year, week, companyId);
                return Ok(appointments);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to get data from database.");
            }
        }
    }
}
