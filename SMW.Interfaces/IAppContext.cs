﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW.Interfaces
{
 public   interface IAppContext
    {
        ICache Cache { get; set; }
    }
}
