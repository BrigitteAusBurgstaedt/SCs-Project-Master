using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace SCsProjectMaster.Source.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public string Categorie { get; set; }

    public string ProjectFileName { get; set; } = null!;

    public string OtherInformation { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? Deadline { get; set; }

    public int? TotalWorkload { get; set; }

    public string ContactPerson { get; set; } = null!;

    public int CustomerId { get; set; }

    public string InvoiceNumber { get; set; }

    public string PreviewUri { get; set; }

    public string CloudUri { get; set; }

    public string ShowcasePictureUri { get; set; }

    public string ShowcaseFolderUri { get; set; }

    public string DokumentsFolderUri { get; set; }

    public string CloudVideoUri { get; set; }

    public string CloudPreviewFolderUri { get; set; }

    public string TextInfo1 { get; set; }

    public string TextInfo2 { get; set; }

    public virtual Employee ContactPersonNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<ExternalUri> ExternalUris { get; set; } = new List<ExternalUri>();

    public virtual Invoice InvoiceNumberNavigation { get; set; }

    public virtual ICollection<Stopwatch> Stopwatches { get; set; } = new List<Stopwatch>();

    public virtual ICollection<Employee> EmployeeLogins { get; set; } = new List<Employee>();

    /// <summary>  
    /// Archiviert ein Projektverzeichnis in eine ZIP-Datei.  
    /// </summary>  
    /// <exception cref="Exception">Wird ausgelöst, wenn die Kategorie "Archiv" nicht gefunden wird oder ohne Pfad ist.</exception>  
    /// <exception cref="Exception">Wird ausgelöst, wenn die angegebene Kategorie nicht gefunden wird oder ohne Pfad ist.</exception>  
    /// <exception cref="DirectoryNotFoundException">Wird ausgelöst, wenn das Projektverzeichnis nicht gefunden wird.</exception>  
    public void Archive()
    {
        Configuration config = Configuration.GetConfiguration();
        string basePath = null;
        string archivePath = null;

        foreach (CategoryAndPath cap in config.CategoriesAndPaths)
        {
            if (cap.Category == "Archiv")
            {
                archivePath = cap.Path;
                break;
            }
        }
        if (string.IsNullOrEmpty(archivePath)) throw new Exception("Archiv category not found or without path");

        foreach (CategoryAndPath cap in config.CategoriesAndPaths)
        {
            if (cap.Category == Categorie)
            {
                basePath = cap.Path;
                break;
            }
        }
        if (string.IsNullOrEmpty(basePath)) throw new Exception("Category not found or without path");

        string projectFolderPath = Path.Combine(basePath, $"{Id}_{Name}");
        if (!Directory.Exists(projectFolderPath)) throw new DirectoryNotFoundException($"Project folder '{projectFolderPath}' not found");

        if (!Directory.Exists(archivePath))
        {
            Directory.CreateDirectory(archivePath);
        }

        string zipFileName = Path.Combine(archivePath, $"{Id}_{Name}.zip");

        ZipFile.CreateFromDirectory(projectFolderPath, zipFileName);
    }

    /// <summary>  
    /// Erstellt die Ordnerstruktur für das angegebene Stammverzeichnis.  
    /// </summary>  
    /// <param name="rootFolder">Das Stammverzeichnis, für das die Ordnerstruktur erstellt werden soll.</param>  
    /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn das <paramref name="rootFolder"/> null ist.</exception>  
    /// <exception cref="Exception">Wird ausgelöst, wenn die angegebene Kategorie nicht gefunden wird oder ohne Pfad ist.</exception>
    public void CreateFolderStructure(FolderInfo rootFolder)
    {
        if (rootFolder == null) throw new ArgumentNullException(nameof(rootFolder));
        Configuration config = Configuration.GetConfiguration();
        string basePath = null;

        foreach (CategoryAndPath cap in config.CategoriesAndPaths)
        {
            if (cap.Category == Categorie)
            {
                basePath = cap.Path;
                break;
            }
        }
        if (string.IsNullOrEmpty(basePath)) throw new Exception("Category not found or without path");

        string projectFolderPath = Path.Combine(basePath, $"{Id}_{Name}");
        if (Directory.Exists(projectFolderPath)) return;
        Directory.CreateDirectory(projectFolderPath);

        CreateSubFolders(rootFolder, projectFolderPath);
    }

    /// <summary>  
    /// Erstellt rekursiv die Unterordnerstruktur.  
    /// </summary>  
    /// <param name="folder">Das aktuelle Verzeichnis, dessen Unterordner erstellt werden sollen.</param>  
    /// <param name="currentPath">Der Pfad des aktuellen Verzeichnisses.</param>  
    private static void CreateSubFolders(FolderInfo folder, string currentPath)
    {
        foreach (var subFolder in folder.SubFolders)
        {
            string subFolderPath = Path.Combine(currentPath, subFolder.FolderName);
            Directory.CreateDirectory(subFolderPath);
            CreateSubFolders(subFolder, subFolderPath);
        }
    }
}
