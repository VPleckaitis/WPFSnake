using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Models
{
    public class Map
    {
        private int rows, columns;
        private Cell[,] _cells;
        public Cell [,] Cells
        {
            get { return _cells; }
            private set { _cells = value; }
        }
        public Map(int rows, int columns)
        {
            _cells = new Cell[rows,columns];
            this.rows = rows;
            this.columns = columns;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    _cells[i, j] = new Cell(i,j);
                }
        }

        public void UpdateCell(int row, int column, Cell.CellTypeEnum cellType)
        {
            _cells[row, column] = new Cell(row,column,cellType);
        }
        public Cell AddFood(Snake snake)
        {
            int emptyCellCount = GetEmptyCellsCount();
            if (emptyCellCount == 0) throw new Exception("0 empty cells left !");
            else if (emptyCellCount <= snake.SnakeBody.Count()) throw new Exception("All remaining spaces are used by snake !");

            Cell cell = new Cell(-1,-1, Cell.CellTypeEnum.DIGESTED_FOOD); // just initialise cells for check

            while (cell.CellType != Cell.CellTypeEnum.EMPTY)
            {
                Random rnd = new Random(); // will need replace it eventually since it is way too inefficient if snake is quite big
                int _row = rnd.Next(rows);
                int _col = rnd.Next(columns);
                cell = _cells[_row, _col];

                if (snake.SnakeBody.Where(o => (o.Column == _col) && (o.Row == _row)).ToList().Count() > 0) cell.CellType = Cell.CellTypeEnum.SNAKE;
            }

            cell.CellType = Cell.CellTypeEnum.FOOD;
            return cell;
        }

        public int GetEmptyCellsCount()
        {
            int emptyCells = 0;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    if (_cells[i, j].CellType == Cell.CellTypeEnum.EMPTY) emptyCells++;

            return emptyCells;
        }
    }
}
