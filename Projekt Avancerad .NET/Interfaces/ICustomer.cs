using Models.Models;

namespace Projekt_Avancerad_.NET.Interfaces
{
    public interface ICustomer : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> WeeklyAppointment();
        Task<int> AppointmentCount(int customerId, int weekNumber, int year);
    }
}
