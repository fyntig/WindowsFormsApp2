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
        Bitmap b1 = new Bitmap(200, 200), b2 = new Bitmap(200, 200);

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
                panel1.BackgroundImage = b1;
                panel1.Refresh();
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
                panel2.BackgroundImage = b2;
                panel2.Refresh();
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        public Form1()
        {
            InitializeComponent();
            g1 = Graphics.FromImage(b1);
            g1.Clear(Color.White);
            g2 = Graphics.FromImage(b2);
            g2.Clear(Color.White);
        }

        class xPoint
        {
            public int x, y, //координата
                        t,  //тип: 1 - пересечение, 2 - окончание, 3 - точка линии
                        v;  //направление - как часы: 1, 3 ,5 ,6 ,7 ,9 ,11, 12
            public double r; //степень близости к наиболее близкой точке второго рисунка

            public xPoint(int _x, int _y, int _t) { x = _x; y = _y; t = _t; }
            public xPoint(int _x, int _y, int _t, int _v) { x = _x; y = _y; t = _t; v = _v; }
          
        }

        List<xPoint> i1 = new List<xPoint>();
        List<xPoint> i2 = new List<xPoint>();

        private void button1_Click(object sender, EventArgs e)
        {

            //это код который определяет чёрный пиксель
            for (int _x = 0; _x < 200; _x++)
                for (int _y = 0; _y < 200; _y++)
                {
                    if (_x == 100 && _y == 100)
                    {
                        b1.SetPixel(_x, _y, Color.Black);
                        panel1.BackgroundImage = b1;
                        panel1.Refresh();                        
                        if (b1.GetPixel(_x, _y).A == 255 && b1.GetPixel(_x, _y).R == 0 && b1.GetPixel(_x, _y).G == 0 && b1.GetPixel(_x, _y).B == 0)
                        {
                            //определяем тип этого пикселя пересечение, окончание или линия
                            MessageBox.Show("fgf");

                        }
                        MessageBox.Show(b1.GetPixel(_x, _y).ToString());
                        break;
                    }
                }


           // foreach (xPoint i in i1)
            //    MessageBox.Show(i.x.ToString()); ;
        }
    }
}
