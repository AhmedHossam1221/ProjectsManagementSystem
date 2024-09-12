using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.CQRS.Users.Commands;
using ProjectsManagement.Helpers;
using ProjectsManagement.ViewModels;
using ProjectsManagement.ViewModels.Auth;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResultViewModel> Register(RegisterRequestViewModel registerRequestViewModel)
        {
            var registerRequestDTO = registerRequestViewModel.MapOne<RegisterRequestDTO>();
            
            var resultDTO = await _mediator.Send(new RegisterUserCommand(registerRequestDTO));

            if (!resultDTO.IsSuccess) 
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        public async Task<ResultViewModel> Login(LoginRequestViewModel loginRequestViewModel)
        {
            var loginRequestDTO = loginRequestViewModel.MapOne<LoginRequestDTO>();

            var resultDTO = await _mediator.Send(new LoginUserCommand(loginRequestDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data, resultDTO.Message);
        }
    }
}
