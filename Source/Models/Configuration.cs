
namespace SCsProjectMaster.Source.Models;

internal class Configuration
{
    public static Configuration Instance { get; set; } = new Configuration();

    public ICollection<FolderPreset> FolderPresets { get; set; } = new List<FolderPreset>();
    public ICollection<List<InvoiceItem>> InvoiceItemsPresets { get; set; } = new List<List<InvoiceItem>>();
    public IDictionary<string, string> CategorieNamesAndPaths { get; set; } = new Dictionary<string, string>();
    public string ArchivePath { get; set; }
    public ICollection<string> Types { get; set; } = new List<string>() { "Video", "Fotografie", "Livestreaming", "Web Entwicklung", "Journalismus" };
    public string FileName { get; set; } = "config.json";
    public Employee User { get; set; }

    private Configuration() {
        CategorieNamesAndPaths.Add("Aktiv", "");
        CategorieNamesAndPaths.Add("Pausiert", "");
        CategorieNamesAndPaths.Add("Archiv", "");
    }

}
