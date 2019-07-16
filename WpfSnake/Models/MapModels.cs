using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Models
{
    class Map
    {
        private int rows, columns;
        private Cell[,] _cells;
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

        public Cell AddFood()
        {
            Cell cell = new Cell(-1,-1, Cell.CellTypeEnum.DIGESTED_FOOD); // just initialise cells for check
            while (cell.CellType != Cell.CellTypeEnum.EMPTY)
            {
                Random rnd = new Random();
                int _row = rnd.Next(rows);
                int _col = rnd.Next(columns);
                cell = _cells[_row, _col];
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
