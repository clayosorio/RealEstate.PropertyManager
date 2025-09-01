namespace PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication
{
    public interface IPasswordService
    {
        void CreatePasswordHash(string password, out string hash, out string salt);
        bool VerifyPasswordHash(string password, string storedHash, string storedSalt);
    }
}
