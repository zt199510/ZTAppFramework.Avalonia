using FluentValidation;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Avalonia.Stared.Validations
{
    public class GlobalValidator : ValidatorFactoryBase
    {

        private readonly IContainerProvider provider;

        public GlobalValidator(IContainerProvider provider)
        {
            this.provider = provider;
        }

        public override IValidator? CreateInstance(Type validatorType)
        {
            return provider.Resolve(validatorType) as IValidator;
        }

        public ValidationResult Validate<T>(T model)
        {
            return GetValidator<T>().Validate(model);
        }
    }
}
