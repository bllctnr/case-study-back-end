using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserRegistered);
        }

        [SecuredOperation("admin")]
        public IResult Delete(int userId)
        {
            User userToDelete = _userDal.Get(u => u.Id == userId);
            _userDal.Delete(userToDelete);

            return new SuccessResult(Messages.RecordsDeleted);
        }

        [SecuredOperation("admin")]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.RecordsListed);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        [SecuredOperation("admin")]
        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId), Messages.RecordsListed);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public IResult Update(int userId, User user)
        {
            User userToUpdate = _userDal.Get(u => u.Id == userId);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            _userDal.Update(userToUpdate);

            return new SuccessResult(Messages.RecordsUpdated);
        }
    }
}
