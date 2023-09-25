using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Monster : MovingElement
    {   
        int wait;
        public Monster ( Map map, int x, int y)
        {
            this.map = map;
            this.x = x;
            this.y = y;          
        }
        public override void MakeMove()
        {
            wait = (wait + 1) % (12 - 2*map.level);
            if (wait % (12 - 2 * map.level) != 0) return;

            int nove_x = x;
            int nove_y = y;
            List<int> list = new List<int>();

            Random rnd = new Random();
            
            if (map.FreeForMonster(nove_x, nove_y - 1)) list.Add(3);
            if (map.FreeForMonster(nove_x, nove_y + 1)) list.Add(2);
            if (map.FreeForMonster(nove_x - 1, nove_y)) list.Add(0);
            if (map.FreeForMonster(nove_x + 1, nove_y)) list.Add(1);
            if (list.Count != 0) {
                int smer = rnd.Next() % list.Count;
                switch (list[smer])
                {

                    case 0:
                        nove_x--;
                        break;

                    case 1:
                        nove_x++;
                        break;
                    case 2:
                        nove_y++;
                        break;
                    case 3:
                        nove_y--;
                        break;
                    default:
                        return;



                }
                if (map.IsHero(nove_x, nove_y)) map.stav=Status.lose;
                map.Move(x, y, nove_x, nove_y);
               
            }
            
         
        }
        public override void Explode()
        {
            map.DeleteMovingElement(x, y);
            map.countofmonsters--;
            map.MovingElementsExceptTheHero.Add(new Explosion(map, x, y));
            map.ShowExplosion(x, y);

        }


    }
}
