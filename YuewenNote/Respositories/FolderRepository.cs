using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;
using YuewenNote.Model;

namespace YuewenNote.Respositories
{
    public class FolderRepository : IDataRepository<Folder>
    {
        private static readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.SQLite");
        private ObservableCollection<Folder> _folders;

        public FolderRepository()
        {
            using (var db = new SQLiteConnection(_dbPath))
            {
                db.CreateTable<Folder>();
                db.CreateTable<Sheet>();
                if (db.ExecuteScalar<int>("select count(1) from Folder") == 0)
                {

                    db.RunInTransaction(() =>
                    {
                        db.InsertWithChildren(new Folder()
                        {
                            Name = "Library",
                            Folders = new ObservableCollection<Folder>(
                                new List<Folder>(){
                                    new Folder()
                                    {
                                        Name = Properties.Resources.TreeItemAllNameString,
                                        Glyth = Properties.Resources.TreeItemAllGryphGeometry
                                    },
                                    new Folder()
                                    {
                                        Name = Properties.Resources.TreeItemLast7DaysNameString,
                                        Glyth = Properties.Resources.TreeItemLast7DaysGryphGeometry
                                    },
                                    new Folder()
                                    {
                                        Name = Properties.Resources.TreeItemTrashNameString,
                                        Glyth = Properties.Resources.TreeItemTrashGryphGeometry
                                    }
                                }
                            )
                        });
                        db.Insert(new Folder()
                        {
                            Name = "On My Computer"
                        });
                    });
                }
                else
                {
                    Load();
                }
            }
        }

        public Task<ObservableCollection<Folder>> Load()
        {
            return Task.Run(async () =>
            {
                var connection = new SQLiteAsyncConnection(_dbPath);
                return new ObservableCollection<Folder>(
                    await connection.GetAllWithChildrenAsync<Folder>(x => x.ParentId == 0));
            });
        }
        public Task Add(Folder folder)
        {
            _folders.Add(folder);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.InsertAsync(folder);
        }

        public Task Remove(Folder folder)
        {
            _folders.Remove(folder);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.DeleteAsync(folder);
        }

        public Task Update(Folder folder)
        {
            var oldFolder = _folders.FirstOrDefault(c => c.Id == folder.Id);
            if (oldFolder == null)
            {
                throw new System.ArgumentException("Folder not found");
            }

            _folders.Remove(oldFolder);
            _folders.Add(folder);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.UpdateAsync(folder);
        }

        public Task<Folder> GetById(Folder t)
        {
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.GetWithChildrenAsync<Folder>(t.Id);
        }
    }
}
