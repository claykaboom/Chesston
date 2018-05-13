using ChessTonGame.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public class Movements : List<Movement>
    {

        public event Events.PieceMovedEventHandler OnMovementAdded;

        public new void Add(Movement item)
        {
            base.Add(item);
            if (null != OnMovementAdded)
            {
                OnMovementAdded(item);
            }
        }
        public override string ToString()
        {
            StringBuilder finalString = new StringBuilder();
            this.ForEach(m => finalString.AppendLine(m.ToString()));
            return finalString.ToString();
        }

    }
}
