using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataMapping
{
    /// <summary>
    /// Interaction logic for LoadTableWindow.xaml
    /// </summary>
    public partial class LoadTableWindow : Window
    {
        DataMappingVM vm = DataMappingVM.GetInstance();

        public LoadTableWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set default file extension 
            dlg.DefaultExt = ".xlsx";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and set it in vm
            if (result == true)
            {
                // Open document 
                vm.FilePath = dlg.FileName;
            }
        }

        private void Button_Enter_Click(object sender, RoutedEventArgs e)
        {
            if (cmbbx_TableName.SelectedItem != null)
            {
                if (vm.ReadDataFromFile(cmbbx_TableName.SelectedItem.ToString()))
                    Close();
            }
        }
    }
}
