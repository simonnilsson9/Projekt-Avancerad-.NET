using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Projekt_Avancerad_.NET.Data;
using Projekt_Avancerad_.NET.Interfaces;

namespace Projekt_Avancerad_.NET.Services
{
    public class CompanyRepository : IRepository<Company>
    {
        private AppDbContext _appDbContext;
        public CompanyRepository(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<Company> Add(Company entity)
        {
            var result = await _appDbContext.Companies.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Company> Delete(int id)
        {
            var result = await _appDbContext.Companies.FirstOrDefaultAsync(c => c.CompanyId == id);
            if (result != null)
            {
                _appDbContext.Companies.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _appDbContext.Companies.ToListAsync();
        }

        public async Task<Company> GetById(int id)
        {
            return await _appDbContext.Companies.FirstOrDefaultAsync(c => c.CompanyId == id);
        }

        public async Task<Company> Update(Company entity)
        {
            var result = await _appDbContext.Companies.FirstOrDefaultAsync(c => c.CompanyId == entity.CompanyId);
            if (result != null)
            {
                result.Name = entity.Name;
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
