using SQLiteNetExtensions.Attributes;
using System;
using System.ComponentModel;
using SQLite;

namespace YuewenNote.Model
{
    public class Sheet : INotifyPropertyChanged
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
                if (value != null)
                {
                    name = value; OnPropertyChanged("Name");
                }

            }
        }

        private DateTime lastModified;

        public DateTime LastModified
        {
            get { return lastModified; }
            set
            {
                if (value != lastModified)
                {
                    lastModified = value; OnPropertyChanged("LastModified");
                }

            }
        }

        private int version;

        public int Version
        {
            get { return version; }
            set
            {
                if (value != version)
                {
                    version = value;
                    OnPropertyChanged("Version");
                }

            }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set
            {
                if (value != content)
                {
                    content = value;
                    OnPropertyChanged("Content");
                }

            }
        }

        private int folderId;
        [ForeignKey(typeof(Folder))]
        public int FolderId
        {
            get { return folderId; }
            set
            {
                if (value != folderId)
                {
                    folderId = value; OnPropertyChanged("FolderId");
                }

            }
        }

        private Folder folder;
        [ManyToOne]
        public Folder Folder
        {
            get { return folder; }
            set
            {
                if (value != folder)
                {
                    folder = value;
                    OnPropertyChanged("Folder");
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
