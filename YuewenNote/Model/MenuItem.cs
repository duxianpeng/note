using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace YuewenNote.Model
{
    public class MenuItem : INotifyPropertyChanged
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

        private string header;

        public string Header
        {
            get { return header; }
            set
            {
                if (value != header)
                {
                    header = value;
                    OnPropertyChanged("Header");
                }
            }
        }

        private string inputGestureText;

        public string InputGestureText
        {
            get { return inputGestureText; }
            set
            {
                if (value != inputGestureText)
                {
                    inputGestureText = value;
                    OnPropertyChanged("InputGestureText");
                }
            }
        }

        private int parentId;

        [ForeignKey(typeof(MenuItem))]
        public int ParentId
        {
            get { return parentId; }
            set
            {
                if (value != parentId)
                {
                    parentId = value;
                    OnPropertyChanged("ParentId");
                }
            }
        }

        private MenuItem parent;

        [ManyToOne]
        public MenuItem Parent
        {
            get;
            set;
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<MenuItem> MenuItems { get; set; }

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
