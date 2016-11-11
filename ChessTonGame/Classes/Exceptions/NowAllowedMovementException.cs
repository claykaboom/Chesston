using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Exceptions
{
   public class NowAllowedMovementException:Exception
    {
       public NowAllowedMovementException():base("Movimento não permitido")
       {

       }
    }
}
