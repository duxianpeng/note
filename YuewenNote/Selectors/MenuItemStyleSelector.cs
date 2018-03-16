using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YuewenNote.ViewModel;

namespace YuewenNote.Selectors
{
    public class MenuItemStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            //if (item is SeparatorViewModel)
            //   return ((FrameworkElement)container).FindResource("contextMenuSeparator") as Style;

            return ((FrameworkElement)container).FindResource("contextMenuMenuItem") as Style;
        }
    }
}
