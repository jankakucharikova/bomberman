using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Door : Barrier
    {
        bool show;
        public Door(Map map, int x, int y): base(map, x, y)
        {
        }
        public override void Explode()
        {
            if (!show)
            {
                map.ToDoor(x, y);
                show = true;
            }
            else map.stav = Status.lose;

        }
    }
}
