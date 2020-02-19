using System;
using System.Collections.Generic;
using System.Configuration;


namespace Game
{
    class SaiceRobot : Robot
    {

        private const int saiRobbat = 500;
        private const int saiRobgrooz = 250;
        override public int GetButtery()
        {
            return saiRobbat;
        }

        override public int Grooz()
        {
            return saiRobgrooz;
        }

        override public bool DecodResult()
        {
            return true;
        }
    }
}
