using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class KiborgRobot: Robot
    {
        private const int kibRobbat = 2000;
        private const int kibRobgrooz = 1000;
        Random random = new Random();
        override public int GetButtery()
        {
            return kibRobbat;
        }

        override public int Grooz()
        {
            return kibRobgrooz;
        }

        override public bool DecodResult()
        {
            return random.Next(0, 100) <= 60;
        }
    }
}
