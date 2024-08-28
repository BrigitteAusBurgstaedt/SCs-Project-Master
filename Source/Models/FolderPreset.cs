using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCsProjectMaster.Source.Models;

public class FolderPreset
{
    public string Name { get; set; }
    public FolderInfo Root { get; set; } = new();
}
