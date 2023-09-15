using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using CommunityToolkit.WinUI.UI;
using CommunityToolkit.WinUI.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Inventory_Qubit.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompanyInventoryPage : Page
    {
        public CompanyInventoryPage()
        {
            this.InitializeComponent();
            CompanyInventoryDatePicker.SelectedDate = DateTime.Today;
            this.CompanyInventoryDatePicker.LayoutUpdated += DatePickerControl_LayoutUpdated;
        }

        private void DatePickerControl_LayoutUpdated(object sender, object e)
        {
            Windows.UI.Color color = ColorHelper.ToColor("#020066");
            if (VisualTreeHelper
                .GetOpenPopupsForXamlRoot(this.CompanyInventoryDatePicker.XamlRoot)
                .FirstOrDefault() is Popup popup &&
                popup.Child.FindDescendant<Grid>(x => x.Name is "HighlightRect") is Grid highlightRect)
            {
                highlightRect.Background = new SolidColorBrush(color);
            }
        }
    }
}
