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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataMapping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataMappingVM vm = DataMappingVM.GetInstance();
        LoadTableWindow loadTableWindow = null;
        DataCompareWindow dataCompareWindow = null;
        TableDataWindow tableDataWindow = null;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void BtnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (dataCompareWindow == null)
            {
                dataCompareWindow = new DataCompareWindow();
                dataCompareWindow.Closed += dataCompareWindow_Closed;
                dataCompareWindow.Show();
            }
        }

        private void Button_LoadTable_Click(object sender, RoutedEventArgs e)
        {
            if (loadTableWindow == null)
            {
                loadTableWindow = new LoadTableWindow();
                loadTableWindow.Closed += loadTableWindow_Closed;
                loadTableWindow.Show();
            }
        }

        private void Button_TableData_Click(object sender, RoutedEventArgs e)
        {
            if (tableDataWindow == null)
            {
                tableDataWindow = new TableDataWindow();
                tableDataWindow.Closed += tableDataWindow_Closed;
                tableDataWindow.Show();
            }
        }

        private void Button_CleanSave_Click(object sender, RoutedEventArgs e)
        {
            if (vm.CleanAndSave())
                MessageBox.Show("New file saved!");
        }

        private void Button_SetDelete_Click(object sender, RoutedEventArgs e)
        {
            ComparisonItem item = (ComparisonItem) GridEmployees.SelectedItem;

            if (item != null)
            {
                vm.SetDelete(item);
            }
        }

        private void loadTableWindow_Closed(object sender, EventArgs e)
        {
            loadTableWindow = null;
        }

        private void dataCompareWindow_Closed(object sender, EventArgs e)
        {
            dataCompareWindow = null;
        }

        private void tableDataWindow_Closed(object sender, EventArgs e)
        {
            tableDataWindow = null;
        }
    }
}
