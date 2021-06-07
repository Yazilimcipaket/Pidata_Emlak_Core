using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Models
{
    public class KayitOlModel
    {
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string SifreTekrar { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string CepTelefonu { get; set; }
    }
    public class KayitOlModelVadilator : AbstractValidator<KayitOlModel>
    {
        private bool IsPasswordValid(string arg)
        {

            if (arg != null)
            {
                if (arg.Any(char.IsUpper) && arg.Any(char.IsLower) && arg.Any(char.IsNumber))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public KayitOlModelVadilator()
        {
            RuleFor(x => x.Eposta).NotNull().EmailAddress();
            RuleFor(x => x.Sifre).MinimumLength(6)
                .NotNull().WithMessage("Şifre Boş olamaz")
                .Must(IsPasswordValid).WithMessage("Sifre Bir büyük harf bir küçük harf birde rakam içermelidir.")
                .Equal(x=>x.SifreTekrar).WithMessage("Sifreler Eşleşmiyor");
            RuleFor(x => x.CepTelefonu).MaximumLength(10).MinimumLength(10).NotNull();
          
        }
    }
}
