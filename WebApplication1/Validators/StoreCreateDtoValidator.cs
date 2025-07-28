using FluentValidation;
using WebApplication1.Models;

namespace WebApplication1.Validators
{
    public class StoreCreateDtoValidator : AbstractValidator<StoreCreateDto>
    {
        public StoreCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Mağaza adı zorunludur.")
                .MaximumLength(100).WithMessage("Mağaza adı en fazla 100 karakter olabilir.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Mağaza adı sadece harf ve boşluk içerebilir.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Konum bilgisi zorunludur.")
                .MaximumLength(100).WithMessage("Konum en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleForEach(x => x.Products)
                .SetValidator(new ProductCreateDtoValidator());
        }
    }
}
