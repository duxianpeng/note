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
    public class MenuItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MenuItemTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return MenuItemTemplate;
        }
    }
}
