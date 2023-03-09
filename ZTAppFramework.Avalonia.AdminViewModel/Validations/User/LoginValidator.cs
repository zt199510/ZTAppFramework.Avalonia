using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Avalonia.AdminViewModel.Validations
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("用户长度不能为空！")
                .Length(5, 30)
                .WithMessage("用户长度限制在5到30个字符之间！");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("密码长度不能为空！")
                .Length(5, 30)
                .WithMessage("密码长度限制在5到30个字符之间！");
        }
    }
}
