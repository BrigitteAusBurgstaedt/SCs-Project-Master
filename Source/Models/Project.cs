using System;
using System.Collections.Generic;
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

        string projectFolderPath = Path.Combine(basePath, Id + " " + Name);
        if (Directory.Exists(projectFolderPath)) return;
        Directory.CreateDirectory(projectFolderPath);

        CreateSubFolders(rootFolder, projectFolderPath);
    }

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
