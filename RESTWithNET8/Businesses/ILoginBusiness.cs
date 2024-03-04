using RESTWithNET8.Data.ValueObjects;

namespace RESTWithNET8.Businesses
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);

        TokenVO ValidateCredentials(TokenVO token);

        bool RevokeToken(string username);
    }
}
