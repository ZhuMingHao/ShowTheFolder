using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ShowTheFolder
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public MainViewModel()
        {
        }

        public Command PickCommand => new Command(() => this.BtnClick());

        private async void BtnClick()
        {
            StorageFolder Selectfolder = ApplicationData.Current.LocalFolder;
            this.Items = await FolderService.GetItems(Selectfolder);
        }
        private ObservableCollection<Item> _items;

       
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        private Item _selectItem;
        public Item SelectItem
        {
            get
            {
                return _selectItem;
            }
            set
            {
                _selectItem = value;
                OnPropertyChanged();
                if(_selectItem.IType== ItemType.Folder)
                {
                    var curren = Window.Current.Content as Frame;
                    curren.Navigate(typeof(MainPage), _selectItem);
                }   
            }
        }
        public  async Task Handle(Item para)
        {
            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(para.Source);
            this.Items = await FolderService.GetItems(folder);
        }
    }
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public Command(Action execute, Func<bool> canexecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this.execute = execute;
            this.canExecute = canexecute ?? (() => true);
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object o)
        {
            return this.canExecute();
        }

        public void Execute(object p)
        {
            if (!CanExecute(p))
            {
                return;
            }
            this.execute();
        }
    }
}
