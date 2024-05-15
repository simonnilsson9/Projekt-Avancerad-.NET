using Models.Models;

namespace Projekt_Avancerad_.NET.Interfaces
{
    public interface IAppointment : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsForMonth(int year, int month, int companyId);
        Task<IEnumerable<Appointment>> GetAppointmentsForWeek(int year, int week, int compandyId);


    }
}
