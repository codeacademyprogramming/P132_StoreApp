using FluentValidation;

namespace Store.Api.Admin.Dtos.AccountDtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().MaximumLength(14).MinimumLength(6);
            RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(20).MinimumLength(6);
        }
    }
}
