using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Models
{
    public class Snake
    {
        private Cell _snakeHead;
        public List<Cell> SnakeBody = new List<Cell>();

        public Cell SnakeHead // just so we can access this from other classes
        {
            get { return _snakeHead; }
            set { _snakeHead = value; }
        }

        public Snake(Cell startingCell)
        {
            _snakeHead = startingCell;
            SnakeBody.Add(startingCell);
        }

        public void Grow()
        {
            SnakeBody.Insert(0, _snakeHead); // grow from head. At this point we have 2
        }

        public void Move(Cell next)
        {
            Cell tail = SnakeBody.Last();
            SnakeBody.Remove(tail);
            tail.CellType = Cell.CellTypeEnum.EMPTY;

            _snakeHead = next;
            SnakeBody.Insert(0, _snakeHead);
        }

        public bool SnakeHitTheCell(Cell next)
        {
            var isThereAHit = SnakeBody.Where(o => (o.Row == next.Row && o.Column == next.Column));
            return isThereAHit.Count() > 0;
        }


    }
}
