
using System.Xml.Serialization;

namespace SCsProjectMaster.Source.Models;

internal class Configuration
{
    public ICollection<FolderPreset> FolderPresets { get; set; }
    public ICollection<List<InvoiceItem>> InvoiceItemsPresets { get; set; }
    public ICollection<CategoryAndPath> CategoriesAndPaths { get; set; }
    public ICollection<string> Types { get; set; }
    public Employee User { get; set; }

    private Configuration()
    {
        //
        // Standard Config
        //

        // FolderPresets
        FolderPresets = new List<FolderPreset>()
        {
            new FolderPreset()
            {
                Name = "Preset 1",
                Root = new FolderInfo()
                {
                    FolderName = "project",
                    SubFolders = new List<FolderInfo>()
                    {
                        new FolderInfo()
                        {
                            FolderName = "photo",
                            SubFolders = new List<FolderInfo>()
                            {
                                new FolderInfo()
                                {
                                FolderName = "cam a"
                                },
                                new FolderInfo()
                                {
                                FolderName = "cam b"
                                },
                                new FolderInfo()
                                {
                                FolderName = "cam c"
                                }
                            }
                        },
                        new FolderInfo()
                        {
                            FolderName = "video"
                        },
                        new FolderInfo()
                        {
                            FolderName = "result"
                        }
                    }
                }
            }
        };

        // InvoiceItemsPresets
        InvoiceItemsPresets = new List<List<InvoiceItem>>();

        // CategoriesAndPaths
        // TODO Load Categories from DB
        CategoriesAndPaths = new List<CategoryAndPath>()
        {
            new()
            {
                Category = "Aktiv",
                Path = "C:\\Projekte\\Aktiv"
            },
            new()
            {
                Category = "Pausiert",
                Path = "C:\\Projekte\\Pausiert"
            },
            new()
            {
                Category = "Archiv",
                Path = "C:\\Projekte\\Archiv"
            },
        };

        // Types
        Types = new List<string>() { "Video", "Fotografie", "Livestreaming", "Web Entwicklung", "Journalismus" };

    }

    public static void LoadConfiguration(string path)
    {
        using TextReader reader = new StreamReader(path);
        XmlSerializer xmlSerializer = new(typeof(Configuration));
        Configuration config = (Configuration)xmlSerializer.Deserialize(reader);
        SetConfiguration(config);
    }

    public static void SaveConfiguration(string path)
    {
        Configuration config = GetConfiguration();
        using TextWriter writer = new StreamWriter(path);
        XmlSerializer xmlSerializer = new(typeof(Configuration));
        xmlSerializer.Serialize(writer, config);
    }

    public static Configuration GetConfiguration()
    {
        Configuration config;
        string configString = Preferences.Default.Get("Configuration", "Unknown");
        if (configString == "Unknown")
        {
            config = new Configuration();
            SetConfiguration(config);
            return config;
        }
        using TextReader reader = new StringReader(configString);
        XmlSerializer xmlSerializer = new(typeof(Configuration));
        config = (Configuration)xmlSerializer.Deserialize(reader);
        return config;
    }

    public static void SetConfiguration(Configuration config)
    {
        using TextWriter writer = new StringWriter();
        XmlSerializer xmlSerializer = new(typeof(Configuration));
        xmlSerializer.Serialize(writer, config);
        Preferences.Default.Set("Configuration", writer.ToString());
    }

    public ICollection<string> Categories()
    {
        ICollection<string> categories = new List<string>();
        foreach (CategoryAndPath cap in CategoriesAndPaths)
        {
            categories.Add(cap.Category);
        }
        return categories;
    }

}
