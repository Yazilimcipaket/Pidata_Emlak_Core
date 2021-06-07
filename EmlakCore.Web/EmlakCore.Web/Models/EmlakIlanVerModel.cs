using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Models
{
    public class EmlakIlanVerModel
    {
        public List<EmlakTurleriModel> EmlakTurleri { get; set; }
        public int EmlakTuru { get; set; }
        public string Metrekare { get; set; }
        public string OdaSayisi { get; set; }
        public string Kat { get; set; }
        public string IsinmaTuru { get; set; }
        public string AdresIl { get; set; }
        public string AdresIlce { get; set; }
        public string AdresDetay { get; set; }
        public string Tip { get; set; }
        public decimal Fiyat { get; set; }

    }
    public class EmlakIlanVerVadilator : AbstractValidator<EmlakIlanVerModel>
    {
        public EmlakIlanVerVadilator()
        {
            RuleFor(x => x.EmlakTuru).NotNull();
            RuleFor(x => x.Metrekare).NotNull();
            RuleFor(x => x.OdaSayisi).NotNull();
            RuleFor(x => x.Kat).NotNull();
            RuleFor(x => x.IsinmaTuru).NotNull();
            RuleFor(x => x.AdresIl).NotNull();
            RuleFor(x => x.AdresDetay).NotNull();
            RuleFor(x => x.AdresIlce).NotNull();
            RuleFor(x => x.Tip).NotNull();
            RuleFor(x => x.Fiyat).NotNull();
        }
    }
}
