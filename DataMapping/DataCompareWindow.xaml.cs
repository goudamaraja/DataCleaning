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
    /// Interaction logic for DataCompareWindow.xaml
    /// </summary>
    public partial class DataCompareWindow : Window
    {
        DataMappingVM vm = DataMappingVM.GetInstance();

        public DataCompareWindow()
        {
            DataContext = vm;
            InitializeComponent();
            vm.ResetColumns();
        }

        private void BtnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (vm.AnalyzeData())
            {
                Close();
            }
        }

        private void SelectionChanged_LeftColumn(object sender, RoutedEventArgs e)
        {
            vm.FirstColumnSelected = ((ListBox) sender).SelectedItem.ToString();
        }

        private void SelectionChanged_RightColumn(object sender, RoutedEventArgs e)
        {
            vm.SecondColumnSelected = ((ListBox)sender).SelectedItem.ToString();
        }
    }
}
