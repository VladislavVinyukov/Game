using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Earth
    {

        public Random random = new Random();

        public Box GetBox()
        {
            return new Box(random);
        }
    }
}
