using Avalonia.Controls;
using System;
using Avalonia.Interactivity;
using lab7.Models;
using lab7.ViewModels;
using System.Collections.Generic;
using ReactiveUI;

namespace lab7.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        public async void StudentMarkAverage(object sender, RoutedEventArgs e)
        {

            var box = sender as TextBox;
            if (box != null)
            {
                var student = box.DataContext as StudentData;
                student.CountAverage();

            }
            
           
        }

        private async void AboutClick(object sender, RoutedEventArgs e)
        {

            await new AboutDiagView().ShowDialog<string?>((Window)this.VisualRoot);
        }
    }
}
