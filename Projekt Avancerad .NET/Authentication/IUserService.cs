namespace Projekt_Avancerad_.NET.Authentication
{
    public interface IUserService
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<IEnumerable<string>> GetRolesAsync(string username);
    }
}
