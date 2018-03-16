using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using YuewenNote.Model;

namespace YuewenNote.Respositories
{
    public class SheetRepository : IDataRepository<Sheet>
    {
        private static readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.SQLite");
        private ObservableCollection<Sheet> _sheets;

        public SheetRepository()
        {
            using (var db = new SQLiteConnection(_dbPath))
            {
                db.CreateTable<Sheet>();
            }
        }

        public Task<ObservableCollection<Sheet>> Load()
        {
            return Task.Run(async () =>
            {
                var connection = new SQLiteAsyncConnection(_dbPath);
                return new ObservableCollection<Sheet>(
                    await connection.GetAllWithChildrenAsync<Sheet>());
            });
        }
        public Task Add(Sheet sheet)
        {
            _sheets.Add(sheet);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.InsertAsync(sheet);
        }

        public Task Remove(Sheet sheet)
        {
            _sheets.Remove(sheet);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.DeleteAsync(sheet);
        }

        public Task Update(Sheet sheet)
        {
            var oldSheet = _sheets.FirstOrDefault(c => c.Id == sheet.Id);
            if (oldSheet == null)
            {
                throw new System.ArgumentException("Sheet not found");
            }

            _sheets.Remove(oldSheet);
            _sheets.Add(sheet);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.UpdateAsync(sheet);
        }

        public Task<Sheet> GetById(Sheet t)
        {
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.GetWithChildrenAsync<Sheet>(t.Id);
        }
    }
}
