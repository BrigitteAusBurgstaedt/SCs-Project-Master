﻿using System;
using System.Collections.Generic;

namespace SCsProjectMaster.Source.Models;

public partial class TimeStopped
{
    public int StopwatchId { get; set; }

    public string EmployeeLogin { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Employee EmployeeLoginNavigation { get; set; } = null!;

    public virtual Stopwatch Stopwatch { get; set; } = null!;
}
