using Avalonia.Media;

namespace lab7.Models
{
    public class Cell
    {
        string mark;
        public string Mark
        {
            get => mark;
            set
            {
                mark = value;
                switch (value)
                {
                    case "0":
                        
                        break;
                    case "1":
                       
                        break;
                    case "2":
                        
                        break;
                    default:
                        mark = "#ERROR";
                        
                        break;
                }
            }
        }
        public Cell(string mark)
        {
            Mark = mark;
        }
    }
}
