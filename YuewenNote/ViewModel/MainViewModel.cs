using System;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.CodeParser;
using DevExpress.Mvvm.Native;
using GalaSoft.MvvmLight;
using YuewenNote.Model;
using YuewenNote.Respositories;

namespace YuewenNote.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public partial class MainViewModel : ViewModelBase
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        public ObservableCollection<Folder> Folders { get; set; }

        public ObservableCollection<Sheet> Sheets { get; set; }
        private MenuItemRepository menuRepo = new MenuItemRepository();
        private FolderRepository groupRepo = new FolderRepository();
        private SheetRepository sheetRepo = new SheetRepository();
        public MainViewModel()
        {
            MenuItems = menuRepo.Load().Result;
            Sheets = sheetRepo.Load().Result;
            Folders = groupRepo.Load().Result;

        }
    }
}