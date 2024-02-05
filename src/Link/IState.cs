using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame;

public interface IState
{
    public Game1.Direction Direction();

    public Game1.Health Health();

}
