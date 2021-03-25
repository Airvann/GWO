using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
         
        }

        public List<double> averageF;
        public List<double> bestF;
        public List<double[]> Population;


        public void FillList()
        {
            for (int i = 0; i < Population.Count; i++)
            {
                ListViewItem Item = new ListViewItem();
                Item.SubItems.Add((i + 1).ToString());
                Item.SubItems.Add(Math.Round(Population[i][0],4).ToString());
                Item.SubItems.Add(Math.Round(Population[i][1],4).ToString());
                Item.SubItems.Add(Math.Round(Population[i][2],4).ToString());
                listView1.Items.Add(Item);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            float w = pictureBox1.Width;
            float h = pictureBox1.Height;
            Pen p1 = new Pen(Color.Black, 1);
            Pen p2 = new Pen(Color.Green, 2);
            Pen p3 = new Pen(Color.Blue, 2);
            Font f1 = new Font("TimesNewRoman",8);
            Font f2 = new Font("TimesNewRoman", 8, FontStyle.Bold);
            float x0 = 30;
            float y0 = h-30;
            e.Graphics.DrawLine(p1, x0, y0, w, y0);
            e.Graphics.DrawLine(p1, x0, y0, x0, 0);
            e.Graphics.DrawLine(p1, x0, 0, x0-5, 10);
            e.Graphics.DrawLine(p1, x0, 0, x0+5, 10);
            e.Graphics.DrawLine(p1, w - 5, y0, w - 15, y0+5);
            e.Graphics.DrawLine(p1, w - 5, y0, w - 15, y0-5);

            float mx = (w - 60) / (bestF.Count);
            float mh = (float)((h - 60) / ((1.1*Population[0][2]-Math.Min(0,averageF[0]))));

            double a = 1;
            if (bestF.Count < 31) a = 2;
            else if (bestF.Count < 101) a = 5;
            else if (bestF.Count < 151) a = 10;
            else if (bestF.Count < 301) a = 20;
            else if (bestF.Count < 501) a = 50;
            else if (bestF.Count < 1001) a = 100;
            else if (bestF.Count < 2001) a = 200;
            else a = 1000;

            double b = 1.1*Population[0][2]-Math.Min(0,averageF[0]);
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

            for (int i = 0; i < averageF.Count - 1; i++)
            {
                e.Graphics.DrawLine(p2, (float)(x0 + mx * i), (float)(y0 - mh * (averageF[i] - Math.Min(0, averageF[0]))), (float)(x0 + mx * (i + 1)), (float)(y0 - mh * (averageF[i + 1]-Math.Min(0, averageF[0]))));
                e.Graphics.DrawLine(p3, (float)(x0 + mx * i), (float)(y0 - mh * (bestF[i]-Math.Min(0, averageF[0]))), (float)(x0 + mx * (i + 1)), (float)(y0 - mh * (bestF[i + 1]-Math.Min(0, averageF[0]))));
                //float s = i / a;
                if (Math.Floor((decimal)(i / a)) - (decimal)(i / a) == 0)
                {
                    e.Graphics.DrawLine(p1, (float)(x0 + (mx) * (i)), y0 + 2, (float)(x0 + mx * (i)), y0 - 2);
                    e.Graphics.DrawString(Convert.ToString(i),f1,Brushes.Black,(float)(x0 + mx * (i)),(float)(y0+4));
                    
                }
            }
            if (Math.Floor((decimal)((averageF.Count - 1) / a)) - (decimal)((averageF.Count - 1) / a) == 0)
           {
                e.Graphics.DrawLine(p1, (float)(x0 + (mx) * (averageF.Count - 1)), y0 + 2, (float)(x0 + mx * (averageF.Count - 1)), y0 - 2);
                e.Graphics.DrawString(Convert.ToString(averageF.Count - 1), f1, Brushes.Black, (float)(x0 + mx * (averageF.Count - 1)), (float)(y0 +4));
            }

            float zero = (float)(y0 + mh*Math.Min(0, averageF[0]));

            for (int i = -6; i < 12; i++)
            {
                e.Graphics.DrawLine(p1, (float)(x0 + 2), (float)(zero-mh*c*i), (float)(x0 - 2), (float)(zero-mh*c*i));
                e.Graphics.DrawString(Convert.ToString((c * i )), f1, Brushes.Black, (float)(x0 - 29), (float)(zero-mh*c*i - 8));
            }
            e.Graphics.DrawString("k", f2, Brushes.Black, (float)(w-15), (float)(y0 + 4));
            e.Graphics.DrawString("f", f2, Brushes.Black, (float)(x0 - 29), (float)(2));

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
