using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCsProjectMaster.Source.Models;

internal class FolderInfo
{
    public string FolderName { get; set; }
    public ICollection<FolderInfo> Children { get; set; } = new List<FolderInfo>();
}
