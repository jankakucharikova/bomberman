using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Map
    { 
        private char[,] plane;
        int width;
        int heigth;
        public int countofmonsters;
        public int level;

        public Status stav = Status.notstarted;

        Bitmap[] icons;
        int sx; 
        public Hero hero;
        public List<MovingElement> MovingElementsExceptTheHero;
        public PressedKey pressedkey;


        public Map(string MapPath, string IconsPath, int level)
        {
            this.level = level;
            LoadIcons(IconsPath);
            LoadMap(MapPath, level);
            stav = Status.running;
        }

        public void Move(int fromx, int fromy, int tox, int toy)
        {
            char c = plane[fromx, fromy];
            plane[fromx, fromy] = 'G';
            plane[tox, toy] = c;
            
            if (c == 'H')
            {
                hero.x = tox;
                hero.y = toy;
                return; 
            }

            foreach (MovingElement p in MovingElementsExceptTheHero)
            {
                if ((p.x == fromx) && (p.y == fromy))
                {
                    p.x = tox;
                    p.y = toy;
                    break;
                }
            }
        }

        public void DeleteMovingElement(int x, int y)
        {
            for (int i = 0; i < MovingElementsExceptTheHero.Count; i++)
            {
                if ((MovingElementsExceptTheHero[i].x == x) && (MovingElementsExceptTheHero[i].y == y))
                {
                    MovingElementsExceptTheHero.RemoveAt(i);
                    break;
                }
            }
            plane[x, y] = 'G';
        }

      
        public bool IsHero(int x, int y)
        {
            return plane[x, y] == 'H';
        }

        public bool IsDoor(int x, int y)
        {
            return plane[x, y] == 'D';
        }

        public bool FreeForHero(int x, int y)
        {
            return (plane[x, y] == 'G' || plane[x,y]=='D');
        }

        public bool IsGrass(int x, int y)
        {
            return (plane[x, y] =='G');
        }

        public bool IsWall(int x, int y)
        {
            return (plane[x, y] == 'X');
        }

        public bool FreeForMonster(int x, int y)
        {
            return (plane[x, y] == 'G'|| plane[x,y]== 'H' || plane[x, y] == 'D');
        }
        
        public void ToGrass(int x , int y)
        {
            plane[x, y] = 'G';
        }

        public void ToDoor(int x, int y)
        {
            plane[x, y] = 'D';
        }

        public void PlantBomb(int x, int y)
        {
            plane[x, y] = 'B';            
        }

        public void ShowExplosion(int x,int y)
        {
            plane[x, y] = 'T';
        }

        public void LoadMap(string path, int level)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(path);
            for (int i = 0; i < level; i++)
            {
                MovingElementsExceptTheHero = new List<MovingElement>();
                countofmonsters = 0;
                width = int.Parse(sr.ReadLine());
                heigth = int.Parse(sr.ReadLine());
                plane = new char[width, heigth];                

                for (int y = 0; y < heigth; y++)
                {
                    string line = sr.ReadLine();
                    for (int x = 0; x < width; x++)
                    {
                        char symbol = line[x];
                        plane[x, y] = symbol;
                       
                        switch (symbol)
                        {
                            case 'B':
                                Bomb Bomb = new Bomb(this, x, y);
                                MovingElementsExceptTheHero.Add(Bomb);
                                break;

                            case 'H':
                                this.hero = new Hero(this, x, y);
                                break;

                            case 'M':
                                Monster Monster = new Monster(this, x, y);
                                MovingElementsExceptTheHero.Add(Monster);
                                countofmonsters++;
                                break;

                            case 'W':
                                Barrier Barrier = new Barrier(this, x, y);
                                MovingElementsExceptTheHero.Add(Barrier);
                                break;

                            case 'D':
                                Door Door = new Door(this, x, y);
                                plane[x, y] = 'W';
                                MovingElementsExceptTheHero.Add(Door);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            sr.Close();
        }
        public void LoadIcons(string path)
        {
            Bitmap bmp = new Bitmap(path);
            this.sx = bmp.Height;
            int count = bmp.Width / sx; 
            icons = new Bitmap[count];
            for (int i = 0; i < count; i++)
            {
                Rectangle rect = new Rectangle(i * sx, 0, sx, sx);
                icons[i] = bmp.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);
            }
        }

        public void DrawMap(Graphics g, int WidthOfWindow, int HeightOfWindow)
        {
            int wsection = WidthOfWindow / sx;
            int hsection = HeightOfWindow / sx;

            if (wsection > width)
                wsection = width;

            if (hsection > heigth)
                hsection = heigth;

            int dx = hero.x - wsection / 2;
            if (dx < 0)
                dx = 0;
            if (dx + wsection - 1 >= this.width)
                dx = this.width - wsection;

            int dy = hero.y - hsection / 2;
            if (dy < 0)
                dy = 0;
            if (dy + hsection - 1 >= this.heigth)
                dy = this.heigth - hsection;

            for (int x = 0; x < wsection; x++)
            {
                for (int y = 0; y < hsection; y++)
                {
                    int mx = dx + x; 
                    int my = dy + y; 

                    char c = plane[mx, my];
                    int indexOfIcon = "XBDGMHWT".IndexOf(c); 

                    g.DrawImage(icons[indexOfIcon], x * sx, y * sx);
                }
            }
        }

        public void MoveAllElements(PressedKey pressedkey)
        {
            this.pressedkey = pressedkey;
            for(int i =0;i<MovingElementsExceptTheHero.Count;)
            {
                MovingElementsExceptTheHero[i].MakeMove();
                if ((i<MovingElementsExceptTheHero.Count) && (MovingElementsExceptTheHero[i] != null))
                    i++;
            }
            hero.MakeMove();
        }
    }
}
 