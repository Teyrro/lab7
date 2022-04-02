using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab7.Models
{
    public class CellAverageMark : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string mark;
        public string Mark
        {
            get => mark;
            set
            {
                mark = value;
                double val = Convert.ToDouble(value);
                if (val < 1) Background = Brushes.Red;
                else if (val < 1.5) Background = Brushes.Yellow;
                else Background = Brushes.Green;
                NotifyPropertyChanged();
            }
        }
        public IBrush Background { get; set; }
        public CellAverageMark(string mark)
        {
            Mark = mark;
        }
    }
}
