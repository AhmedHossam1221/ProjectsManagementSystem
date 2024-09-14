using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Users.Commands
{
    public record RegisterUserCommand(RegisterRequestDTO registerRequestDTO) : IRequest<ResultDTO>;
    
    public record RegisterRequestDTO(string FirstName, 
        string LastName, 
        string UserName,
        string Email,
        string PhoneNumber,
        string Password,
        string ConfirmPassword);

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDTO>
    {
        IRepository<User> _userRepository;
        IMediator _mediator;

        public RegisterUserCommandHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.FirstAsync(u => u.Email == request.registerRequestDTO.Email);

            if (result is not null)
            {
                return ResultDTO.Faliure("Email is already registered!");
            }

            result = await _userRepository.FirstAsync(user => user.UserName == request.registerRequestDTO.UserName);

            if (result is not null) 
            {
                return ResultDTO.Faliure("Username is alerady registered!");
            }

            var user = request.registerRequestDTO.MapOne<User>();

            user.PasswordHash = CreatePasswordHash(request.registerRequestDTO.Password);

            user = await _userRepository.AddAsync(user);

            await _userRepository.SaveChangesAsync();

            return ResultDTO.Sucess(user, "User registred successfully!");
        }

        private string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
