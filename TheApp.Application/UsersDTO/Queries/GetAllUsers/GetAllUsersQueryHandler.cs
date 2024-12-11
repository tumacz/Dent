using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudiosQuery;
using TheApp.Application.DataTransferObjects;
using AutoMapper;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.UsersDTO.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserIdentityManagerRepository _repository;

        public GetAllUsersQueryHandler(IMapper mapper, IUserIdentityManagerRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _repository.GetAllUsers();
            var dto = _mapper.Map<IEnumerable<UserDTO>>(usersList);

            return dto;
        }
    }
}
