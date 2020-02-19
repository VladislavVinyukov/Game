using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Cloud
    {
        public int Cash
        {
            get;
            private set;
        }
        public double Battary
        {
            get;
            private set;
        }
        public double PosX
        {
            get;
            private set;
        }
        public double PosY
        {
            get;
            private set;
        }

        public Cloud(int sum, double battary,double x, double y)
        {
            this.Battary = battary;
            this.Cash = sum;
            PosX = x;
            PosY = y;
        }
    }
}
