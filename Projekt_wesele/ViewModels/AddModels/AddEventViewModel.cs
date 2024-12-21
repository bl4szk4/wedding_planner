using Projekt_wesele.Models;
using Projekt_wesele.ViewModels;
using System.Windows;

public class AddEventViewModel : DialogViewModelBase
{
    public Event Item { get; set; }

    public AddEventViewModel(Event item)
    {
        Item = item;
        Item.StartTime = DateTime.Now; 
        Item.EndTime = DateTime.Now.AddHours(1);
    }

    protected override void Save()
    {
        if (string.IsNullOrWhiteSpace(Item.Name))
        {
            MessageBox.Show("Name is required. Please fill in the Name field.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (Item.EndTime <= Item.StartTime)
        {
            MessageBox.Show("End Time must be later than Start Time.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        base.Save();
    }
}
