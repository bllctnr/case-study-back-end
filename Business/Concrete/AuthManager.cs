using Business.Abstract;
using Business.Constants;
using Core.Results;
using Core.Utilities.JWT;
using Core.Utilities.Security.Hashing;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IResult Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            string test = passwordHash.ToString();
            _userService.Add(user);
            return new SuccessResult(Messages.UserRegistered);
        }

        public IResult Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
            if(userToCheck == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorResult(Messages.PasswordError);
            }

            var accessToken = CreateAccessToken(userToCheck);
            UserForLoginResponseDto result = new UserForLoginResponseDto
            {
                FirstName = userToCheck.FirstName,
                LastName = userToCheck.LastName,
                token = accessToken.Data.Token,
                expiration = accessToken.Data.Expiration
            };

            return new SuccessDataResult<UserForLoginResponseDto>(result, Messages.SuccessfulLogin);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IResult UserExist(string email)
        {
            if (_userService.GetByEmail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }

            return new SuccessResult();
        }
    }
}
