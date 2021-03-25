using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace AIS
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            InitDataGridView1();
        }

        public Algoritm algst = new Algoritm();
        public string Fname = "";
        public double[,] showobl = new double[2, 2];
        public int z;
        public float[] Ar = new float[8];
        public bool[] flines = new bool[8];
        public double exact = 0;
        public float[] A = new float[8];
        public double[,] showoblbase = new double[2, 2];
        public double[,] oblbase = new double[2, 2];

        bool flag = false;
        bool[] Red = new bool[10];
        Random R = new Random();

        int t = 0;


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //сокращение
            if (Red[4] == true)
            {
                Red[4] = false;
                Red[6] = true;
                algst.Sokrashenie();
                m = algst.Pop.Count;
                dataGridView1.Rows[1].Cells[1].Value = algst.Pop.Count;
                dataGridView1.Rows[2].Cells[1].Value = m;
                dataGridView1.Rows[3].Cells[1].Value = Math.Round(algst.AverF[t],6);
                dataGridView1.Rows[4].Cells[1].Value = Math.Round(algst.AverF[t] - algst.AverF[t - 1], 6);
                dataGridView1.Rows[5].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 4).ToString() + "; " + Math.Round(algst.Pop[0][1], 4).ToString() + ")";
                dataGridView1.Rows[6].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
                dataGridView1.Rows[7].Cells[1].Value = exact.ToString();
                dataGridView1.Rows[8].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
                    
                pictureBox3.Refresh();
                pictureBox1.Refresh();
                pictureBox2.Refresh();
            }
        }


        private void InitDataGridView1()
        {
            dataGridView2.RowCount = 4;
            dataGridView2.Rows[0].Cells[0].Value = "(x*, y*)";
            dataGridView2.Rows[1].Cells[0].Value = "f*";
            dataGridView2.Rows[2].Cells[0].Value = "Точное решение:";
            dataGridView2.Rows[3].Cells[0].Value = "Отклонение от точного решения:";
          

            dataGridView1.RowCount = 9;
            dataGridView1.Rows[0].Cells[0].Value = "Номер популяции:";
            dataGridView1.Rows[1].Cells[0].Value = "Размер популяции:";
            dataGridView1.Rows[2].Cells[0].Value = "Количество клеток памяти:";
            dataGridView1.Rows[3].Cells[0].Value = "Средняя приспособленность:";
            dataGridView1.Rows[4].Cells[0].Value = "Изменение средней приспос-ти:";
            dataGridView1.Rows[5].Cells[0].Value = "Клетка с наилучшей приспос-тью:";
            dataGridView1.Rows[6].Cells[0].Value = "Наилучшая приспособленность:";
            dataGridView1.Rows[7].Cells[0].Value = "Точное решение:";
            dataGridView1.Rows[8].Cells[0].Value = "Отклонение от точного решения:";
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            Pen p1 = new Pen(Color.Black, 2);
            Pen p2 = new Pen(Color.Gray, 2);
            Pen p3 = new Pen(Color.Red, 2);
            Pen p4 = new Pen(Color.Gray, 1);
            Font f1 = new Font("TimesNewRoman", 12,FontStyle.Bold);
            Font f2 = new Font("TimesNewRoman", 9);

            e.Graphics.DrawLine(p4, 20, 100, 20, 215);
            e.Graphics.DrawLine(p4, 345, 100, 345, 215);
            e.Graphics.DrawLine(p4, 20, 100, 345, 100);
            e.Graphics.DrawLine(p4, 20, 215, 345, 215);
            e.Graphics.DrawString("Формирование новой популяции", f2, Brushes.Black, 145, 84);

            e.Graphics.DrawLine(p1, 50, 30, 50, 119);
            e.Graphics.DrawLine(p1, 50, 115, 55, 105);
            e.Graphics.DrawLine(p1, 49, 115, 44, 105);

            if (Red[0] == true)
            {
                e.Graphics.DrawLine(p3, 50, 30, 50, 119);
                e.Graphics.DrawLine(p3, 50, 115, 55, 105);
                e.Graphics.DrawLine(p3, 49, 115, 44, 105);
            }

            e.Graphics.DrawLine(p1, 45, 155, 190, 155);
            e.Graphics.DrawLine(p1, 130, 154, 120, 149);
            e.Graphics.DrawLine(p1, 130, 155, 120, 160);

            if (Red[1] == true)
            {
                e.Graphics.DrawLine(p3, 45, 155, 190, 155);
                e.Graphics.DrawLine(p3, 130, 154, 120, 149);
                e.Graphics.DrawLine(p3, 130, 155, 120, 160);
            }

            e.Graphics.DrawLine(p1, 190, 155, 300, 155);
            e.Graphics.DrawLine(p1, 262, 154, 252, 149);
            e.Graphics.DrawLine(p1, 262, 155, 252, 160);

            if (Red[2] == true)
            {
                e.Graphics.DrawLine(p3, 190, 155, 300, 155);
                e.Graphics.DrawLine(p3, 262, 154, 252, 149);
                e.Graphics.DrawLine(p3, 262, 155, 252, 160);
            }

            e.Graphics.DrawLine(p1, 315, 155, 315, 270);
            e.Graphics.DrawLine(p1, 315, 232, 320, 222);
            e.Graphics.DrawLine(p1, 314, 232, 309, 222);

            if (Red[3] == true)
            {
                e.Graphics.DrawLine(p3, 315, 155, 315, 270);
                e.Graphics.DrawLine(p3, 315, 232, 320, 222);
                e.Graphics.DrawLine(p3, 314, 232, 309, 222);
            }


            e.Graphics.DrawLine(p1, 315, 270, 315, 400);
            e.Graphics.DrawLine(p1, 315, 350, 320, 340);
            e.Graphics.DrawLine(p1, 314, 350, 309, 340);

            if (Red[4] == true)
            {
                e.Graphics.DrawLine(p3, 315, 270, 315, 400);
                e.Graphics.DrawLine(p3, 315, 350, 320, 340);
                e.Graphics.DrawLine(p3, 314, 350, 309, 340);
            }


            e.Graphics.DrawLine(p1, 99, 390, 180, 390);
            e.Graphics.DrawLine(p1, 100, 390, 110, 395);
            e.Graphics.DrawLine(p1, 100, 389, 110, 384);

            if (Red[7] == true)
            {
                e.Graphics.DrawLine(p3, 99, 390, 180, 390);
                e.Graphics.DrawLine(p3, 100, 390, 110, 395);
                e.Graphics.DrawLine(p3, 100, 389, 110, 384);
            }


            e.Graphics.DrawLine(p1, 180, 390, 300, 390);
            e.Graphics.DrawLine(p1, 233, 390, 243, 395);
            e.Graphics.DrawLine(p1, 233, 389, 243, 384);

            if (Red[6] == true)
            {
                e.Graphics.DrawLine(p3, 180, 390, 300, 390);
                e.Graphics.DrawLine(p3, 233, 390, 243, 395);
                e.Graphics.DrawLine(p3, 233, 389, 243, 384);
            }

            e.Graphics.DrawLine(p1, 60, 270, 300, 270);
            e.Graphics.DrawLine(p1, 60, 270, 60, 155);
            e.Graphics.DrawLine(p1, 60, 195, 65, 205);
            e.Graphics.DrawLine(p1, 59, 195, 54, 205);

            if (Red[5] == true)
            {
                e.Graphics.DrawLine(p3, 60, 270, 300, 270);
                e.Graphics.DrawLine(p3, 60, 270, 60, 155);
                e.Graphics.DrawLine(p3, 60, 195, 65, 205);
                e.Graphics.DrawLine(p3, 59, 195, 54, 205);
            }


            e.Graphics.DrawLine(p1, 35, 390, 35, 155);
            e.Graphics.DrawLine(p1, 35, 195, 40, 205);
            e.Graphics.DrawLine(p1, 34, 195, 29, 205);

            if (Red[9] == true)
            {
                e.Graphics.DrawLine(p3, 35, 390, 35, 155);
                e.Graphics.DrawLine(p3, 35, 195, 40, 205);
                e.Graphics.DrawLine(p3, 34, 195, 29, 205);
            }

            e.Graphics.DrawLine(p1, 180, 390, 180, 500);
            e.Graphics.DrawLine(p1, 180, 471, 185, 461);
            e.Graphics.DrawLine(p1, 179, 471, 174, 461);

            if (Red[8] == true)
            {
                e.Graphics.DrawLine(p3, 180, 390, 180, 500);
                e.Graphics.DrawLine(p3, 180, 471, 185, 461);
                e.Graphics.DrawLine(p3, 179, 471, 174, 461);
            }


            e.Graphics.DrawString("да", f1, Brushes.Black, 285, 320);
            e.Graphics.DrawString("нет", f1, Brushes.Black, 160, 245);
            e.Graphics.DrawString("да", f1, Brushes.Black, 150, 435);
            e.Graphics.DrawString("нет", f1, Brushes.Black, 100, 365);


            e.Graphics.DrawLine(p2, 0, 580, 400, 580);
           // e.Graphics.FillEllipse(Brushes.LightPink, 10, 10,200,200);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public int Ip;
        public int K;
        public double[,] obl;
        public int klon;
        public double parkl;
        public int n;
        public double eps;
        public double sigma;
        public double g;
        public int percent;
        public int numb;
        int m = 0;
        bool endreport = false;

        private void button4_Click(object sender, EventArgs e)
        {
            //создание начальной популяции
            endreport = false;
            button15.Enabled = true;
            Red[0] = true;
            for (int i = 1; i < 10; i++)
                Red[i] = false; 
            m = 0;
            algst = new Algoritm();
            algst.Init(Ip,K,obl,z,klon,parkl,n,g,eps,sigma,percent);
            algst.FirstPop();
            flag = true;
            t = 0;
            dataGridView1.Rows[0].Cells[1].Value = t;
            dataGridView1.Rows[1].Cells[1].Value = algst.Pop.Count;
            dataGridView1.Rows[2].Cells[1].Value = m;
            dataGridView1.Rows[3].Cells[1].Value = Math.Round(algst.AverF[t], 6);
            dataGridView1.Rows[5].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 4).ToString() + "; " + Math.Round(algst.Pop[0][1], 4).ToString() + ")";
            dataGridView1.Rows[6].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
            dataGridView1.Rows[7].Cells[1].Value = exact.ToString();
            dataGridView1.Rows[8].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
            numb++;
            if ((File.Exists("protocol.dt")))
            {
                Report prot = new Report();
                prot.Prot1(z, Ip, K, klon, parkl, g, algst.Pop, algst.oblast, numb, eps, sigma, percent);
                if ((File.Exists("protocol.dt")))
                {
                    FileStream fs = new FileStream("protocol.dt", FileMode.Append, FileAccess.Write);
                    StreamWriter r1 = new StreamWriter(fs);
                    r1.Write(prot.toprotocol);
                    r1.Close();
                    fs.Close();
                }
            }

            pictureBox3.Refresh();
            pictureBox1.Refresh();
            pictureBox2.Refresh();

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label1.Text = Fname + " (изображение линий уровня) и популяция клеток:";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            float w = pictureBox1.Width;
            float h = pictureBox1.Height;
            float x0 = w / 2;
            float y0 = h / 2;
            float a = 30;
            float k = 1;


            List<PointF> points = new List<PointF>();
            Pen p1 = new Pen(Color.PaleGreen, 1);
            Pen p2 = new Pen(Color.GreenYellow, 1);
            Pen p3 = new Pen(Color.YellowGreen, 1);
            Pen p4 = new Pen(Color.Yellow, 1);
            Pen p5 = new Pen(Color.Orange, 1);
            Pen p6 = new Pen(Color.OrangeRed, 1);
            Pen p7 = new Pen(Color.Red, 1);
            Pen p8 = new Pen(Color.Brown, 1);
            Pen p9 = new Pen(Color.Maroon, 1);
            Pen p10 = new Pen(Color.Black, 1);
            Pen p11 = new Pen(Color.Blue, 4);

            Font font1 = new Font("TimesNewRoman", 10, FontStyle.Bold);
            Font font2 = new Font("TimesNewRoman", 8);

            pictureBox1.BackColor = Color.White;
         
            //if (flag == true)
            {
                double x1 = showobl[0, 0];
                double x2 = showobl[0, 1];
                double y1 = showobl[1, 0];
                double y2 = showobl[1, 1];



               // z = comboBox1.SelectedIndex;
               // flines = forma3.flines;
               // Ar = forma3.Ar;
                double a1 = Ar[0];//5
                double a3 = Ar[1];//4
                double a5 = Ar[2];//3
                double a7 = Ar[3];//2
                double a9 = Ar[4];//1

                double a10 = Ar[5];//6
                double a11 = Ar[6];//7
                double a12 = Ar[7];//8

                double dx = x2 - x1;
                double dy = y2 - y1;
                double dxy = dx - dy;


                double bxy = Math.Max(dx, dy);
                double step = 0;
                if (bxy < 1.1) step = 0.1;
                else if (bxy < 2.1) step = 0.2;
                else if (bxy < 5.1) step = 0.5;
                else if (bxy < 10.1) step = 1;
                else if (bxy < 20.1) step = 2;
                else if (bxy < 50.1) step = 5;
                else if (bxy < 100.1) step = 10;
                else if (bxy < 200.1) step = 20;
                else if (bxy < 500.1) step = 50;
                else if (bxy < 1000.1) step = 100;
                else if (bxy < 2000.1) step = 200;
                else step = 1000;

                if (dxy > 0)
                {
                    y1 = y1 - dxy / 2;
                    y2 = y2 + dxy / 2;
                }
                else if (dxy < 0)
                {
                    x1 = x1 - Math.Abs(dxy) / 2;
                    x2 = x2 + Math.Abs(dxy) / 2;
                }
                x1 = x1 - 0.05 * Math.Abs(x2 - x1);
                x2 = x2 + 0.05 * Math.Abs(x2 - x1);
                y1 = y1 - 0.05 * Math.Abs(y2 - y1);
                y2 = y2 + 0.05 * Math.Abs(y2 - y1);

                float mw = k * (w) / ((float)(Math.Max(x2 - x1, y2 - y1)));
                float mh = k * (h) / ((float)(Math.Max(x2 - x1, y2 - y1)));
                //for (int i = (x1); i < x2; i++)
                // for (int j = (y1); j < y2; j++)
                for (int ii = 0; ii < w; ii++)
                    for (int jj = 0; jj < h; jj++)
                    {
                        //double mj = j * mh;
                        // double ni = i * mw;
                        double i = (ii * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                        double j = (jj * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                        double i1 = ((ii + 1) * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                        double j1 = ((jj + 1) * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                        double i0 = ((ii - 1) * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                        double j0 = ((jj - 1) * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                        double f = function(i, j, z);// j / k * Math.Sin(Math.Sqrt(Math.Abs(j / k))) + i / k * Math.Sin(Math.Sqrt(Math.Abs(i / k)));
                        double f2 = function(i0, j, z); //(j - 1) / k * Math.Sin(Math.Sqrt(Math.Abs(j - 1) / k)) + i / k * Math.Sin(Math.Sqrt(Math.Abs(i / k)));
                        double f3 = function(i, j0, z); //(j / k) * Math.Sin(Math.Sqrt(Math.Abs(j / k))) + (i - 1) / k * Math.Sin(Math.Sqrt(Math.Abs((i - 1) / k)));
                        double f4 = function(i1, j, z); //(j + 1) / k * Math.Sin(Math.Sqrt(Math.Abs(j + 1) / k)) + (i / k) * Math.Sin(Math.Sqrt(Math.Abs(i / k)));
                        double f5 = function(i, j1, z); //(j / k) * Math.Sin(Math.Sqrt(Math.Abs(j / k))) + (i + 1) / k * Math.Sin(Math.Sqrt(Math.Abs((i + 1) / k)));
                        double f6 = function(i1, j1, z); //(j + 1) / k * Math.Sin(Math.Sqrt(Math.Abs(j + 1) / k)) + (i + 1) / k * Math.Sin(Math.Sqrt(Math.Abs((i + 1) / k)));
                        double f7 = function(i0, j1, z); //(j - 1) / k * Math.Sin(Math.Sqrt(Math.Abs(j - 1) / k)) + (i + 1) / k * Math.Sin(Math.Sqrt(Math.Abs((i + 1) / k)));
                        double f8 = function(i1, j0, z); //(j + 1) / k * Math.Sin(Math.Sqrt(Math.Abs(j + 1) / k)) + (i - 1) / k * Math.Sin(Math.Sqrt(Math.Abs((i - 1) / k)));
                        double f9 = function(i0, j0, z); //(j - 1) / k * Math.Sin(Math.Sqrt(Math.Abs(j - 1) / k)) + (i - 1) / k * Math.Sin(Math.Sqrt(Math.Abs((i - 1) / k)));


                        // if (((f2 < a1) || (f3 < a1) || (f4 < a1) || (f5 < a1) || (f6 < a1) || (f7 < a1) || (f8 < a1) || (f9 < a1)) && (f > a1)) e.Graphics.DrawRectangle(p1, (float)(x0 + mw * j / k), (float)(y0 - mh * i / k), 1, 1);
                        // else if (((f2 < a3) || (f3 < a3) || (f4 < a3) || (f5 < a3) || (f6 < a3) || (f7 < a3) || (f8 < a3) || (f9 < a3)) && (f > a3)) e.Graphics.DrawRectangle(p3, (float)(x0 + mw * j / k), (float)(y0 - mh * i / k), 1, 1);
                        // else if (((f2 < a5) || (f3 < a5) || (f4 < a5) || (f5 < a5) || (f6 < a5) || (f7 < a5) || (f8 < a5) || (f9 < a5)) && (f > a5)) e.Graphics.DrawRectangle(p5, (float)(x0 + mw * j / k), (float)(y0 - mh * i / k), 1, 1);
                        // else if (((f2 < a7) || (f3 < a7) || (f4 < a7) || (f5 < a7) || (f6 < a7) || (f7 < a7) || (f8 < a7) || (f9 < a7)) && (f > a7)) e.Graphics.DrawRectangle(p7, (float)(x0 + mw * j / k), (float)(y0 - mh * i / k), 1, 1);
                        // else if (((f2 < a9) || (f3 < a9) || (f4 < a9) || (f5 < a9) || (f6 < a9) || (f7 < a9) || (f8 < a9) || (f9 < a9)) && (f > a9)) e.Graphics.DrawRectangle(p9, (float)(x0 + mw * j / k), (float)(y0 - mh * i / k), 1, 1);
                        if (((f2 < a1) || (f3 < a1) || (f4 < a1) || (f5 < a1) || (f6 < a1) || (f7 < a1) || (f8 < a1) || (f9 < a1)) && (f > a1) && (flines[4] == true)) e.Graphics.FillRectangle(Brushes.PaleGreen, (float)(ii), (float)(h - jj), 1, 1);
                        else if (((f2 < a3) || (f3 < a3) || (f4 < a3) || (f5 < a3) || (f6 < a3) || (f7 < a3) || (f8 < a3) || (f9 < a3)) && (f > a3) && (flines[3] == true)) e.Graphics.FillRectangle(Brushes.YellowGreen, (float)(ii), (float)(h - jj), 1, 1);
                        else if (((f2 < a5) || (f3 < a5) || (f4 < a5) || (f5 < a5) || (f6 < a5) || (f7 < a5) || (f8 < a5) || (f9 < a5)) && (f > a5) && (flines[2] == true)) e.Graphics.FillRectangle(Brushes.Orange, (float)(ii), (float)(h - jj), 1, 1);
                        else if (((f2 < a7) || (f3 < a7) || (f4 < a7) || (f5 < a7) || (f6 < a7) || (f7 < a7) || (f8 < a7) || (f9 < a7)) && (f > a7) && (flines[1] == true)) e.Graphics.FillRectangle(Brushes.Red, (float)(ii), (float)(h - jj), 1, 1);
                        else if (((f2 < a9) || (f3 < a9) || (f4 < a9) || (f5 < a9) || (f6 < a9) || (f7 < a9) || (f8 < a9) || (f9 < a9)) && (f > a9) && (flines[0] == true)) e.Graphics.FillRectangle(Brushes.Maroon, (float)(ii), (float)(h - jj), 1, 1);
                        else if (((f2 < a10) || (f3 < a10) || (f4 < a10) || (f5 < a10) || (f6 < a10) || (f7 < a10) || (f8 < a10) || (f9 < a10)) && (f > a10) && (flines[5] == true)) e.Graphics.FillRectangle(Brushes.Pink, (float)(ii), (float)(h - jj), 1, 1);
                        else if (((f2 < a11) || (f3 < a11) || (f4 < a11) || (f5 < a11) || (f6 < a11) || (f7 < a11) || (f8 < a11) || (f9 < a11)) && (f > a11) && (flines[6] == true)) e.Graphics.FillRectangle(Brushes.Violet, (float)(ii), (float)(h - jj), 1, 1);
                        else if (((f2 < a12) || (f3 < a12) || (f4 < a12) || (f5 < a12) || (f6 < a12) || (f7 < a12) || (f8 < a12) || (f9 < a12)) && (f > a12) && (flines[7] == true)) e.Graphics.FillRectangle(Brushes.MediumOrchid, (float)(ii), (float)(h - jj), 1, 1);

                    }

                if (flag == true)
                {
                    for (int i = 0; i < algst.Pop.Count; i++)
                        e.Graphics.FillEllipse(Brushes.Blue, (float)((algst.Pop[i][0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algst.Pop[i][1] * k - y1) * h / (y2 - y1) - 3), 5, 5);
                    e.Graphics.FillEllipse(Brushes.Red, (float)((algst.Pop[0][0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algst.Pop[0][1] * k - y1) * h / (y2 - y1) - 3), 7, 7);
                }
                if (Red[1] == true)
                {
                    for (int i = 0; i < algst.Pop.Count; i++)
                        e.Graphics.FillEllipse(Brushes.DarkCyan, (float)((algst.Pop[i][0] * k - x1) * w / (x2 - x1) - 1), (float)(h - (algst.Pop[i][1] * k - y1) * h / (y2 - y1) - 1), 2, 2);

                }
                if (Red[2] == true)
                {
                    for (int i = 0; i < algst.Pop.Count; i++)
                        for (int j = 0; j < algst.Kloni[i].Count; j++ )
                            e.Graphics.FillEllipse(Brushes.DarkCyan, (float)((algst.Kloni[i][j][0] * k - x1) * w / (x2 - x1) - 1), (float)(h - (algst.Kloni[i][j][1] * k - y1) * h / (y2 - y1) - 1), 2, 2);

                }

                ////e.Graphics.DrawLine(p10, (float)(x1 * w / (x1 - x2)), h - a-5, (float)(x1 * w / (x1 - x2)), h - a+5);
                ////e.Graphics.DrawLine(p10, a-5, (float)(h-y1 * h / (y1 - y2)), a+5, (float)(h-y1 * h / (y1 - y2)));

                for (int i = -6; i < 12; i++)
                {
                    e.Graphics.DrawLine(p10, (float)((x1 - i * step) * w / (x1 - x2)), h - a - 5, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                    e.Graphics.DrawLine(p10, a - 5, (float)(h - (y1 - i * step) * h / (y1 - y2)), a + 5, (float)(h - (y1 - i * step) * h / (y1 - y2)));
                    e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                    e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, a - 30, (float)(h - 5 - (y1 - i * step) * h / (y1 - y2)));
                }
            }
            e.Graphics.DrawLine(p10, 0, h - a, w, h - a);
            e.Graphics.DrawLine(p10, a, h, a, 0);
            e.Graphics.DrawLine(p10, a, 0, a - 5, 10);
            e.Graphics.DrawLine(p10, a, 0, a + 5, 10);
            e.Graphics.DrawLine(p10, w - 5, h - a, w - 15, h - a - 5);
            e.Graphics.DrawLine(p10, w - 5, h - a, w - 15, h - a + 5);
            e.Graphics.DrawString("x", font1, Brushes.Black, w - 20, h - a + 5);
            e.Graphics.DrawString("y", font1, Brushes.Black, a - 20, 1);

           
        }


        private float function(double x1, double x2, int f)
        {
            float funct = 0;
            if (f == 0)
            {
                funct = (float)(x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2))));
            }
            else if (f == 1)
            {
                funct = (float)(x1 * Math.Sin(4 * Math.PI * x1) - x2 * Math.Sin(4 * Math.PI * x2 + Math.PI) + 1);
            }
            else if (f == 2)
            {
                double[] c6 = Cpow(x1, x2, 6);
                funct = (float)(1 / (1 + Math.Sqrt((c6[0] - 1) * (c6[0] - 1) + c6[1] * c6[1])));
            }
            else if (f == 3)
            {
                funct = (float)(0.5 - (Math.Pow(Math.Sin(Math.Sqrt(x1 * x1 + x2 * x2)), 2) - 0.5) / (1 + 0.001 * (x1 * x1 + x2 * x2)));
            }
            else if (f == 4)
            {
                funct = (float)((-x1 * x1 + 10 * Math.Cos(2 * Math.PI * x1)) + (-x2 * x2 + 10 * Math.Cos(Math.PI * x2)));
            }
            else if (f == 5)
            {
                funct = (float)(-Math.E + 20 * Math.Exp(-0.2 * Math.Sqrt((x1 * x1 + x2 * x2) / 2)) + Math.Exp((Math.Cos(2 * Math.PI * x1) + Math.Cos(2 * Math.PI * x2)) / 2));
            }
            else if (f == 6)
            {
                funct = (float)(Math.Pow(Math.Cos(2 * x1 * x1) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x1) - 1.2, 2) - Math.Pow(Math.Cos(2 * x2 * x2) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x2) - 1.2, 2));
            }
            else if (f == 7)
            {
                funct = (float)(-Math.Sqrt(Math.Abs(Math.Sin(Math.Sin(Math.Sqrt(Math.Abs(Math.Sin(x1 - 1))) + Math.Sqrt(Math.Abs(Math.Sin(x2 + 2))))))) + 1);
            }
            return funct;
        }

        private double[] Cpow(double x, double y, int p)
        {
            double[] Cp = new double[2];
            Cp[0] = x; Cp[1] = y;
            double x0 = 0;
            double y0 = 0;
            for (int i = 1; i < p; i++)
            {
                x0 = Cp[0] * x - Cp[1] * y;
                y0 = Cp[1] * x + Cp[0] * y;
                Cp[0] = x0; Cp[1] = y0;
            }
            return Cp;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            float w = pictureBox2.Width;
            float h = pictureBox2.Height;
            Pen p1 = new Pen(Color.Black, 1);
            Pen p2 = new Pen(Color.Green, 2);
            Pen p3 = new Pen(Color.Blue, 2);
            Font f1 = new Font("TimesNewRoman", 7);
            Font f2 = new Font("TimesNewRoman", 7, FontStyle.Bold);
            float x0 = 25;
            float y0 = h - 20;
            e.Graphics.DrawLine(p1, x0, y0, w, y0);
            e.Graphics.DrawLine(p1, x0, y0, x0, 0);
            e.Graphics.DrawLine(p1, x0, 0, x0 - 5, 10);
            e.Graphics.DrawLine(p1, x0, 0, x0 + 5, 10);
            e.Graphics.DrawLine(p1, w - 5, y0, w - 15, y0 + 5);
            e.Graphics.DrawLine(p1, w - 5, y0, w - 15, y0 - 5);



            float mx = (w - 60) / (t + 5);//(algst.K);
                float mh = 0;
                try { mh = (float)((h - 60) / ((1.1 * exact - Math.Min(0, algst.AverF[0])))); }
                catch { mh = (float)((h - 60) / (1.1 * exact)); }

                double a = 1;
               /* if (algst.K < 31) a = 2;
                else if (algst.K < 101) a = 5;
                else if (algst.K < 151) a = 10;
                else if (algst.K < 301) a = 20;
                else if (algst.K < 501) a = 50;
                else if (algst.K < 1001) a = 100;
                else if (algst.K < 2001) a = 200;
                else a = 1000;*/
                if (t < 31) a = 2;
                else if (t < 101) a = 5;
                else if (t < 151) a = 10;
                else if (t < 301) a = 20;
                else if (t < 501) a = 50;
                else if (t < 1001) a = 100;
                else if (t < 2001) a = 200;
                else a = 1000;

                double b = 0;
                try { b = 1.1 * exact - Math.Min(0, algst.AverF[0]); }
                catch { b = 1.1 * exact; }
                double c = 1;
                if (b < 0.1) c = 0.01;
                else if (b < 0.2) c = 0.02;
                else if (b < 1) c = 0.1;
                else if (b < 2) c = 0.2;
                else if (b < 11) c = 1;
                else if (b < 21) c = 2;
                else if (b < 51) c = 5;
                else if (b < 101) c = 10;
                else if (b < 200) c = 20;
                else if (b < 1000) c = 100;
                else if (b < 2000) c = 200;
                else c = 500;
               
                    for (int i = 0; i < algst.K; i++)
                    {
                        
                        //float s = i / a;
                        if (Math.Floor((decimal)(i / a)) - (decimal)(i / a) == 0)
                        {
                            e.Graphics.DrawLine(p1, (float)(x0 + (mx) * (i)), y0 + 2, (float)(x0 + mx * (i)), y0 - 2);
                            e.Graphics.DrawString(Convert.ToString(i), f1, Brushes.Black, (float)(x0 + mx * (i)), (float)(y0 + 4));

                        }
                    }
                    if (Math.Floor((decimal)((algst.K) / a)) - (decimal)((algst.K) / a) == 0)
                    {
                        e.Graphics.DrawLine(p1, (float)(x0 + (mx) * (algst.K)), y0 + 2, (float)(x0 + mx * (algst.K)), y0 - 2);
                        e.Graphics.DrawString(Convert.ToString(algst.K), f1, Brushes.Black, (float)(x0 + mx * (algst.K)), (float)(y0 + 4));
                    }

                    if (flag == true)
                    {
                        e.Graphics.FillEllipse(Brushes.Green, (float)(x0), (float)(y0 - 1 - mh * (algst.AverF[0] - Math.Min(0, algst.AverF[0]))), 3, 3);
                        e.Graphics.FillEllipse(Brushes.Blue, (float)(x0), (float)(y0 - 1 - mh * (algst.BestF[0] - Math.Min(0, algst.AverF[0]))), 3,3);
                      
                        for (int i = 0; i < algst.AverF.Count-1; i++)
                        {

                            {
                                e.Graphics.DrawLine(p2, (float)(x0 + mx * i), (float)(y0 - mh * (algst.AverF[i] - Math.Min(0, algst.AverF[0]))), (float)(x0 + mx * (i + 1)), (float)(y0 - mh * (algst.AverF[i + 1] - Math.Min(0, algst.AverF[0]))));
                                e.Graphics.DrawLine(p3, (float)(x0 + mx * i), (float)(y0 - mh * (algst.BestF[i] - Math.Min(0, algst.AverF[0]))), (float)(x0 + mx * (i + 1)), (float)(y0 - mh * (algst.BestF[i + 1] - Math.Min(0, algst.AverF[0]))));
                            }
                        }
                    }

                    float zero = 0;
                    try { zero = (float)(y0 + mh * Math.Min(0, algst.AverF[0])); }
                    catch { zero = (float)(y0); }

                for (int i = -6; i < 12; i++)
                {
                    e.Graphics.DrawLine(p1, (float)(x0 + 2), (float)(zero - mh * c * i), (float)(x0 - 2), (float)(zero - mh * c * i));
                    if ((zero - mh * c * i - 8 > 11) && (zero - mh * c * i - 8 < h-20)) e.Graphics.DrawString(Convert.ToString((c * i)), f1, Brushes.Black, (float)(x0 - 24), (float)(zero - mh * c * i - 8));
                }
                e.Graphics.DrawString("k", f2, Brushes.Black, (float)(w - 15), (float)(y0 + 4));
                e.Graphics.DrawString("f", f2, Brushes.Black, (float)(x0 - 24), (float)(2));
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //клонирование
            if ((Red[0] == true) || (Red[5] == true) || (Red[9] == true))
            {
                Red[0] = false;
                Red[5] = false;
                Red[9] = false;
                Red[1] = true;
                algst.Normalization();
                algst.Klonirovanie();
                pictureBox3.Refresh();
                pictureBox1.Refresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //мутация
            if (Red[1] == true)
            {
                Red[1] = false;
                Red[2] = true;
                algst.Mutation();
                pictureBox3.Refresh();
                pictureBox1.Refresh();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //селекция
            if (Red[2] == true)
            {
                Red[2] = false;
                Red[3] = true;
                algst.Selection();
                t++;
                dataGridView1.Rows[0].Cells[1].Value = t;
                dataGridView1.Rows[1].Cells[1].Value = algst.Pop.Count; 
                dataGridView1.Rows[2].Cells[1].Value = m;
                dataGridView1.Rows[3].Cells[1].Value = Math.Round(algst.AverF[t], 6);
                dataGridView1.Rows[4].Cells[1].Value = Math.Round(algst.AverF[t] - algst.AverF[t-1], 6);
                dataGridView1.Rows[5].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 4).ToString() + "; " + Math.Round(algst.Pop[0][1], 4).ToString() + ")";
                dataGridView1.Rows[6].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
                dataGridView1.Rows[7].Cells[1].Value = exact.ToString();
                dataGridView1.Rows[8].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
                    
                pictureBox3.Refresh();
                pictureBox1.Refresh();
                pictureBox2.Refresh();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //условия окончания локального поиска
            if (Red[3] == true)
            {
                
                if (algst.LockProw(t)==false)
                { Red[5] = true; }//нет
                else if (algst.LockProw(t) == true)
                { Red[4] = true; algst.LocalS++; }//да
                Red[3] = false;
                pictureBox3.Refresh();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //условия глобального поиска
            if (Red[6] == true)
            {
                
                if (algst.GlobalProw(t)==false)
                { Red[7] = true; }//нет
                else if (algst.GlobalProw(t)==true)
                { Red[8] = true; }//да
                Red[6] = false;
                pictureBox3.Refresh();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //добавление
            if (Red[7] == true)
            {
                Red[7] = false;
                Red[9] = true;
                algst.Dobavlenie();
                //t++;
                dataGridView1.Rows[0].Cells[1].Value = t;
                dataGridView1.Rows[1].Cells[1].Value = algst.Pop.Count;
                dataGridView1.Rows[2].Cells[1].Value = m;
                dataGridView1.Rows[3].Cells[1].Value = Math.Round(algst.AverF[t], 6);
                dataGridView1.Rows[4].Cells[1].Value = Math.Round(algst.AverF[t] - algst.AverF[t - 1], 6);
                dataGridView1.Rows[5].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 4).ToString() + "; " + Math.Round(algst.Pop[0][1], 4).ToString() + ")";
                dataGridView1.Rows[6].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
                dataGridView1.Rows[7].Cells[1].Value = exact.ToString();
                dataGridView1.Rows[8].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
                    
                pictureBox3.Refresh();
                pictureBox1.Refresh();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //ответ
            if (Red[8] == true)
            {
                Red[8] = false;
                dataGridView2.Rows[0].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 4).ToString() + "; " + Math.Round(algst.Pop[0][1], 4).ToString() + ")";
                dataGridView2.Rows[1].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
                dataGridView2.Rows[2].Cells[1].Value = exact.ToString();
                dataGridView2.Rows[3].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
                pictureBox3.Refresh();
                algst.kpop = t;
                if (endreport == false)
                {
                    endreport = true;
                    if ((File.Exists("protocol.dt")))
                    {
                        Report prot = new Report();
                        prot.Prot2(algst.Pop, algst.kpop, algst.LocalS, exact);

                        FileStream fs = new FileStream("protocol.dt", FileMode.Append, FileAccess.Write);
                        StreamWriter r1 = new StreamWriter(fs);
                        r1.Write(prot.toprotocol);
                        r1.Close();
                        fs.Close();

                    }
                }

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //новая популяция
            if ((Red[0] == true) || (Red[5] == true) || (Red[9] == true))
            {
                if (t < K)
                {
                    algst.Normalization();
                    algst.Klonirovanie();
                    algst.Mutation();
                    algst.Selection();
                    t++;
                    if (algst.LockProw(t) == true)
                    {
                        algst.Sokrashenie();
                        m = algst.Pop.Count;
                        algst.LocalS++;
                        if (algst.GlobalProw(t) == false)
                        {
                            algst.Dobavlenie();
                            Red[0] = false;
                            Red[5] = false;
                            Red[9] = true;
                        }
                        else if (algst.GlobalProw(t) == true)
                        {
                            Red[0] = false;
                            Red[5] = false;
                            Red[9] = false;
                            Red[8] = true;
                            algst.Sokrashenie();
                            dataGridView2.Rows[0].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 4).ToString() + "; " + Math.Round(algst.Pop[0][1], 4).ToString() + ")";
                            dataGridView2.Rows[1].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
                            dataGridView2.Rows[2].Cells[1].Value = exact.ToString();
                            dataGridView2.Rows[3].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
                            m = algst.Pop.Count;
                        }
                    }
                    else
                    {
                        Red[0] = false;
                        Red[5] = true;
                        Red[9] = false;
                    }
                }
                dataGridView1.Rows[0].Cells[1].Value = t;
                dataGridView1.Rows[1].Cells[1].Value = algst.Pop.Count;
                dataGridView1.Rows[2].Cells[1].Value = m;
                dataGridView1.Rows[3].Cells[1].Value = Math.Round(algst.AverF[t], 6);
                dataGridView1.Rows[4].Cells[1].Value = Math.Round(algst.AverF[t] - algst.AverF[t - 1], 6);
                dataGridView1.Rows[5].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 6).ToString() + "; " + Math.Round(algst.Pop[0][1], 6).ToString() + ")";
                dataGridView1.Rows[6].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
                dataGridView1.Rows[7].Cells[1].Value = exact.ToString();
                dataGridView1.Rows[8].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
                    
                pictureBox1.Refresh();
                pictureBox2.Refresh();
                pictureBox3.Refresh();
                dataGridView1.Refresh();
            }
            else if (Red[8] == false)
                MessageBox.Show("Получение следующий популяции невозможно! \r\nФормирование новой популяции начинается с шага \"Клонирование\".");
            else if (Red[8] == true)
            {
                MessageBox.Show("Ответ получен.");
                Red[8] = false;
                pictureBox3.Refresh();
                algst.kpop = t;
                if (endreport == false)
                {
                    endreport = true;
                    if ((File.Exists("protocol.dt")))
                    {
                        Report prot = new Report();
                        prot.Prot2(algst.Pop, algst.kpop, algst.LocalS, exact);

                        FileStream fs = new FileStream("protocol.dt", FileMode.Append, FileAccess.Write);
                        StreamWriter r1 = new StreamWriter(fs);
                        r1.Write(prot.toprotocol);
                        r1.Close();
                        fs.Close();

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((Red[0] == true) || (Red[5] == true) || (Red[9] == true))
            {
                Red[0] = false;
                Red[5] = false;
                Red[9] = false;
                pictureBox3.Refresh();

                for (int i = t; i < K; i++)
                {
                    algst.Normalization();
                    algst.Klonirovanie();
                    algst.Mutation();
                    algst.Selection();
                    t++;
                    if (algst.LockProw(t) == true)
                    {
                        algst.Sokrashenie();
                        m = algst.Pop.Count;
                        algst.LocalS++;
                        if (algst.GlobalProw(t) == false)
                        {
                            algst.Dobavlenie();
                        }
                        else if (algst.GlobalProw(t) == true)
                        {           
                            algst.Sokrashenie();
                            dataGridView2.Rows[0].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 4).ToString() + "; " + Math.Round(algst.Pop[0][1], 4).ToString() + ")";
                            dataGridView2.Rows[1].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
                            dataGridView2.Rows[2].Cells[1].Value = exact.ToString();
                            dataGridView2.Rows[3].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
                            m = algst.Pop.Count;
                            dataGridView1.Rows[0].Cells[1].Value = t;
                            dataGridView1.Rows[1].Cells[1].Value = algst.Pop.Count;
                            dataGridView1.Rows[2].Cells[1].Value = m;
                            dataGridView1.Rows[3].Cells[1].Value = Math.Round(algst.AverF[t], 6);
                            dataGridView1.Rows[4].Cells[1].Value = Math.Round(algst.AverF[t] - algst.AverF[t - 1], 6);
                            dataGridView1.Rows[5].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 4).ToString() + "; " + Math.Round(algst.Pop[0][1], 4).ToString() + ")";
                            dataGridView1.Rows[6].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
                            dataGridView1.Rows[7].Cells[1].Value = exact.ToString();
                            dataGridView1.Rows[8].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
                    
                            dataGridView1.Refresh();
                            pictureBox1.Refresh();
                            pictureBox2.Refresh();
                            algst.kpop = t;
                            break;
                        }
                    }

                    dataGridView1.Rows[0].Cells[1].Value = t;
                    dataGridView1.Rows[1].Cells[1].Value = algst.Pop.Count;
                    dataGridView1.Rows[2].Cells[1].Value = m;
                    dataGridView1.Rows[3].Cells[1].Value = Math.Round(algst.AverF[t], 6);
                    dataGridView1.Rows[4].Cells[1].Value = Math.Round(algst.AverF[t] - algst.AverF[t - 1], 6);
                    dataGridView1.Rows[5].Cells[1].Value = "(" + Math.Round(algst.Pop[0][0], 4).ToString() + "; " + Math.Round(algst.Pop[0][1], 4).ToString() + ")";
                    dataGridView1.Rows[6].Cells[1].Value = Math.Round(algst.Pop[0][2], 6).ToString();
                    dataGridView1.Rows[7].Cells[1].Value = exact.ToString();
                    dataGridView1.Rows[8].Cells[1].Value = Math.Round(Math.Abs(exact - algst.Pop[0][2]), 6).ToString();
                    
                    dataGridView1.Refresh();
                    pictureBox1.Refresh();
                    pictureBox2.Refresh();


                }
                if (endreport == false)
                {
                    endreport = true;
                    if ((File.Exists("protocol.dt")))
                    {
                        Report prot = new Report();
                        prot.Prot2(algst.Pop, algst.kpop, algst.LocalS, exact);

                        FileStream fs = new FileStream("protocol.dt", FileMode.Append, FileAccess.Write);
                        StreamWriter r1 = new StreamWriter(fs);
                        r1.Write(prot.toprotocol);
                        r1.Close();
                        fs.Close();

                    }
                }
            }
            else if (Red[8] == false)
                MessageBox.Show("Получить ответ невозможно! \r\nПерейдите к шагу \"Клонирование\".");
            else if (Red[8] == true)
            {
                MessageBox.Show("Ответ получен.");
                Red[8] = false;
                pictureBox3.Refresh();
            }
        }

       
        Form5 forma5 = new Form5();  

        private void button15_Click(object sender, EventArgs e)
        {
            if (forma5.IsDisposed) forma5 = new Form5();
            forma5.Population = algst.Pop;
            forma5.FillList();
            forma5.FillData(Ip, K, klon, parkl, g, eps, sigma, percent);
            forma5.Show();
        }

        Form3 forma3 = new Form3();   

        private void button3_Click(object sender, EventArgs e)
        {
            if (forma3.IsDisposed) forma3 = new Form3();
            forma3.A = A;
            forma3.Ar = Ar;
            forma3.flines = flines;
            forma3.obl = showobl;
            forma3.oblbase = showoblbase;
            forma3.Show();
        }

        private void button3_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Здесь будет открываться файл со справкой");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Process.Start("protocol.dt");
        }


    }

    

}
