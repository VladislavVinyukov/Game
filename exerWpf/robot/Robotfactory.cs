using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Robotfactory
    {
        public Robot GetRobot(Random random)
        {
            int number = random.Next(0, 100);
            if(number <= 20)
            {
                return new SaiceRobot();
            }
            else if (number <= 50)
            {
                return new KiborgRobot();
            }
            else
            {
                return new WorkerRobot();
            }
        }    
    }
}
