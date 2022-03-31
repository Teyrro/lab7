using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;


namespace lab7.Models
{
    public class StudentData : INotifyPropertyChanged
    {
        public StudentData(string name)
        {
            Name = name;
            Cells = new ObservableCollection<Cell>(FillSubjects());
            CountAverage();
        }

        private ObservableCollection<Cell> FillSubjects ()
        {
            ObservableCollection<Cell> tmp = new ObservableCollection<Cell>();
            for (int i = 0; i < 4; i++)
            {
                tmp.Add(new Cell("1"));
            }
            return tmp;
        }

        public StudentData(string name, ObservableCollection<Cell> cell)
        {
            Name = name;
            Cells = cell;
            CountAverage();
        }

        public string Name { get; set; }
        public ObservableCollection<Cell> Cells { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool IsChecked { get; set; }
        private string averageMark;
        public string AverageMark 
        { 
            get => averageMark;
            set
            {
                averageMark = value;
                double val = Convert.ToDouble(value);
                if (val < 1) Background = Brushes.Red;
                else if (val < 1.5) Background = Brushes.Yellow;
                else Background = Brushes.Green;
                NotifyPropertyChanged();
            }
        }
        IBrush background;
        public IBrush Background
        {
            get => background;
            set
            {
                background = value;
                NotifyPropertyChanged();
            }
        }
        public void CountAverage()
        {
            double sum = 0, count = 0;
            for (int i = 0; i < Cells.Count; i++)
            {
                if (Cells[i].Mark != "#ERROR") sum += Convert.ToDouble(Cells[i].Mark);
                count++;
            }
            AverageMark = Convert.ToString(sum / count);
        }

    }
}
