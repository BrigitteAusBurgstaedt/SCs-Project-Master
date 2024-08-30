using ClosedXML.Excel;
using System;
using System.Collections.Generic;

namespace SCsProjectMaster.Source.Models;

public partial class Invoice
{
    public string Number { get; set; } = null!;

    public DateOnly? CreationDate { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual Project Project { get; set; }

    public void Create(string invoicePath)
    {
        // TODO weitere Felder füllen
        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Data", "vorlage_rechnung.xlsx");

        // Datei öffnen
        using (var workbook = new XLWorkbook(templatePath))
        {
            var worksheet = workbook.Worksheet(1); // Daten kommen auf das erste Blatt

            // Rechnungsinformationen in die Vorlage schreiben
            worksheet.Cell("B16").Value = Number;
            worksheet.Cell("G7").Value = Number;
            worksheet.Cell("G8").Value = CreationDate?.ToString("dd.MM.yyyy");

            // Positionen starten ab Zeile 25
            int currentRow = 25;

            foreach (var item in InvoiceItems)
            {
                worksheet.Cell(currentRow, 1).Value = item.Position; // Position
                worksheet.Cell(currentRow, 2).Value = item.Name;     // Name
                worksheet.Cell(currentRow, 5).Value = item.Units;    // Menge
                worksheet.Cell(currentRow, 6).Value = item.UnitPrice; // Einzelpreis
                worksheet.Cell(currentRow, 7).Value = item.Units * item.UnitPrice; // Gesamtpreis

                currentRow++;
            }

            // Excel-Datei speichern
            workbook.SaveAs(invoicePath);
        }
    }
}
