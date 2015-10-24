using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;

namespace KulturPRO.Utillities
{
    public class BrushWrapper
    {
        public string Name { get; set; }
        public Brush Brush { get; set; }

    }

    public class ColorViewModel : INotifyPropertyChanged
    {
        private readonly List<BrushWrapper> colors_;

        private BrushWrapper topBarBrush_;
        private BrushWrapper backgroundBrush_;
        public ColorViewModel()
        {
            colors_ = (from p in typeof(Brushes).GetProperties()
                       select new BrushWrapper
                       {
                           Brush = StringToBrushConverter.Convert(p.Name), Name=p.Name
                       }).ToList();

            topBarBrush_ = colors_.Single(b => b.Name == Properties.Settings.Default.TopBarBrush);
        }

        public List<BrushWrapper> Colors
        {
            get { return colors_; }
        }

        public BrushWrapper TopBarBrush
        {
            get 
            { 
                return topBarBrush_; 
            }
            set
            {
                if (topBarBrush_ != value)
                {
                    topBarBrush_ = value;
                    Properties.Settings.Default.TopBarBrush = topBarBrush_.Name;
                    OnPropertyChanged("TopBarBrush");
                }
                    
            }
        }

        public BrushWrapper BackgroundBrush
        {
            get
            {
                return backgroundBrush_;
            }
            set
            {
                if (backgroundBrush_ != value)
                {
                    backgroundBrush_ = value;
                    Properties.Settings.Default.WindowsBackground = backgroundBrush_.Name;
                    OnPropertyChanged("BackgroundBrush");
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    } 
}
