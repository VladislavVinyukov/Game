using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class WorkerRobot : Robot
    {
        private const int workRobotbat = 2500;
        private const int workRobotgrooz = 1500;
        Random random = new Random();
        override public int GetButtery()
        {
            return workRobotbat;
        }

        override public int Grooz()
        {
            return workRobotgrooz;
        }

        override public bool DecodResult()
        {
            return random.Next(0, 100) <= 30;
        }
    }
}
