using System;
using System.Collections.Generic;

namespace Dn6Poc.TravalApi.Models
{
    public partial class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
