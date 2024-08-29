
using System.Diagnostics;
using System.Xml.Serialization;

namespace SCsProjectMaster.Source.Models;

public class Configuration
{
    public List<FolderPreset> FolderPresets { get; set; }
    public List<CategoryAndPath> CategoriesAndPaths { get; set; }
    public List<string> Types { get; set; }

    private Configuration() { }

    public static void LoadConfiguration(string path)
    {
        XmlSerializer xmlSerializer = new(typeof(Configuration));
        Configuration config = (Configuration)xmlSerializer.Deserialize(File.OpenText(path));
        SetConfiguration(config);
    }

    public static void SaveConfiguration(string path)
    {
        string filePath = Path.Combine(path, "config.xml");
        Configuration config = GetConfiguration();
        using TextWriter writer = new StreamWriter(filePath);
        XmlSerializer xmlSerializer = new(typeof(Configuration));
        xmlSerializer.Serialize(writer, config);
    }

    public static Configuration GetConfiguration()
    {
        using TextReader reader = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Data", "config.xml"));
        XmlSerializer xmlSerializer = new(typeof(Configuration));
        Configuration config = (Configuration)xmlSerializer.Deserialize(reader);
        return config;
    }

    public static void SetConfiguration(Configuration config)
    {
        using TextWriter writer = File.CreateText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Data", "config.xml"));
        XmlSerializer xmlSerializer = new(typeof(Configuration));
        xmlSerializer.Serialize(writer, config);
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
