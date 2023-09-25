using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Barrier: MovingElement
    {       
        public Barrier(Map map, int x, int y)
        {
            this.map = map;
            this.x = x;
            this.y = y;

        }
        public override void MakeMove()
        {
            
        }

        public override void Explode()
        {
            map.ToGrass(x, y);
            map.MovingElementsExceptTheHero.Add(new Explosion(map, x, y));
            map.ShowExplosion(x, y);

        }
    }
}
