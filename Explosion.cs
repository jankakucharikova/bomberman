
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Explosion : MovingElement
    {
        bool wait = false;
        public Explosion(Map map, int x, int y)
        {
            this.map = map;
            this.x = x;
            this.y = y;

        }
        public override void MakeMove()
        {
            if (wait)
                map.DeleteMovingElement(x, y);
            else wait = true;
        }
        public override void Explode()
        {
            
        }
    }
}
