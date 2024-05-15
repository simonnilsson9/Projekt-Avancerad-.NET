
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Projekt_Avancerad_.NET.Data;
using Projekt_Avancerad_.NET.Interfaces;

namespace Projekt_Avancerad_.NET.Services
{
    public class LoginRepository : ILogin
    {
        private AppDbContext _appDbContext;
        public LoginRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<LoginInfo> Add(LoginInfo entity)
        {
            var result = await _appDbContext.LoginInfos.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<LoginInfo> Delete(int id)
        {
            var result = await _appDbContext.LoginInfos.FirstOrDefaultAsync(l => l.LoginId == id);
            if(result != null)
            {
                _appDbContext.LoginInfos.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<LoginInfo>> GetAll()
        {
            return await _appDbContext.LoginInfos.ToListAsync();
        }

        public async Task<LoginInfo> GetByCustomerId(int customerId)
        {
            return await _appDbContext.LoginInfos.FirstOrDefaultAsync(l => l.CustomerId == customerId);
        }
    

        public async Task<LoginInfo> GetByEmail(string email)
        {
            return await _appDbContext.LoginInfos.FirstOrDefaultAsync(l => l.EMail == email);
        }

        public async Task<LoginInfo> GetById(int id)
        {
            return await _appDbContext.LoginInfos.FirstOrDefaultAsync(l =>l.LoginId == id);
        }

        public async Task<int?> GetCompanyIdByEmail(string email)
        {
            var loginInfo = await _appDbContext.LoginInfos.FirstOrDefaultAsync(l => l.EMail == email);
            return loginInfo.CompanyId;
        }

        public async Task<int?> GetCustomerIdByEmail(string email)
        {
            var loginInfo = await _appDbContext.LoginInfos.FirstOrDefaultAsync(l => l.EMail == email);
            return loginInfo.CustomerId;
        }

        public async Task<LoginInfo> Update(LoginInfo entity)
        {
            var result = await _appDbContext.LoginInfos.FirstOrDefaultAsync(l => l.LoginId == entity.LoginId);
            if (result != null)
            {
                result.EMail = entity.EMail;
                result.CompanyId = entity.CompanyId;
                result.CustomerId = entity.CustomerId;
                result.Role = entity.Role;
                result.Password = entity.Password;

                return result;
            }
            return null;
        }
    }
}
