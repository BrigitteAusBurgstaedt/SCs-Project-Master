using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCsProjectMaster.Source.Models;

public class FolderInfo
{
    public string FolderName { get; set; }
    public ICollection<FolderInfo> SubFolders { get; set; } = new List<FolderInfo>();
}
