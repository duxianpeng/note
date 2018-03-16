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

namespace YuewenNoteLibrary
{
    public abstract class CustomDialog : Window
    {
        protected UIElement _placeHolderPanel;
        protected Button _okBtn;
        protected Button _cancelBtn;
        protected abstract Decorator GetButtonsPlaceHolder();

        static CustomDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomDialog), new FrameworkPropertyMetadata(typeof(CustomDialog)));
        }
        public CustomDialog()
        {
            _okBtn = new Button();
            _okBtn.Content = "OK";
            _okBtn.Click += OkButtonOnClick;
            _okBtn.Margin = new Thickness(0, 0, 5, 0);

            _cancelBtn = new Button();
            _cancelBtn.Content = "Cancel";
            _cancelBtn.Click += CancelButtonOnClick;

            _placeHolderPanel = CreateLayoutPanel(_okBtn, _cancelBtn);
            this.Loaded += LoadedOnWindow;

            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, this.OnCloseWindow));
        }


        private UIElement CreateLayoutPanel(Button okBtn, Button cancelBtn)
        {
            Grid btngrid = new Grid();
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            btngrid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            btngrid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            btngrid.ColumnDefinitions.Add(coldef);

            btngrid.Children.Add(okBtn);
            Grid.SetColumn(okBtn, 1);

            btngrid.Children.Add(cancelBtn);
            Grid.SetColumn(cancelBtn, 2);

            return btngrid;
        }


        // called when window is loaded; extends Aero glass effect
        protected virtual void LoadedOnWindow(object sender, RoutedEventArgs e)
        {
            // put Window Buttons into placeholder
            Decorator placeholder = GetButtonsPlaceHolder();

            if (placeholder == null)
                throw new NotSupportedException("Placeholder must be created already in the initialization of the Window");

            placeholder.Child = _placeHolderPanel;
        }

        protected void OkButtonOnClick(object sender, RoutedEventArgs args)
        {
            DialogResult = true;
        }
        private void CancelButtonOnClick(object sender, RoutedEventArgs args)
        {
            DialogResult = false;
        }
        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}
