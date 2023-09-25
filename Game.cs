using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman

{
    public partial class GAME : Form
    {
        public GAME()
        {
            InitializeComponent();
        }

        Map map;
        Graphics g;
        int level = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            g = CreateGraphics();
            map = new Map("plan.txt", "ikonky.png", level);
            this.Text = "Počet nezabitých príšer: " + map.countofmonsters + ".";
            timer1.Enabled = true;
            button1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (map.stav)
            {
                case Status.running:
                    map.MoveAllElements(pressedkey);
                    map.DrawMap(g, ClientSize.Width, ClientSize.Height);
                    this.Text = "Počet nezabitých príšer: " + map.countofmonsters+ ".";
                    break;

                case Status.win:
                    if (level < 3)
                    {
                        map = new Map("plan.txt", "ikonky.png", ++level);
                        map.stav = Status.running;
                    }
                    else
                    {
                        timer1.Enabled = false;
                        g.Clear(Color.White);
                        MessageBox.Show("Vyhra!");
                        this.Dispose();
                    }
                        break;

                case Status.lose:
                    timer1.Enabled = false;
                    g.Clear(Color.Green);
                    MessageBox.Show("Prohra!");
                    this.Dispose();
                    break;

                default:
                    break;
            }
        }

        PressedKey pressedkey = PressedKey.none;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                pressedkey = PressedKey.space;
                return true;
            }
            if (keyData == Keys.Up)
            {
                pressedkey = PressedKey.up;
                return true;
            }
            if (keyData == Keys.Down)
            {
                pressedkey = PressedKey.down;
                return true;
            }
            if (keyData == Keys.Left)
            {
                pressedkey = PressedKey.left;
                return true;
            }
            if (keyData == Keys.Right)
            {
                pressedkey = PressedKey.right;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            pressedkey = PressedKey.none;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
