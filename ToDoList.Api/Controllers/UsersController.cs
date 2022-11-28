namespace ToDoList.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler<SignUp> _signUpHandler;
        private readonly ICommandHandler<SignIn> _signInHandler;
        private readonly ITokenStorage _tokenStorage;

        public UsersController
        (
            ICommandHandler<SignUp> signUpHandler, 
            ICommandHandler<SignIn> signInHandler, 
            ITokenStorage tokenStorage
        )
        {
            _signUpHandler = signUpHandler;
            _signInHandler = signInHandler;
            _tokenStorage = tokenStorage;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            command = command with { UserId = Guid.NewGuid() };
            await _signUpHandler.HandleAsync(command);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<JwtDto>> SingIn(SignIn command)
        {
            await _signInHandler.HandleAsync(command);
            var accessToken = _tokenStorage.Get();

            return Ok(accessToken);
        }
    }
}
