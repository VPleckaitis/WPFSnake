using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Models
{
    class Snake
    {
        private Cell SnakeHead;
        List<Cell> SnakeBody = new List<Cell>();

        public Snake(Cell startingCell)
        {
            SnakeHead = startingCell;
            SnakeBody.Add(startingCell);
            //Test - again for travis 4
        }
    }
}
