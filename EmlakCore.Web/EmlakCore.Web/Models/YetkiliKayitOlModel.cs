using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Models
{
    public class YetkiliKayitOlModel
    {
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string SifreTekrar { get; set; }
        public string YetkiliAd { get; set; }
        public string YetkiliSoyad { get; set; }
        public string IsyeriAdi { get; set; }
        public string AdresIl { get; set; }
        public string AdresIlce { get; set; }
        public string AdresDetay { get; set; }
        public string TelefonNo { get; set; }
        public string Fax { get; set; }
    }
    public class YetkiliKayitOlModelVadilator : AbstractValidator<YetkiliKayitOlModel>
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
        public YetkiliKayitOlModelVadilator()
        {
            RuleFor(x => x.Eposta).NotNull().EmailAddress().MinimumLength(10).MaximumLength(45);
            RuleFor(x => x.Sifre).NotNull().MinimumLength(5).MaximumLength(45).Must(IsPasswordValid).Equal(x=>x.SifreTekrar);
            RuleFor(x => x.YetkiliAd).NotNull().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.YetkiliSoyad).NotNull().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.IsyeriAdi).NotNull().MinimumLength(5).MaximumLength(45);
            RuleFor(x => x.AdresIl).NotNull().MinimumLength(3).MaximumLength(45);
            RuleFor(x => x.AdresIlce).NotNull().MinimumLength(3).MaximumLength(45);
            RuleFor(x => x.AdresDetay).NotNull().MinimumLength(10).MaximumLength(500);
            RuleFor(x => x.TelefonNo).NotNull().MinimumLength(10).MaximumLength(10);
            RuleFor(x => x.Fax).MinimumLength(13).MaximumLength(13);
        }
    }
}
