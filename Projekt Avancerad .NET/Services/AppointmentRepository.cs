using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.VisualBasic;
using Models.Models;
using Projekt_Avancerad_.NET.Data;
using Projekt_Avancerad_.NET.Interfaces;
using System.Globalization;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Projekt_Avancerad_.NET.Services
{
    public class AppointmentRepository : IAppointment
    {
        private AppDbContext _dbContext;
        private readonly IAppointmentHistory _history;
        public AppointmentRepository(AppDbContext context, IAppointmentHistory history)
        {
            _dbContext = context;
            _history = history;

        }
        public async Task<Appointment> Add(Appointment entity)
        {
            var result = await _dbContext.Appointments.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var history = new AppointmentHistory
            {
                AppointmentId = entity.AppointmentId.ToString(),
                ChangeDate = DateTime.Now,
                ChangeType = "Add",
                Changes = JsonSerializer.Serialize(new
                {
                    AppointmentDate = entity.Date,
                    CustomerId = entity.CustomerId,
                    CompanyId = entity.CompanyId
                })
            };
            await _history.AddHistory(history);
            return result.Entity;
        }

        public async Task<Appointment> Delete(int id)
        {
            var result = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);
            if (result != null)
            {               
                _dbContext.Appointments.Remove(result);
                await _dbContext.SaveChangesAsync();
                var history = new AppointmentHistory
                {
                    AppointmentId = result.AppointmentId.ToString(),
                    ChangeDate = DateTime.Now,
                    ChangeType = "Delete",
                    Changes = JsonSerializer.Serialize(new
                    {
                        AppointmentDate = result.Date,
                        CustomerId = result.CustomerId,
                        CompanyId = result.CompanyId
                    })
                };

                // Lägg till historiken i databasen
                await _history.AddHistory(history);
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _dbContext.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetById(int id)
        {
            return await _dbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task<Appointment> Update(Appointment entity)
        {
            var result = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == entity.AppointmentId);
            if (result != null)
            {
                
                var history = new AppointmentHistory
                {
                    AppointmentId = result.AppointmentId.ToString(),
                    ChangeDate = DateTime.Now,
                    ChangeType = "Update",
                    Changes = JsonSerializer.Serialize(new
                    {
                        OriginalDate = result.Date,
                        NewDate = entity.Date,                        
                        NewCustomerId = entity.CustomerId,                        
                        NewCompanyId = entity.CompanyId
                    })
                };

                result.Date = entity.Date;
                result.CustomerId = entity.CustomerId;
                result.CompanyId = entity.CompanyId;
                
                await _history.AddHistory(history);
                
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsForMonth(int year, int month, int companyId)
        {
            return await _dbContext.Appointments
            .Where(a => a.Date.Year == year && a.Date.Month == month && a.CompanyId == companyId)
            .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsForWeek(int year, int week, int companyId)
        {
            var startDate = HelpMethods.FirstDateOfWeekStandard(year, week);
            var endDate = startDate.AddDays(6);

            return await _dbContext.Appointments.Where(a => a.Date >= startDate && a.Date <= endDate && a.CompanyId == companyId).ToListAsync();
        }

        
    }
}
