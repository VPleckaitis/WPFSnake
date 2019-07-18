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
        private MovementDirection mov = MovementDirection.None;
        public MovementDirection Direction
        {
            get { return mov; }
            set { mov = value; }
        }

        public enum MovementDirection
        {
            None,
            Up,
            Down,
            Left,
            Right
        };
  
    }
}
