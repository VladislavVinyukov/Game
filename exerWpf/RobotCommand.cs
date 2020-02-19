using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class RobotCommand
    {
        private Cloud cloud;
        private Robot robot;
        public RobotCommand(Robot robot)
        {
            this.robot = robot;
        }

        public void Undo(History his)
        {
            cloud = his.Beckup();
            robot.chargeOfRobot = cloud.Battary;
            robot.sumOfCargo = cloud.Cash;
        }

        public double GetPosX()
        {
            return cloud.PosX;
        }

        public double GetPosY()
        {
            return cloud.PosY;
        }
    }
}
