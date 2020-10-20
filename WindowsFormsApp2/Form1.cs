using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Graphics g1, g2;
        Point CurrentPoint, PrevPoint;
        bool isPressed;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            CurrentPoint = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                PrevPoint = CurrentPoint;
                CurrentPoint = e.Location;
                Pen _p = new Pen(Color.Black);
                g1.DrawLine(_p, PrevPoint, CurrentPoint);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            CurrentPoint = e.Location;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                PrevPoint = CurrentPoint;
                CurrentPoint = e.Location;
                Pen _p = new Pen(Color.Black);
                g2.DrawLine(_p, PrevPoint, CurrentPoint);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        public Form1()
        {
            InitializeComponent();
            g1 = panel1.CreateGraphics();
            g2 = panel2.CreateGraphics();
        }

        class xPoint
        {
            public int x, y; //координата
            public string t;  //тип: cross - пересечение, end - окончание, line - точка линии
            public int v;     //направление - как часы: 1, 3 ,5 ,6 ,7 ,9 ,11, 12

            public xPoint(string _t, int _x, int _y) { t = _t; x = _x; y = _y; }
            public xPoint(string _t, int _v, int _x, int _y) { t = _t; v = _v; x = _x; y = _y; }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
