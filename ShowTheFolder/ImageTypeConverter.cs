using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ShowTheFolder
{
    public class ImageTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
           var image =   new BitmapImage();
            ItemType type = (ItemType)value;
          
            switch (type)
            {
                case ItemType.File:
                    image.UriSource = new Uri("ms-appx:/Assets/file-icon.png");
                    break;
                 
                case ItemType.Folder:
                    image.UriSource = new Uri("ms-appx:/Assets/folder-icon.png");
                    break;              
                    
            }
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
