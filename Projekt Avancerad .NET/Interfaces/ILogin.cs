using Models.Models;

namespace Projekt_Avancerad_.NET.Interfaces
{
    public interface ILogin : IRepository<LoginInfo>
    {
        Task<int?> GetCustomerIdByEmail(string email);
        Task<int?> GetCompanyIdByEmail(string email);
        Task<LoginInfo> GetByEmail(string email);
        Task<LoginInfo> GetByCustomerId(int customerId);

    }
}
