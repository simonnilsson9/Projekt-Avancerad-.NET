
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Projekt_Avancerad_.NET.Data;
using Projekt_Avancerad_.NET.Interfaces;
using System.Reflection.Metadata.Ecma335;
namespace Projekt_Avancerad_.NET.Services

{
    public class CustomerRepository : ICustomer
    {
        private AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<Customer> Add(Customer entity)
        {
            var result = await _appDbContext.Customers.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        
        public async Task<Customer> Delete(int id)
        {
            var result = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (result != null)
            {
                _appDbContext.Customers.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _appDbContext.Customers.Include(a => a.Appointments).ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _appDbContext.Customers.Include(a => a.Appointments).FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Update(Customer entity)
        {
            var result = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == entity.CustomerId);
            if (result != null)
            {
                result.Name = entity.Name;
                result.Email = entity.Email;
                result.Phone = entity.Phone;
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> WeeklyAppointment()
        {
            var today = DateTime.Today;
            int dayOfWeek = (int)today.DayOfWeek;
            int daysToMonday = (dayOfWeek == 0 ? 6 : dayOfWeek - 1); // Om det är söndag (0), vill vi gå 6 dagar tillbaka. Annars, dagar till måndag.
            var startOfWeek = today.AddDays(-daysToMonday);
            var endOfWeek = startOfWeek.AddDays(6);

            return await _appDbContext.Customers
                .Where(c => c.Appointments
                .Any(a => a.Date >= startOfWeek && a.Date <= endOfWeek))
                .ToListAsync();
        }

        public async Task<int> AppointmentCount(int customerId, int weekNumber, int year)
        {
            var firstDayOfYear = new DateTime(year, 1, 1);
            var days = (int)firstDayOfYear.DayOfWeek - 1;
            var firstMonday = firstDayOfYear.AddDays(days > 0 ? (7 - days) : 0);

            var startOfWeek = firstMonday.AddDays((weekNumber - 1) * 7);
            var endOfWeek = startOfWeek.AddDays(6);

            return await _appDbContext.Appointments
                .Where(a => a.CustomerId == customerId 
                && a.Date >= startOfWeek && a.Date <= endOfWeek)
                .CountAsync();
        }
    }
}
