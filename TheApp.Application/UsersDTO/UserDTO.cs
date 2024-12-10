using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Application.UsersDTO
{
    public class UserDTO
    {
        //public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        //public string UserName { get; set; } = default!;

        //public List<string> Roles { get; set; } = new List<string>();
    }
}
