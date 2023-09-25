using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Hero: MovingElement
    { 
        bool planting;
        bool door;
        public int countofbombs;
        public Hero(Map map,  int x, int y)
        {
            countofbombs = 0;
            this.map = map;
            this.x = x;
            this.y = y;
        }
        public static int bombx;
        public static int bomby;
        public override void MakeMove()
        {
            int new_x = x;
            int new_y = y;
            int old_x = x;
            int old_y = y;

            switch (map.pressedkey)
            {
                case PressedKey.space:
                    if (!planting && (countofbombs < map.level)&& !door)
                    {
                        planting = true;
                        bombx = x;
                        bomby = y;
                        Bomb bomb = new Bomb(map, x, y);
                        map.MovingElementsExceptTheHero.Add(bomb);
                        countofbombs++;
                    }
                    break;

                case PressedKey.none:
                    break;

                case PressedKey.left:
                    new_x--;
                    break;

                case PressedKey.up:
                    new_y--;
                    break;

                case PressedKey.right:
                    new_x++;
                    break;

                case PressedKey.down:
                    new_y++;
                    break;

                default:
                    break;
            }


            if (map.FreeForHero(new_x, new_y))
            {   
                if (door)
                {
                    map.Move(x, y, new_x, new_y);
                    map.ToDoor(old_x, old_y);
                    door = false;                   

                }
                if (map.IsDoor(new_x, new_y) && map.countofmonsters != 0)
                {
                    door = true;
                    map.Move(x, y, new_x, new_y);
                    
                }
                else if (map.IsDoor(new_x, new_y) && map.countofmonsters == 0) map.stav = Status.win;

                map.Move(x, y, new_x, new_y);

                if (planting && map.pressedkey != PressedKey.none && map.pressedkey != PressedKey.space && !door)
                {
                    map.PlantBomb(bombx, bomby);
                    planting = false;
                    return;
                }
            } 
        }
        public override void Explode()
        {            
            map.MovingElementsExceptTheHero.Add(new Explosion(map, x, y));
            map.ShowExplosion(x, y);
            map.stav = Status.lose;
        }
    }
}
