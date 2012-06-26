using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testgame
{
    interface Player
    {
        public Player();

        private enum PlayerState
        {
            None,
            Penalized,
            Frozen,
        }
    }
}
