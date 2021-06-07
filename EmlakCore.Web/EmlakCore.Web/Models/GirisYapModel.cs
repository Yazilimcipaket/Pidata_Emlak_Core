using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Models
{
    public class GirisYapModel
    {
        public GirisYapModel()
        {
            ReturnUrl = "/";
        }
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class GirisYapModelValidator: AbstractValidator<GirisYapModel>
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
        public GirisYapModelValidator()
        {
            RuleFor(x => x.Eposta).EmailAddress().MinimumLength(10).MaximumLength(45).NotNull();
            RuleFor(x => x.Sifre).MinimumLength(6).NotNull().Must(IsPasswordValid);
        }
    }
}
