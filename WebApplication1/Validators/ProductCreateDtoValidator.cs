using FluentValidation;
using WebApplication1.Models;

namespace WebApplication1.Validators
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı zorunludur.")
                .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olabilir.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Ürün adı sadece harf ve boşluk içerebilir.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Fiyat zorunludur.")
                .GreaterThan(0).WithMessage("Fiyat pozitif bir sayı olmalıdır.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
