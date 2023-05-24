
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Common;

public interface ISoftDelete
{
    bool IsDeleted { get; }
}