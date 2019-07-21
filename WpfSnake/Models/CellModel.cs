using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Models
{
    public class Cell
    {
        private int _column=0, _row=0;
        private CellTypeEnum _cellType;

        public Cell(int row, int column) : base()
        {
            Row = row;
            Column = column;
            CellType = CellTypeEnum.EMPTY;
        }
        public Cell(int row, int column, CellTypeEnum type)
        {
            Row = row;
            Column = column;
            CellType = type;
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
