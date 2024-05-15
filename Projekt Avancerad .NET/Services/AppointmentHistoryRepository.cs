using Microsoft.EntityFrameworkCore;
using Models.Models;
using Projekt_Avancerad_.NET.Data;
using Projekt_Avancerad_.NET.Interfaces;

namespace Projekt_Avancerad_.NET.Services
{
    public class AppointmentHistoryRepository : IAppointmentHistory
    {
        private readonly AppDbContext _appDbContext;

        public AppointmentHistoryRepository(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task AddHistory(AppointmentHistory history)
        {
            await _appDbContext.AppointmentHistories.AddAsync(history);
            await _appDbContext.SaveChangesAsync();            
        }

        public async Task<IEnumerable<AppointmentHistory>> GetAllHistory()
        {
            return await _appDbContext.AppointmentHistories.ToListAsync();                                 
        }
    }
}
