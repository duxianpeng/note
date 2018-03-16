using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.CodeParser.CSharp;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;
using YuewenNote.Model;
using YuewenNote.Properties;
using YuewenNoteLibrary.Properties;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace YuewenNote.Respositories
{
    public class MenuItemRepository : IDataRepository<MenuItem>
    {

        private static readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.SQLite");
        public ObservableCollection<MenuItem> _menuitems { get; private set; }

        public MenuItemRepository()
        {

            using (var db = new SQLiteConnection(_dbPath))
            {
                db.CreateTable<MenuItem>();
                if (db.ExecuteScalar<int>("select count(1) from MenuItem") == 0)
                {

                    db.RunInTransaction(() =>
                    {
                        var fileMenu = new MenuItem() { Header = Resources.MenuHeaderFile };
                        var newSheetMenu = new MenuItem()
                        {
                            Header = Resources.MenuHeaderFileNewSheet,
                            InputGestureText = Resources.MenuGestureFileNewSheet
                        };
                        var newGroupMenu = new MenuItem()
                        {
                            Header = Resources.MenuHeaderFileNewGroup,
                            InputGestureText = Resources.MenuGestureFileNewGroup
                        };

                        db.Insert(fileMenu);
                        db.Insert(newSheetMenu);
                        db.Insert(newGroupMenu);

                        fileMenu.MenuItems = new ObservableCollection<MenuItem>(new List<MenuItem> { newSheetMenu, newGroupMenu });
                        db.UpdateWithChildren(fileMenu);

                    });
                }
                else
                {
                    Load();
                }
            }
        }

        public Task<ObservableCollection<MenuItem>> Load()
        {
            return Task.Run(async () =>
            {
                var connection = new SQLiteAsyncConnection(_dbPath);
                return new ObservableCollection<MenuItem>(
                    await connection.GetAllWithChildrenAsync<MenuItem>(x => x.ParentId == 0));
            });
        }

        public Task Add(MenuItem menuItem)
        {
            _menuitems.Add(menuItem);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.InsertAsync(menuItem);
        }

        public Task Remove(MenuItem menuItem)
        {
            _menuitems.Remove(menuItem);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.DeleteAsync(menuItem);
        }

        public Task Update(MenuItem menuItem)
        {
            var oldMenuItem = _menuitems.FirstOrDefault(c => c.Id == menuItem.Id);
            if (oldMenuItem == null)
            {
                throw new System.ArgumentException("MenuItem not found");
            }

            _menuitems.Remove(oldMenuItem);
            _menuitems.Add(menuItem);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.UpdateAsync(menuItem);
        }

        public Task<MenuItem> GetById(MenuItem t)
        {
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.GetWithChildrenAsync<MenuItem>(t.Id);
        }
    }
}
