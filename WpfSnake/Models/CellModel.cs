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

        public int Column
        {
            get { return _column; }
            set { _column = value; }
        }

        public int Row
        {
            get { return _row; }
            set { _column = value; }
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
            DIGESTED_FOOD,
            SNAKE
        }
    }
}
