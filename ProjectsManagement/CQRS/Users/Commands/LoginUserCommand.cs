using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories;
using ProjectsManagement.Services;
using ProjectsManagement.Specification.UserSpecifications;


namespace ProjectsManagement.CQRS.Users.Commands
{
    public record LoginUserCommand(LoginRequestDTO loginRequestDTO) : IRequest<ResultDTO>;

    public class LoginRequestDTO 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    };

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultDTO>
    {
        IRepository<User> _userRepository;
        IMediator _mediator;

        public LoginUserCommandHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var spec = new UserSpecification(request.loginRequestDTO.Email);
            var user = await _userRepository.First(spec.Criteria);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.loginRequestDTO.Password, user.PasswordHash))
            {
                return ResultDTO.Faliure("Email or Password is incorrect");
            }

            var token = TokenGenerator.GenerateToken(user.ID.ToString(), user.FullName, "1");

            return ResultDTO.Sucess(token, "User Login Successfully!");
        }

    }
}
