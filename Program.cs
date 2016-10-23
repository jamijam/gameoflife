using System;
using System.IO;
using System.Text;
using System.Threading;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1){
                Console.WriteLine("You must supply a seed file");
                Environment.Exit(-1);
            }

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            var grid = new Grid(args[0]);
            var speed = args.Length == 2 ? Int16.Parse(args[1]) : 50;

            while (true) {
                Console.Clear();
                grid.PrintGrid();

                Thread.Sleep(speed);
                grid.Tick();
            }
        }
    }

    public class Grid {
        private int[,] _grid;
        private readonly char lifeSymbol = '\u25CF'; // \u26AB ⚫ \u25CF ● 

        public Grid(string filePath){
            if (!System.IO.File.Exists(filePath))
                throw new Exception($"File {filePath} does not exist");

            InitFromFile(filePath);
        }

        public int Size {
            get{
                return _grid.GetLength(0);
            }
        }

        private void InitFromFile(string filePath){
            var allLines = File.ReadAllLines(filePath);
            if (allLines.Length == 0)
                throw new Exception("The seed file is empty");
            
            _grid = InitGridFromLine(allLines[0]);

            for (int r=0;r<allLines.Length;r++){
                var line = allLines[r];
                for (int c=0;c<line.Length;c++){
                    if (line[c] == '#'){
                        _grid[r,c] = 1;
                    }
                }
            }
        }

        private int[,] InitGridFromLine(string line){
            var length = line.Length;
            return new int[length, length];
        }

        public void PrintGrid(){
            for (int r=0;r<_grid.GetLength(0);r++){
                var  builder = new StringBuilder(); 
                for (int c=0;c<_grid.GetLength(1);c++){

                    if (_grid[r,c] == 1)
                        builder.Append(lifeSymbol);
                    else
                        builder.Append(' ');
                }

                builder.Append(Environment.NewLine);
                System.Console.Write(builder.ToString());
            }
        }

        private int[] SurroundingCells(int row, int col) {
            int[] surrounding = new int[8];
            int index = 0;
            for (int r = row-1;r <= row + 1;r++){
                if ((r < 0) || (r >= _grid.GetLength(0))) continue;
                for (int c = col-1;c <= col + 1;c++){
                    if ((c < 0) || (c >= _grid.GetLength(1))) continue;

                    if ((r == row) && (c == col)) continue;
                    surrounding[index++] = _grid[r, c];
                }
            }
            return surrounding;
        }

        public int NumberOfLiveNeighbours(int row, int col) {
            var surrounding = SurroundingCells(row, col);
            var sum = 0;

            for (int i=0;i<surrounding.Length;i++)
                sum += surrounding[i];

            return sum;
        }

        public bool IsCellAliveAt(int row, int col){
            return _grid[row,col] == 1;
        }

        public int DetermineFate(int row, int col){
            var count = NumberOfLiveNeighbours(row, col);

            if (IsCellAliveAt(row, col)){

                if (count < 2)
                    return 0;

                else if ( (count == 2) || (count == 3))
                    return 1;

                else if (count > 3)
                    return 0;                
            }
            else {
                if (count == 3)
                    return 1;
            }

            return 0;
        }

        public void Tick(){

            var newGrid = _grid.Clone() as int[,];

            for (int r=0;r<_grid.GetLength(0);r++){
                for (int c=0;c<_grid.GetLength(1);c++){
                    newGrid[r,c] = DetermineFate(r, c);
                }
            }

            _grid = newGrid;
        }
    }
}