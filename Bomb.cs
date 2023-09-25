using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Bomb: MovingElement
    { 
        int timeexplode;
        public Bomb(Map map, int x, int y)
        {
            timeexplode = 10;
            this.map = map;
            this.x = x;
            this.y = y;
        }
        public override void MakeMove()
        {
            timeexplode--;
            if (timeexplode < 1) Explode();
            
        }
        public override void Explode()
        {
            if (map.hero.x == x && map.hero.y == y)
                map.stav = Status.lose;
            map.hero.countofbombs--;
            map.DeleteMovingElement(x, y);
            map.MovingElementsExceptTheHero.Add(new Explosion(map, x, y));
            map.ShowExplosion(x, y);
            for (int i = -1; i < 2; i += 2)
            {
                if (!map.IsWall(x + i, y))

                    if (map.IsGrass(x + i, y))
                    {
                        map.MovingElementsExceptTheHero.Add(new Explosion(map, x + i, y));
                        map.ShowExplosion(x + i, y);
                    }
                    else
                    {
                        foreach (MovingElement p in map.MovingElementsExceptTheHero)
                            if (p.x == x + i && p.y == y)
                            {
                                p.Explode();
                                break;
                            }
                        if (map.hero.x == x + i && map.hero.y == y) map.hero.Explode();

                    }
                if (!map.IsWall(x, y + i))

                    if (map.IsGrass(x, y + i))
                    {
                        map.MovingElementsExceptTheHero.Add(new Explosion(map, x, y + i));
                        map.ShowExplosion(x, y + i);
                    }
                    else
                    {
                        foreach (MovingElement p in map.MovingElementsExceptTheHero)
                            if (p.x == x && p.y == y + i)
                            {
                                p.Explode();
                                break;
                            }
                        if (map.hero.x == x && map.hero.y == y+i) map.hero.Explode();
                    }
            }

        }


    }
}
