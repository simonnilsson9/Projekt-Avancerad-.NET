using Models.Models;

namespace Projekt_Avancerad_.NET.Interfaces
{
    public interface IAppointmentHistory
    {
        Task AddHistory(AppointmentHistory history);
        Task<IEnumerable<AppointmentHistory>> GetAllHistory();
    }
}
