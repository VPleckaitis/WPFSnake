using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Models
{
    public class Cell
    {
        private int _column, _row;
        private CellTypeEnum _cellType;

        public Cell(int row, int column)
        {
          new  Cell(row, column,CellTypeEnum.EMPTY);
        }
        public Cell(int row, int column, CellTypeEnum type)
        {
            _row = row;
            _column = column;
            _cellType = type;
        }

        public int Column
        {
            get { return _column; }
            set { _column = value; }
        }

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        public CellTypeEnum CellType
        {
            get { return _cellType; }
            set { _cellType = value; }
        }

        public enum CellTypeEnum
        {
            EMPTY,
            FOOD,
            EATEN_FOOD,
            DIGESTED_FOOD,
            SNAKE
        }
    }
}
