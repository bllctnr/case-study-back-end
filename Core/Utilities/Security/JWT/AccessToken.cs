using Entities.Dtos;
using System;

namespace Core.Utilities.JWT
{
    public class AccessToken : IDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
