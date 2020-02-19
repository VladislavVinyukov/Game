using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using robots;

namespace Game
{
    class Box:ABox
    {
        public int Price
        {
            get;
            private set;
        }
        public int Distance
        {
            get;
            private set;
        }
        public int Mass
        {
            get;
            private set;
        }
        public bool Toxic
        {
            get;
            private set;
        }
        public bool NeedDecoding
        {
            get;
            private set;
        }
        public bool Fail
        {
            get;
            private set;
        }
        public Box(Random random)
        {
            Distance = random.Next(1,100);
            Mass = random.Next(1, 70);
            Toxic = random.Next(0, 100) >= 50;
            NeedDecoding = random.Next(0, 100) >= 50;
            Fail = random.Next(0, 100) >= 50;
            Price = random.Next(1, 50);
        }
        public override string ToString()
        {
            return "Price " + Price + " distance "+ Distance + " mass"+ Mass;
        }

        public override int Open()
        {
            return Price;
        }
    }
}
