using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Map.Commands
{
    public class NextMapCommand : ICommand
    {
        private MapHandler map;
        public NextMapCommand(MapHandler map) {
            this.map = map;
        }
        public void Execute()
        {
            map.cycle_next(); 
        }
    }
}
