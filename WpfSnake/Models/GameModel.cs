using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Models
{
    public class Game
    {
        private Snake snake;
        private Map map;
        public int mapRows=10, mapColumns=10;

        private bool gameOver = false;
        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value;
                if (gameOver)
                {
                    GameOverAction();
                }
            }
        }

        private MovementDirection direction = MovementDirection.None;
        public MovementDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public enum MovementDirection
        {
            None,
            Up,
            Down,
            Left,
            Right
        };

        public Game(Snake _snake, Map _map)
        {
            this.snake = _snake;
            this.map = _map;
        }

        private Cell GetNextCell(Cell currentCell)
        {
            int row = currentCell.Row;
            int column = currentCell.Column;

            switch (direction)
            {
                case MovementDirection.Down:
                    row++;
                     break;
                case MovementDirection.Up:
                    row--;
                    break;
                case MovementDirection.Left:
                    column--;
                    break;
                case MovementDirection.Right:
                    column++;
                    break;
                default: break;
            }
            if (row >= mapRows || row < 0 || column >= mapColumns || column < 0) GameOver = true;

            return map.Cells[row, column];
        }

        void GameOverAction()
        {

        }
    }
}
