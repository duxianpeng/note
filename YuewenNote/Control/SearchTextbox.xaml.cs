using GalaSoft.MvvmLight.Command;
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

namespace YuewenNote.Control
{
    /// <summary>
    /// SearchTextbox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTextbox : UserControl
    {
        public SearchTextbox()
        {
            InitializeComponent();
        }

        public RelayCommand<object> ClickCommand
        {
            get { return (RelayCommand<object>)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register("ClickCommand", typeof(RelayCommand<object>), typeof(SearchTextbox), new UIPropertyMetadata(null));
    }
}
