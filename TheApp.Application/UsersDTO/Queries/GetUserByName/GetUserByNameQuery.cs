using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Domain.Entities;

namespace TheApp.Application.UsersDTO.Queries.GetUserByName
{
    public class GetUserByNameQuery : IRequest<UserDTO>
    {
        public string UserName { get; set; }

        public GetUserByNameQuery(string userName) 
        {
            UserName = userName;
        }
    }
}
