using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YuewenNote
{

    public class MvvmChromiumWebBrower : ChromiumWebBrowser, INotifyPropertyChanged
    {
        public static DependencyProperty ShouldReloadProperty = DependencyProperty.Register("ShouldReload", typeof(bool), typeof(MvvmChromiumWebBrower),
            new PropertyMetadata(default(bool), (obj, args) =>
            {
                MvvmChromiumWebBrower target = (MvvmChromiumWebBrower)obj;
                target.ShouldReload = (bool)args.NewValue;
                target.Reload();
            }));

        public bool ShouldReload
        {
            get
            {
                return (bool)GetValue(ShouldReloadProperty);
            }
            set
            {
                SetValue(ShouldReloadProperty, value);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
