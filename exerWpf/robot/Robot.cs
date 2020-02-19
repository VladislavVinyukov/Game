using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using robots;

namespace Game
{
     class Robot
     {

        private const int standbat = 100;
        private const int standgrooz = 30;
        public string Name { get; private set; }
        public double chargeOfRobot = 0;
        public int sumOfCargo = 0;
        public int MaxMassCargo { get; }
        private Proxy proxy;
        private int move = 0;
        public Robot()
        {
            chargeOfRobot = GetButtery();
            MaxMassCargo = Grooz();
            proxy = new Proxy(this);
        }

         public void Go()
         {
             move++;
             chargeOfRobot-=1;
         }
        virtual public int GetButtery()
        {
            return standbat;
        }

        virtual public int Grooz()
        {
            return standgrooz;
        }

        virtual public bool DecodResult()
        {
            return true;
        }
        public override string ToString()
        {
            return "Battary:  "+ chargeOfRobot + " Price" + sumOfCargo + " Move " +move+". \n";
        }
        public void GetGrooz(ABox box)
        {
            
            sumOfCargo += box.Open();


        }

        public Cloud State(double x, double y)
        {
            return new Cloud(sumOfCargo, chargeOfRobot, x, y);
        }
    }

}
