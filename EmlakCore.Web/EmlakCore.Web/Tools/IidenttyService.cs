﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Tools
{
    public interface IidenttyService
    {
        bool GirisYap(AccessToken accessToken);
    }
}
