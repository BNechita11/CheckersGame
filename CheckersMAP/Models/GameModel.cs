using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMAP.Models
{
    public class GameModel
    {
        public Player WhitePlayer { get; set; }
        public Player RedPlayer { get; set; }

        public GameModel()
        {
            WhitePlayer = new Player();
            RedPlayer = new Player();
        }
    }

}
