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
        private bool mapHasChanged = false; //just so we know we have to redraw it on canvas
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

        public bool MapHasChanged
        {
            get { return mapHasChanged; }
            set { mapHasChanged = value; }
        }

        private MovementDirection direction = MovementDirection.None;
        public MovementDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private Cell foodCell;
        public Cell FoodCell
        {
            get { return foodCell; }

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

            foodCell = map.AddFood();
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

        public void Update()
        {
            if (direction != MovementDirection.None)
            {
                try
                {
                    Cell next = GetNextCell(snake.SnakeHead);
                    if (!gameOver) // If we haven't hit border yet
                        if (snake.SnakeHitTheCell(next)) gameOver = true;
                        else if (map.Cells[next.Row, next.Column].CellType == Cell.CellTypeEnum.DIGESTED_FOOD)
                        {
                            gameOver = true;
                        }
                        else
                        {
                            snake.Move(next);
                            if (next.CellType == Cell.CellTypeEnum.FOOD)
                            {
                                snake.Grow();
                                map.UpdateCell(next.Row, next.Column, Cell.CellTypeEnum.DIGESTED_FOOD);
                                foodCell = map.AddFood();
                                MapHasChanged = true; // we've ate / added food and so we need to redraw it
                            }


                        }
                }
                catch { gameOver = true; }
            }
        }

       

        void GameOverAction()
        {

        }
    }
}
