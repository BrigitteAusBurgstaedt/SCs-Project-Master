﻿using System;
using System.Collections.Generic;

namespace SCs_Project_Master.Source.Logic;

public partial class Stopwatch
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int ProjectId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<TimeStopped> TimeStoppeds { get; set; } = new List<TimeStopped>();
}
