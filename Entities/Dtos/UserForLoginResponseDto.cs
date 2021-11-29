using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserForLoginResponseDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
