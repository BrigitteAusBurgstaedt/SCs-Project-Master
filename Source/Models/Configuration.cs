
namespace SCsProjectMaster.Source.Models;

internal class Configuration
{
    public static Configuration Instance { get; set; } = new Configuration();

    public ICollection<FolderPreset> FolderPresets { get; set; } = new List<FolderPreset>()
    {
        new FolderPreset()
        {
            Name = "Preset 1",
            Root = new FolderInfo()
            {
                FolderName = "root",
                Children = new List<FolderInfo>() {
                    new FolderInfo() {
                        FolderName = "photo",
                        Children = new List<FolderInfo>() {
                            new FolderInfo() {
                                FolderName = "cam a"
                            },
                            new FolderInfo() {
                                FolderName = "cam b"
                            },
                            new FolderInfo() {
                                FolderName = "cam c"
                            }
                        }
                    },
                    new FolderInfo() {
                        FolderName = "video"
                    },
                    new FolderInfo() {
                        FolderName = "result"
                    }
                }
            }
        }
    };
    public ICollection<List<InvoiceItem>> InvoiceItemsPresets { get; set; } = new List<List<InvoiceItem>>();
    public Dictionary<string, string> CategorieNamesAndPaths { get; set; } = new Dictionary<string, string>();
    public string ArchivePath { get; set; }
    public ICollection<string> Types { get; set; } = new List<string>() { "Video", "Fotografie", "Livestreaming", "Web Entwicklung", "Journalismus" };
    public string FileName { get; set; } = "config.json";
    public Employee User { get; set; }

    private Configuration()
    {
        CategorieNamesAndPaths.Add("Aktiv", "");
        CategorieNamesAndPaths.Add("Pausiert", "");
        CategorieNamesAndPaths.Add("Archiv", "");
    }

}
