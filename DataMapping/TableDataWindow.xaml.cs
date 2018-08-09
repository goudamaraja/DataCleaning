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
    /// Interaction logic for TableDataWindow.xaml
    /// </summary>
    public partial class TableDataWindow : Window
    {
        DataMappingVM vm = DataMappingVM.GetInstance();

        public TableDataWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
