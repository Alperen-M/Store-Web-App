using FluentValidation;
using WebApplication1.Dtos;

namespace WebApplication1.Validators
{
    public class StoreUpdateDtoValidator : AbstractValidator<StoreUpdateDto>
    {
        public StoreUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Mağaza adı zorunludur.")
                .MaximumLength(100).WithMessage("Mağaza adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Konum zorunludur.")
                .MaximumLength(200).WithMessage("Konum en fazla 200 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
