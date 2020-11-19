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
                        t,  //тип: 1 - пересечение, 2 - окончание, 3 - точка линии, 0 - точка
                        v;  //направление - как часы: 1, 3 ,5 ,6 ,7 ,9 ,11, 12
            public double r; //степень близости к наиболее близкой точке второго рисунка

            public xPoint(int _x, int _y, int _t) { x = _x; y = _y; t = _t; }
            public xPoint(int _x, int _y, int _t, int _v) { x = _x; y = _y; t = _t; v = _v; }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<xPoint> i1 = new List<xPoint>();
            List<xPoint> i2 = new List<xPoint>();

            Pen _p = new Pen(Color.Black);



            //это код который определяет чёрный пиксель (крайние игнорируются, потому что он всегда будет окончанием или пересечением)
            for (int _x = 1; _x < 199; _x++)
                for (int _y = 1; _y < 199; _y++)


                    if (b1.GetPixel(_x, _y).A == 255 && b1.GetPixel(_x, _y).R == 0 && b1.GetPixel(_x, _y).G == 0 && b1.GetPixel(_x, _y).B == 0)
                    {
                        int _v = 0, _s = -1, __v = 1; //направление окончания и сумма соседей 0 - точка, 1 - окончание, 2 - линия 3+ - пересечение
                                                      //определяем тип этого пикселя пересечение, окончание или линия
                                                      //определим его соседей
                        for (int __x = -1; __x < 2; __x++)
                            for (int __y = -1; __y < 2; __y++)
                            {
                                __v++;
                                if (b1.GetPixel(_x + __x, _y + __y) == Color.Black)
                                {
                                    _s++;
                                    if (__x != 0 && __y != 0) _v = __v;
                                }
                            }

                        switch (_s)
                        {
                            case 0:
                                i1.Add(new xPoint(_x, _y, 0));
                                break;
                            case 1:
                                i1.Add(new xPoint(_x, _y, 2, _v));
                                break;
                            case 2:
                                i1.Add(new xPoint(_x, _y, 3));
                                break;
                            default:
                                MessageBox.Show("!!");
                                i1.Add(new xPoint(_x, _y, 1));
                                break;
                        }
                    }
                 


            for (int _x = 1; _x < 199; _x++)
            for (int _y = 1; _y < 199; _y++)
                    
                if (b2.GetPixel(_x, _y).A == 255 && b2.GetPixel(_x, _y).R == 0 && b2.GetPixel(_x, _y).G == 0 && b2.GetPixel(_x, _y).B == 0)
                        {
                        
                        int _v = 0, _s = -1, __v = 1; //направление окончания и сумма соседей 0 - точка, 1 - окончание, 2 - линия 3+ - пересечение
                                                      //определяем тип этого пикселя пересечение, окончание или линия
                                                      //определим его соседей
                        for (int __x = -1; __x < 2; __x++)
                            for (int __y = -1; __y < 2; __y++)
                            {
                                __v++;
                                if (b2.GetPixel(_x+__x, _y+__y) == Color.Black)
                                {
                                    _s++;
                                    if (__x != 0 && __y != 0) _v = __v;
                                }
                            }

                        if (_s == 0)
                                i2.Add(new xPoint(_x, _y, 0));
                        if (_s == 1) {
                            MessageBox.Show("!");
                            i2.Add(new xPoint(_x, _y, 2, _v));
                        }
                        if (_s == 2)
                            i2.Add(new xPoint(_x, _y, 3));
                        if (_s > 2)
                            i2.Add(new xPoint(_x, _y, 1));
                             
                        

                        }

                


            // foreach (xPoint i in i1)
            //    MessageBox.Show(i.x.ToString()); ;
        }
    }
}
