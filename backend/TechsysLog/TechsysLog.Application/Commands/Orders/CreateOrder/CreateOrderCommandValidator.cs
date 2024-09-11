using FluentValidation;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Application.Commands.Orders.CreateOrder
{

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Order number is required.")
                .GreaterThan(0).WithMessage("Order number must be greater than zero.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Value is required.")
                .GreaterThan(0).WithMessage("Value must be greater than zero.");
        }
    }

    public class OrderAddressValidator : AbstractValidator<OrderAddress>
    {
        public OrderAddressValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Address Id is required.");

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("CEP is required.");

            RuleFor(x => x.Rua)
                .NotEmpty().WithMessage("Street (Rua) is required.");

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("Number (Numero) is required.");

            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("Neighborhood (Bairro) is required.");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("City (Cidade) is required.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("State (Estado) is required.");
        }
    }

}
