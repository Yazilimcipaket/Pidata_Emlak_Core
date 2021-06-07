using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Business.Tools
{
   public class Araclar
    {
        public static string MusteriNoUret(int tip)
        {
            Random random = new Random();
            int No = random.Next(10000, 99999);
            string KullaniNo = tip.ToString() + No.ToString();
            return KullaniNo;
        }
    }
}

