using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTheFolder
{
    public class Folder
    {
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        private ObservableCollection<File> _subFiles;
        public ObservableCollection<File> SubFiles
        {
            get { return _subFiles ?? (_subFiles = new ObservableCollection<File>()); }
            set
            {
                _subFiles = value;
            }
        }

        private ObservableCollection<Folder> _subFolder;
        public ObservableCollection<Folder> SubFolders
        {
            get { return _subFolder ?? (_subFolder = new ObservableCollection<Folder>()); }
            set
            {
                _subFolder = value;
            }
        }

        public Folder()
        {

        }
    }
    public class File
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
    public class Item
    {
        public string ItemName { get; set; }
        public ItemType IType { get; set; }
        public string Source { get; set; }
    }
    public enum ItemType
    {
        File,
        Folder
    }
}
