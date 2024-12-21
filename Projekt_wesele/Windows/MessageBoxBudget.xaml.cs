using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Linq;
using System.Windows;
using Projekt_wesele.Models;

namespace Projekt_wesele.Views
{
    public partial class BudgetMessageBox : Window
    {
        public bool IsSaveClicked { get; private set; } = false;
        private readonly IEnumerable<BudgetItem> _items;

        public BudgetMessageBox(string message, IEnumerable<BudgetItem> items)
        {
            InitializeComponent();
            MessageText.Text = message;
            _items = items;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            IsSaveClicked = true;

            SaveBudgetReportToPdf();

            this.DialogResult = true;
            this.Close();
        }

        private void SaveBudgetReportToPdf()
        {
            string filePath = "BudgetReport.pdf";

            try
            {
                using (var writer = new PdfWriter(filePath))
                using (var pdf = new Document(new PdfDocument(writer)))
                {
                    var headerStyle = new iText.Layout.Style().SetFontSize(18);

                    pdf.Add(new Paragraph("Budget Report")
                        .AddStyle(headerStyle)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                    var groupedItems = _items.GroupBy(item => item.Category).OrderBy(group => group.Key);

                    foreach (var group in groupedItems)
                    {
                        var categoryStyle = new iText.Layout.Style().SetFontSize(16);

                        pdf.Add(new Paragraph($"Category: {group.Key}")
                            .AddStyle(categoryStyle)
                            .SetMarginTop(20));

                        foreach (var item in group)
                        {
                            pdf.Add(new Paragraph(
                                $"Name: {item.Name}, Cost: {item.Cost:C}, Essential: {item.IsEssential}, Payed: {item.IsPayed}")
                                .SetFontSize(12)
                                .SetMarginBottom(5));
                        }
                    }

                    var totalCost = _items.Sum(item => item.Cost);
                    var totalCostStyle = new iText.Layout.Style().SetFontSize(14);

                    pdf.Add(new Paragraph($"Total Cost: {totalCost:C}")
                        .AddStyle(totalCostStyle)
                        .SetMarginTop(20));
                }

                MessageBox.Show($"Report saved to {filePath}", "Save Report", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving report: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
