using Core.Results;
using Core.Utilities.JWT;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IResult Register(UserForRegisterDto userForRegisterDto, string password);
        IResult Login(UserForLoginDto userForLoginDto);
        IResult UserExist(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
