
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using YuewenNote.Model;

namespace YuewenNote.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private RelayCommand<MenuItem> menuDistributeCommand;
        public RelayCommand<MenuItem> MenuDistributeCommand
        {
            get
            {
                return menuDistributeCommand ?? (menuDistributeCommand = new RelayCommand<MenuItem>(ExecuteMenuDistributeCommand));
            }
        }

        private void ExecuteMenuDistributeCommand(MenuItem selectedItem)
        {
            switch (selectedItem.Header)
            {
                case "New Sheet":
                    PerformNewSheetAction();
                    break;
                case "New Group":
                    PerformNewGroupAction();
                    break;
            }
        }

        private void PerformNewGroupAction()
        {
            throw new NotImplementedException();
        }

        private void PerformNewSheetAction()
        {
            throw new NotImplementedException();
        }
    }
}
