
using System.Collections.ObjectModel;
using System.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace YuewenNote.Model
{
    public class Folder : INotifyPropertyChanged
    {

        private int id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }

            }
        }

        private string glyth;

        public string Glyth
        {
            get { return glyth; }
            set
            {
                if (value != glyth)
                {
                    glyth = value;
                    OnPropertyChanged("Glyth");
                }

            }
        }

        public string category;

        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value; OnPropertyChanged("Category");
            }
        }

        private int parentId;
        [ForeignKey(typeof(Folder))]
        public int ParentId
        {
            get
            {
                return parentId;
            }
            set
            {
                if (value != parentId)
                {
                    parentId = value;
                    OnPropertyChanged("ParentId");
                }
            }
        }


        private bool isActive;

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                if (value != isActive)
                {
                    isActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
        }

        private Folder parent;

        [ManyToOne]
        public Folder Parent
        {
            get { return parent; }
            set
            {
                if (value != parent)
                {
                    parent = value;
                    OnPropertyChanged("Parent");
                }
            }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Folder> Folders { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Sheet> Sheets
        {
            set;
            get;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string strPropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
            }
        }

    }
}
