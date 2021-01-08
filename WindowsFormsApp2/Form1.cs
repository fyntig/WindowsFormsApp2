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
                        v;  //направление - как часы: 1, 2 ,3 ,4 ,5 ,6 ,7, 8
            public double r = 1d; //степень близости к наиболее близкой точке второго рисунка

            public xPoint(int _x, int _y, int _t) { x = _x; y = _y; t = _t; }
            public xPoint(int _x, int _y, int _t, int _v) { x = _x; y = _y; t = _t; v = _v; }

            public void GetInfo()
            {
    
                MessageBox.Show($"координаты: {x};{y}, расстояние: {r}");
            }

        }

        //если точка чёрная возвращает 1
        private bool isBlack(Color cl)
        {
            bool Result = false;
            if (cl.A == 255 && cl.R == 0 && cl.G == 0 && cl.B == 0) Result = true;
            return Result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<xPoint> i1 = new List<xPoint>();
            List<xPoint> i2 = new List<xPoint>();

            //Pen _p = new Pen(Color.Black);

            //это код который определяет чёрный пиксель (крайние игнорируются, потому что он всегда будет окончанием или пересечением)
            for (int _x = 1; _x < b1.Width; _x++)
                for (int _y = 1; _y < b1.Height; _y++)
                {

                    if (isBlack(b1.GetPixel(_x, _y)))
                    //if (b1.GetPixel(_x, _y).A == 255 && b1.GetPixel(_x, _y).R == 0 && b1.GetPixel(_x, _y).G == 0 && b1.GetPixel(_x, _y).B == 0)
                    //if (Color.Equals(b1.GetPixel(_x, _y), Color.Black)) так требуется перегрузка операторов == и != для структуры (Color не тут, поэтому не сможем)
                    {
                        int _v = 0, _s = 0; //направление окончания и сумма соседей 0 - точка, 1 - окончание, 2 - линия 3+ - пересечение
                                            //определяем тип этого пикселя пересечение, окончание или линия
                                            //определим его соседей
                        /*for (int __x = -1; __x < 2; __x++)
                        for (int __y = -1; __y < 2; __y++)
                        {
                                __v++;
                                
                                // это не то что надо, надо считат ьне количество чёрных пискелей, а количество смен белого на чёрный
                                if (b1.GetPixel(_x + __x, _y + __y).A == 255 && b1.GetPixel(_x + __x, _y + __y).R == 0 && b1.GetPixel(_x + __x, _y + __y).G == 0 && b1.GetPixel(_x + __x, _y + __y).B == 0)
                                {
                                    _s++;
                                    if (__x != 0 && __y != 0) _v = __v;
                                } }*/

                        if (isBlack(b1.GetPixel(_x - 1, _y - 1))) //первый пиксель в обходе (левый верхний)
                            if (!isBlack(b1.GetPixel(_x - 1, _y + 0))) //слева от проверяемого пикселя  
                            {
                                _s++;
                                _v = 1;
                            }

                        if (isBlack(b1.GetPixel(_x + 0, _y - 1))) // верхний
                            if (!isBlack(b1.GetPixel(_x - 1, _y - 1)))
                            {
                                _s++;
                                _v = 2;
                            }

                        if (isBlack(b1.GetPixel(_x + 1, _y - 1))) // правый верхний
                            if (!isBlack(b1.GetPixel(_x + 0, _y - 1)))
                            {
                                _s++;
                                _v = 3;
                            }

                        if (isBlack(b1.GetPixel(_x + 1, _y + 0))) // правый
                            if (!isBlack(b1.GetPixel(_x + 1, _y - 1)))
                            {
                                _s++;
                                _v = 4;
                            }

                        if (isBlack(b1.GetPixel(_x + 1, _y + 1))) // правый нижний
                            if (!isBlack(b1.GetPixel(_x + 1, _y + 0)))
                            {
                                _s++;
                                _v = 5;
                            }

                        if (isBlack(b1.GetPixel(_x + 0, _y + 1))) // нижний
                            if (!isBlack(b1.GetPixel(_x + 1, _y + 1)))
                            {
                                _s++;
                                _v = 6;
                            }

                        if (isBlack(b1.GetPixel(_x - 1, _y + 1))) // левый нижний
                            if (!isBlack(b1.GetPixel(_x + 0, _y + 1)))
                            {
                                _s++;
                                _v = 7;
                            }

                        if (isBlack(b1.GetPixel(_x - 1, _y + 0))) // левый
                            if (!isBlack(b1.GetPixel(_x - 1, _y + 1)))
                            {
                                _s++;
                                _v = 8;
                            }
                        //MessageBox.Show(_s.ToString());

                        switch (_s)
                        {
                            case 0:
                                //i1.Add(new xPoint(_x, _y, 0));
                                //MessageBox.Show($"point {_x} {_y} чёрная");
                                break;
                            case 1:
                                i1.Add(new xPoint(_x, _y, 2, _v));
                                //MessageBox.Show("vector");
                                //i1.Last().GetInfo();
                                break;
                            case 2:
                                //i1.Add(new xPoint(_x, _y, 3));
                                //i1.Last().GetInfo();
                                //MessageBox.Show($"line {_x} {_y} чёрная");
                                break;
                            default:
                                //MessageBox.Show("cross");
                                //i1.Add(new xPoint(_x, _y, 1));
                                break;
                        }
                    }

                        if (isBlack(b2.GetPixel(_x, _y)))
                        //if (b1.GetPixel(_x, _y).A == 255 && b1.GetPixel(_x, _y).R == 0 && b1.GetPixel(_x, _y).G == 0 && b1.GetPixel(_x, _y).B == 0)
                        //if (Color.Equals(b1.GetPixel(_x, _y), Color.Black)) так требуется перегрузка операторов == и != для структуры (Color не тут, поэтому не сможем)
                        {
                            int _v = 0, _s = 0; //направление окончания и сумма соседей 0 - точка, 1 - окончание, 2 - линия 3+ - пересечение
                                                //определяем тип этого пикселя пересечение, окончание или линия
                                                //определим его соседей
                            /*for (int __x = -1; __x < 2; __x++)
                            for (int __y = -1; __y < 2; __y++)
                            {
                                    __v++;

                                    // это не то что надо, надо считат ьне количество чёрных пискелей, а количество смен белого на чёрный
                                    if (b1.GetPixel(_x + __x, _y + __y).A == 255 && b1.GetPixel(_x + __x, _y + __y).R == 0 && b1.GetPixel(_x + __x, _y + __y).G == 0 && b1.GetPixel(_x + __x, _y + __y).B == 0)
                                    {
                                        _s++;
                                        if (__x != 0 && __y != 0) _v = __v;
                                    } }*/

                            if (isBlack(b2.GetPixel(_x - 1, _y - 1))) //первый пиксель в обходе (левый верхний)
                                if (!isBlack(b2.GetPixel(_x - 1, _y + 0))) //слева от проверяемого пикселя  
                                {
                                    _s++;
                                    _v = 1;
                                }

                            if (isBlack(b2.GetPixel(_x + 0, _y - 1))) // верхний
                                if (!isBlack(b2.GetPixel(_x - 1, _y - 1)))
                                {
                                    _s++;
                                    _v = 2;
                                }

                            if (isBlack(b2.GetPixel(_x + 1, _y - 1))) // правый верхний
                                if (!isBlack(b2.GetPixel(_x + 0, _y - 1)))
                                {
                                    _s++;
                                    _v = 3;
                                }

                            if (isBlack(b2.GetPixel(_x + 1, _y + 0))) // правый
                                if (!isBlack(b2.GetPixel(_x + 1, _y - 1)))
                                {
                                    _s++;
                                    _v = 4;
                                }

                            if (isBlack(b2.GetPixel(_x + 1, _y + 1))) // правый нижний
                                if (!isBlack(b2.GetPixel(_x + 1, _y + 0)))
                                {
                                    _s++;
                                    _v = 5;
                                }

                            if (isBlack(b2.GetPixel(_x + 0, _y + 1))) // нижний
                                if (!isBlack(b2.GetPixel(_x + 1, _y + 1)))
                                {
                                    _s++;
                                    _v = 6;
                                }

                            if (isBlack(b2.GetPixel(_x - 1, _y + 1))) // левый нижний
                                if (!isBlack(b2.GetPixel(_x + 0, _y + 1)))
                                {
                                    _s++;
                                    _v = 7;
                                }

                            if (isBlack(b2.GetPixel(_x - 1, _y + 0))) // левый
                                if (!isBlack(b2.GetPixel(_x - 1, _y + 1)))
                                {
                                    _s++;
                                    _v = 8;
                                }
                            //MessageBox.Show(_s.ToString());

                            switch (_s)
                            {
                                case 0:
                                    //i2.Add(new xPoint(_x, _y, 0));
                                    //MessageBox.Show($"point {_x} {_y} чёрная");
                                    break;
                                case 1:
                                    i2.Add(new xPoint(_x, _y, 2, _v));
                                    //MessageBox.Show("vector");
                                    break;
                                case 2:
                                    //i2.Add(new xPoint(_x, _y, 3));
                                    //i2.Last().GetInfo();
                                    //MessageBox.Show($"line {_x} {_y} чёрная");
                                    break;
                                default:
                                    //MessageBox.Show("cross");
                                    //i2.Add(new xPoint(_x, _y, 1));
                                    break;
                            }
                        }   
                }

            foreach (xPoint i in i1)
                foreach (xPoint j in i2)
                {
                    //пока рассчёт только для окончаний
                    //if (i.t == 2)
                    //MessageBox.Show("i1: " + i.x.ToString() + "; " + i.y.ToString());

                    double tmp_a = Math.Abs(Math.Abs(4 - i.v) - Math.Abs(4 - j.v));
                    double tmp_b = (Math.Abs(i.x - j.x) + 1) * (Math.Abs(i.y - j.y ) + 1);
                    double tmp_i = 1000 * tmp_a / tmp_b;
                    //MessageBox.Show(tmp_i.ToString() + "=" + tmp_a.ToString() + "/" + tmp_b.ToString() + " ->" + i.x.ToString() + ","+i.y.ToString() + ";" + j.x.ToString() + "," + j.y.ToString());
                    if (i.r < tmp_i) i.r = tmp_i;
                    if (j.r < tmp_i) j.r = tmp_i;                       

                }

            double res1 = 0d, res2 = 0d;
            foreach (xPoint i in i1)
            {
                //MessageBox.Show(i.r.ToString());
                res1 += i.r;
            }
            res1 = res1 / i1.Count;

            MessageBox.Show(res1.ToString());
            //MessageBox.Show("i1: " + i.r.ToString());
            foreach (xPoint i in i2)
            {
                //MessageBox.Show(i.r.ToString());
                res2 += i.r;
            }
            res2 = res2 / i2.Count;

            MessageBox.Show(res2.ToString());

            if (res1 > 10 && res2 > 10)
                MessageBox.Show("ПОХОЖИ!");
            else
                MessageBox.Show("РАЗНЫЕ");


        }
    }
}
