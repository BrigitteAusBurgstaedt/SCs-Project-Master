﻿using System;
using System.Collections.Generic;

namespace DatabaseTest;

public partial class ExternalUri
{
    public int ProjectId { get; set; }

    public int Id { get; set; }

    public string Uri { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
