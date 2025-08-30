using PropertyManager.Domain.Users.Entities;

namespace PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication
{
    public interface ITokenProvider
    {
        string Create(User user);
    }
}
