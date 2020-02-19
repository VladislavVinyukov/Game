using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class History
    {
        public Stack<Cloud> clouds;

        public History()
        {
            clouds = new Stack<Cloud>();
        }

        public void Save(Cloud cloudRobot)
        {
            clouds.Push(cloudRobot);
        }
        public Cloud Beckup()
        {
            return clouds.Pop();
        }

    }
}
