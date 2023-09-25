using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    abstract class Element
    {
        
    }

    abstract class MovingElement : Element
    {
        public Map map;
        public int x;
        public int y;
       
        public abstract void MakeMove();
        public abstract void Explode();      

    }

    enum PressedKey { none, left, up, right, down, space };
    public enum Status { notstarted, running, win, lose };
    
}
