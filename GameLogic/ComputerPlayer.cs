using System;
using System.Collections.Generic;

namespace GameLogic
{
    public abstract class ComputerPlayer<T> : Player
    {
        protected Random random;

        public ComputerPlayer(string name) : base(name)
        {
            random = new Random();
        }

        public abstract (int, int) GetMove(Board<T> board);
    }
}
