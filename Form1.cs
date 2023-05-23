using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_4._1
{
    
    public partial class Form1 : Form
    {
        private List<CCircle> Circles = new List<CCircle>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked == false)//не нажат ctrl
            {
                foreach (CCircle list_Circle in Circles)
                {
                    list_Circle.set_color("Black"); //снимает выделение со всех объектов
                }
                CCircle Circle = new CCircle(e.X, e.Y, 20);//создает новый объект с выделением
                Circles.Add(Circle);
            }
            if (checkBox1.Checked == true) //нажат ctrl
            {
                foreach (CCircle list_Circle in Circles)
                {
                    if (list_Circle.checkCircle(e) == true && checkBox2.Checked == true) //проверка на массо-вое выделение
                    {
                        break;
                    }
                }
                Refresh();
            }
            Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                checkBox1.Checked = true;
                CCircle.set_ctrl(true);
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            checkBox1.Checked = false;
            CCircle.set_ctrl(false);
        }
        public class CCircle
        {
            private int x, y;
            private float radius;
            private string color = "Red";
            private static bool check_ctrl = false;
            public CCircle(int x, int y, float radius)
            {
                this.x = x;
                this.y = y;
                this.radius = radius;
            }
            public void draw(Graphics Canvas)
            {
                if (color == "Red")
                {
                    Canvas.DrawEllipse(new Pen(Color.Red), x - radius, y - radius, radius * 2, radius * 2);
                }
                else
                {
                    Canvas.DrawEllipse(new Pen(Color.Black), x - radius, y - radius, radius * 2, radius * 2);
                }
            }
            public void set_color(string color)
            {
                this.color = color;
            }
            public string get_color()
            {
                return color;
            }
            public bool checkCircle(MouseEventArgs e)
            {
                if (check_ctrl)
                {
                    if (Math.Pow(e.X - x, 2) + Math.Pow(e.Y - y, 2) <= Math.Pow(radius, 2) && color != "Red")
                    {
                        color = "Red";
                        return true;
                    }
                }
                return false;
            }
            static public void set_ctrl(bool check) //сеттер флага выделения
            {
                check_ctrl = check;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Circles.Count(); i++)
            {
                if (Circles[i].get_color() == "Red") //проверка выделения объектов
                {
                    Circles.RemoveAt(i); //удаление выделенных объектов
                    --i;
                }
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CCircle list_Circle in Circles)
            {
                list_Circle.draw(e.Graphics); //Рисует все круги из списка
            }

        }
    }
}
