using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Exceptions
{
   public class MovimentoNaoPermitidoException:Exception
    {
       public MovimentoNaoPermitidoException():base("Movimento não permitido")
       {

       }
    }
}
