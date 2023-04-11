using EntityLayer;
using FluentValidation;

namespace SigortamNetUI.Validations
{

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.TCKN).NotEmpty().WithMessage("Tc Kimlik alanı boş olamaz.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş olamaz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyisim alanı boş olamaz.");
            RuleFor(x => x.Birthdate).NotEmpty().WithMessage("Doğum tarihi alanı boş olamaz.");
           
        }
    }
}
