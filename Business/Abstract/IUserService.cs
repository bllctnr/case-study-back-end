using Core.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetById(int userId);
        User GetByEmail(string email);
        IDataResult<List<User>> GetAll();
        IResult Add(User user);
        IResult Delete(int userId);
        IResult Update(int userId, User user);

        List<OperationClaim> GetClaims(User user);
    }
}
