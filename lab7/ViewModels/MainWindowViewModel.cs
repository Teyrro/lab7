using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ReactiveUI;
using System.Reactive;
using lab7.Models;

namespace lab7.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<StudentData> data;
        public ObservableCollection<StudentData> Data
        {
            get => data;
            set => this.RaiseAndSetIfChanged(ref data, value);
        }

        private ObservableCollection<CellAverageMark> newcells;

        public ObservableCollection<CellAverageMark> NewCells
        {
            get => newcells;
            set => this.RaiseAndSetIfChanged(ref newcells, value);
        }
    
    public MainWindowViewModel()
        {
            Data = new ObservableCollection<StudentData>();
            NewCells = new ObservableCollection<CellAverageMark>();
            Init();

            Exit = ReactiveCommand.Create(() => Environment.Exit(0));
            Save = ReactiveCommand.Create(() => SaveTable());
            Load = ReactiveCommand.Create(() => LoadTable());
            About = ReactiveCommand.Create(() => OpenDiag());
            AddStudent = ReactiveCommand.Create(() => AddStudentToData());
            DellStudent = ReactiveCommand.Create(() => DellStudentFromData());
        }

        void Init()
        {
            Data.Add(new StudentData("Aleks"));
            Data.Add(new StudentData("Chort"));
            foreach (StudentData i in Data) 
            { 
                i.CountAverage();
            }
            
            for (int index = 0; index < 5; index++)
            {
                NewCells.Add(new CellAverageMark("0"));
            }
            for (int index = 0; index < NewCells.Count - 1; index++)
            {
                AverageMarkForSubject(index);
            }
        }
        
        public ReactiveCommand<Unit, Unit> Exit { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public ReactiveCommand<Unit, Unit> Load { get; }
        public ReactiveCommand<Unit, Unit> About { get; }
        public ReactiveCommand<Unit, Unit> AddStudent { get; }
        public ReactiveCommand<Unit, Unit> DellStudent { get; }

        public void ChangeMark(int ind)
        {
        
        }
        private void SaveTable()
        {
            using (StreamWriter file = new StreamWriter("studData.txt"))
            {
                for(int i = 0; i < Data.Count; i++)
                {
                    file.WriteLine(Data[i].Name);
                    for (int j = 0; j < Data[i].Cells.Count; j++)
                    {
                        file.Write(Data[i].Cells[j].Mark);
                        if (j != Data[i].Cells.Count - 1)
                            file.Write(" ");
                    }
                    file.WriteLine();
                }
                
            }
          
        }

        public void AverageMarkForSubject(int index)
        {
            double sum = 0;
            int count = 0;
            foreach(var student in Data)
            {
                sum += Convert.ToDouble(student.Cells[index].Mark);
                count += 1;
            }
            NewCells[index].Mark = Convert.ToString(sum / count);

            foreach (var student in Data)
            {
                sum += Convert.ToDouble(student.Cells[^1].Mark);
                count += 1;
            }
            NewCells[^1].Mark = Convert.ToString(sum / count);
        }
        private void LoadTable()
        {
            string? line = "";
            Data.Clear();
            using (StreamReader file = new StreamReader("studData.txt"))
            {
                while((line = file.ReadLine())!=null)
                {
                    string[] parts = file.ReadLine().Split(" ");
                    ObservableCollection<Cell> marks = new ObservableCollection<Cell>();
                    foreach (string part in parts) {
                        Cell newItem = new Cell(part);
                        marks.Add(newItem);
                    }
                    Data.Add(new StudentData(line, marks));
                }

            }
         
        }
        private void OpenDiag()
        {
        
        }
        private void AddStudentToData()
        {
            StudentData s = new StudentData("");
            Data.Add(s);
        
        }

        void UpdateCollection(StudentData yes)
        {
            int index = Data.IndexOf(yes);
            if (index == -1)
            {
                Data.RemoveAt(index);
                Data.Add((StudentData)yes);
            }

        }

        private void DellStudentFromData()
        {
            for (int i = 0; i < Data.Count;)
            {
                StudentData s = Data[i];
                if (s.IsChecked)
                {
                    Data.Remove(s);
                }
                else i++;
            }
        }

        
    }
    
}
