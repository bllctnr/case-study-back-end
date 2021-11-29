using Entities.Concrete;
using System.Collections.Generic;

namespace Core.Utilities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
