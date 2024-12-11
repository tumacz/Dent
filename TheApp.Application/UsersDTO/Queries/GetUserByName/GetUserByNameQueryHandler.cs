using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Application.DataTransferObjects;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.UsersDTO.Queries.GetUserByName
{
    public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserIdentityManagerRepository _repository;

        public GetUserByNameQueryHandler(IMapper mapper, IUserIdentityManagerRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UserDTO> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByName(request.UserName);
            
            var dtos = _mapper.Map<UserDTO>(user);

            return dtos;
        }
    }
}
