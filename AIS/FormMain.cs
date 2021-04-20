using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace GWO
{
    public partial class FormMain : Form
    {
        private Algoritm alg;
        private int population = 0;
        private int MaxIteration = 0;
        private double[,] obl = new double[2, 2];
        private List<Vector> exactPoints;

        private bool[] flines = new bool[8];
        private float k = 1;
        private float[] Ar = new float[8];
        private double[,] showobl = new double[2, 2];
        private bool flag = false;
        private bool flag2 = false;
        private double exact = 0;

        public FormMain()
        {
            InitializeComponent();
            comboBoxSelectParams.SelectedIndex = 0;
            InitDataGridView();

            comboBoxSelectParams.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);

            if (File.Exists("protocol.txt"))
                File.Delete("protocol.txt");

            FileStream fs = new FileStream("protocol.txt", FileMode.Append, FileAccess.Write);

            StreamWriter r = new StreamWriter(fs);
            r.Write($"+-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+\n" +
                    $"| Номер функции | Размер популяции | Количество итераций |        Cреднее значение отклонения     |  Наименьшее значение отклонения  |Среднеквадратическое отклонение | Количество успехов  |\n" +
                    $"|               |                  |                     |            от точного решения          |                                  |                                |                     |\n" +
                    $"+---------------+------------------+---------------------+----------------------------------------+----------------------------------+--------------------------------+---------------------+\n");
            r.Close();
            fs.Close();
        }

        private void InitDataGridView()
        {
            dataGridView1.RowCount = 2;
            dataGridView1.Columns[0].DefaultCellStyle.Font = new Font("Times new roman", 12, FontStyle.Italic);
            dataGridView1.Rows[0].Cells[0].Value = "x";
            dataGridView1.Rows[1].Cells[0].Value = "y";

            dataGridView2.RowCount = 2;
            dataGridView2.Rows[0].Cells[0].Value = "Размер начальной популяции";
            dataGridView2.Rows[1].Cells[0].Value = "Максимальное количество итераций";
            dataGridView2.Rows[0].Cells[1].Value = 100;
            dataGridView2.Rows[1].Cells[1].Value = 100;

            dataGridView3.RowCount = 3;
            dataGridView3.Columns[0].DefaultCellStyle.Font = new Font("Times new roman", 12, FontStyle.Italic);
            dataGridView3.Rows[0].Cells[0].Value = "x";
            dataGridView3.Rows[1].Cells[0].Value = "y";
            dataGridView3.Rows[2].Cells[0].Value = "f*";            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( dataGridView1.Rows[0].Cells[1].Value != null  &&
                dataGridView1.Rows[0].Cells[2].Value != null  &&
                dataGridView1.Rows[1].Cells[1].Value != null  &&
                dataGridView1.Rows[1].Cells[2].Value!= null)
            {
                //создать начальную популяцию
                if ((comboBox1.SelectedIndex != -1) && (comboBoxSelectParams.SelectedIndex != -1))
                {
                    int z = comboBox1.SelectedIndex;

                    obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                    obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
                    obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
                    obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

                    population = Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value);
                    MaxIteration = Convert.ToInt32(dataGridView2.Rows[1].Cells[1].Value);
                    Params param = (comboBoxSelectParams.SelectedIndex == 0) ? Params.Linear : Params.Quadratic;
                    alg = new Algoritm();

                    Wolf result = alg.FastStartAlg(population, MaxIteration, obl, z, param);
                    dataGridView3.Rows[0].Cells[1].Value = string.Format($"{result.coords[0]:F8}");
                    dataGridView3.Rows[1].Cells[1].Value = string.Format($"{result.coords[1]:F8}");
                    dataGridView3.Rows[2].Cells[1].Value = string.Format($"{result.fitness:F8}");
                    flag2 = true;
                    pictureBox1.Refresh();
                }
            }
            else
                MessageBox.Show("Введите корректные параметры", "Ошибка при запуске алгоритма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            exactPoints = new List<Vector>();
            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-500";
                dataGridView1.Rows[0].Cells[2].Value = "500";
                dataGridView1.Rows[1].Cells[1].Value = "-500";
                dataGridView1.Rows[1].Cells[2].Value = "500";
                exact = 837.9658;
                exactPoints.Add(new Vector(420.9687, 420.9687));

                Ar[0] = -200;
                Ar[1] = -1;
                Ar[2] = 300;
                Ar[3] = 600;
                Ar[4] = 800;
                flag = true;
                pictureBox2.Image = Properties.Resources.Швефель;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-2";
                dataGridView1.Rows[0].Cells[2].Value = "2";
                dataGridView1.Rows[1].Cells[1].Value = "-2";
                dataGridView1.Rows[1].Cells[2].Value = "2";
                exact = 4.253888;
                exactPoints.Add(new Vector(-1.6288, -1.6288));
                exactPoints.Add(new Vector(1.6288, 1.6288));
                exactPoints.Add(new Vector(-1.6288, 1.6288));
                exactPoints.Add(new Vector(1.6288, -1.6288));

                Ar[0] = 0;
                Ar[1] = 1;
                Ar[2] = 2;
                Ar[3] = 3;
                Ar[4] = 4;
                flag = true;
                pictureBox2.Image = Properties.Resources.мульти;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-2";
                dataGridView1.Rows[0].Cells[2].Value = "2";
                dataGridView1.Rows[1].Cells[1].Value = "-2";
                dataGridView1.Rows[1].Cells[2].Value = "2";
                exact = 1;
                exactPoints.Add(new Vector(0.5, -0.866));
                exactPoints.Add(new Vector(-0.5, 0.866));
                exactPoints.Add(new Vector(0.5, 0.866));
                exactPoints.Add(new Vector(-0.5, -0.866));
                exactPoints.Add(new Vector(1, 0));
                exactPoints.Add(new Vector(-1, 0));

                Ar[0] = 0.2F;
                Ar[1] = 0.45F;
                Ar[2] = 0.499999F;
                Ar[3] = 0.6F;
                Ar[4] = 0.9F;
                flag = true;
                pictureBox2.Image = Properties.Resources.рут;
            } 
            else if (comboBox1.SelectedIndex == 3)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-10";
                dataGridView1.Rows[0].Cells[2].Value = "10";
                dataGridView1.Rows[1].Cells[1].Value = "-10";
                dataGridView1.Rows[1].Cells[2].Value = "10";
                exact = 1;
                exactPoints.Add(new Vector(0, 0));

                Ar[0] = 0.2F;
                Ar[1] = 0.4F;
                Ar[2] = 0.6F;
                Ar[3] = 0.8F;
                Ar[4] = 0.99F;
                flag = true;
                pictureBox2.Image = Properties.Resources.Шафер;
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 20;
                exactPoints.Add(new Vector(0, 0));

                Ar[0] = -20F;
                Ar[1] = -10F;
                Ar[2] = 0F;
                Ar[3] = 10F;
                Ar[4] = 19F;
                flag = true;
                pictureBox2.Image = Properties.Resources.Растригин;
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-10";
                dataGridView1.Rows[0].Cells[2].Value = "10";
                dataGridView1.Rows[1].Cells[1].Value = "-10";
                dataGridView1.Rows[1].Cells[2].Value = "10";
                exact = 20;
                exactPoints.Add(new Vector(0, 0));

                Ar[0] = 4F;
                Ar[1] = 7F;
                Ar[2] = 10F;
                Ar[3] = 14F;
                Ar[4] = 19F;
                flag = true;
                pictureBox2.Image = Properties.Resources.Эклея;
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 14.060606;
                exactPoints.Add(new Vector(-3.3157, -3.0725));

                Ar[0] = 2F;
                Ar[1] = 8F;
                Ar[2] = 10F;
                Ar[3] = 12F;
                Ar[4] = 14F;
                flag = true;
                pictureBox2.Image = Properties.Resources.Skin;
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 1;
                exactPoints.Add(new Vector(1, -2));

                Ar[0] = 0.1F;
                Ar[1] = 0.15F;
                Ar[2] = 0.2F;
                Ar[3] = 0.3F;
                Ar[4] = 0.5F;
                flag = true;
                pictureBox2.Image = Properties.Resources.Trapfall;
            }
            else if (comboBox1.SelectedIndex == 8)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-3";
                dataGridView1.Rows[0].Cells[2].Value = "3";
                dataGridView1.Rows[1].Cells[1].Value = "-1";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 0;
                exactPoints.Add(new Vector(1, 1));

                Ar[0] = -350F;
                Ar[1] = -180F;
                Ar[2] = -30F;
                Ar[3] = -4F;
                Ar[4] = -0.5F;
                flag = true;
                pictureBox2.Image = Properties.Resources.Розенброк;
            }
            else if (comboBox1.SelectedIndex == 9)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 0;
                exactPoints.Add(new Vector(0, 0));

                Ar[0] = -7F;
                Ar[1] = -4F;
                Ar[2] = -2F;
                Ar[3] = -0.8F;
                Ar[4] = -0.1F;
                flag = true;
                pictureBox2.Image = Properties.Resources.параболическая;
            }

            Ar[5] = 0;
            Ar[6] = 0;
            Ar[7] = 0;
            for (int i = 0; i < 5; i++)
                flines[i] = true;
            flines[5] = false;
            flines[6] = false;
            flines[7] = false;

            flag2 = false;

            showobl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
            showobl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
            showobl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
            showobl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

            pictureBox1.Refresh();
            dataGridView1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            float w = pictureBox1.Width;
            float h = pictureBox1.Height;
            float x0 = w/2;
            float y0 = h/2;
            float a = 30;
            
            Pen p10 = new Pen(Color.Black, 1);

            Font font1 = new Font("TimesNewRoman", 10, FontStyle.Bold);
            Font font2 = new Font("TimesNewRoman", 8);
            
            pictureBox1.BackColor = Color.White;
            if (flag)
            {
                double x1 = showobl[0, 0];
                double x2 = showobl[0, 1];
                double y1 = showobl[1, 0];
                double y2 = showobl[1, 1];

                int z = comboBox1.SelectedIndex;
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
                double dxy = dx-dy;

                double bxy = Math.Max(dx, dy);
                double step;
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

                if (dxy>0)
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
                        for (int ii = 0; ii < w; ii++)
                            for (int jj = 0; jj < h; jj++)
                            {
                                double i = (ii * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                                double j = (jj * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                                double i1 = ((ii + 1) * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                                double j1 = ((jj + 1) * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                                double i0 = ((ii - 1) * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                                double j0 = ((jj - 1) * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                                double f = function(i, j, z);
                                double f2 = function(i0, j, z); 
                                double f3 = function(i, j0, z); 
                                double f4 = function(i1, j, z); 
                                double f5 = function(i, j1, z); 
                                double f6 = function(i1, j1, z);
                                double f7 = function(i0, j1, z);
                                double f8 = function(i1, j0, z);
                                double f9 = function(i0, j0, z);

                                if (((f2 < a1) || (f3 < a1) || (f4 < a1) || (f5 < a1) || (f6 < a1) || (f7 < a1) || (f8 < a1) || (f9 < a1)) && (f > a1)&&(flines[4]==true)) e.Graphics.FillRectangle(Brushes.PaleGreen, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a3) || (f3 < a3) || (f4 < a3) || (f5 < a3) || (f6 < a3) || (f7 < a3) || (f8 < a3) || (f9 < a3)) && (f > a3)&&(flines[3]==true)) e.Graphics.FillRectangle(Brushes.YellowGreen, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a5) || (f3 < a5) || (f4 < a5) || (f5 < a5) || (f6 < a5) || (f7 < a5) || (f8 < a5) || (f9 < a5)) && (f > a5)&&(flines[2]==true)) e.Graphics.FillRectangle(Brushes.Orange, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a7) || (f3 < a7) || (f4 < a7) || (f5 < a7) || (f6 < a7) || (f7 < a7) || (f8 < a7) || (f9 < a7)) && (f > a7)&&(flines[1]==true)) e.Graphics.FillRectangle(Brushes.Red, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a9) || (f3 < a9) || (f4 < a9) || (f5 < a9) || (f6 < a9) || (f7 < a9) || (f8 < a9) || (f9 < a9)) && (f > a9)&&(flines[0]==true)) e.Graphics.FillRectangle(Brushes.Maroon, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a10) || (f3 < a10) || (f4 < a10) || (f5 < a10) || (f6 < a10) || (f7 < a10) || (f8 < a10) || (f9 < a10)) && (f > a10) && (flines[5] == true)) e.Graphics.FillRectangle(Brushes.Pink, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a11) || (f3 < a11) || (f4 < a11) || (f5 < a11) || (f6 < a11) || (f7 < a11) || (f8 < a11) || (f9 < a11)) && (f > a11) && (flines[6] == true)) e.Graphics.FillRectangle(Brushes.Violet, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a12) || (f3 < a12) || (f4 < a12) || (f5 < a12) || (f6 < a12) || (f7 < a12) || (f8 < a12) || (f9 < a12)) && (f > a12) && (flines[7] == true)) e.Graphics.FillRectangle(Brushes.MediumOrchid, (float)(ii), (float)(h - jj), 1, 1);

                            }

                        //Отрисовка результата работы алгоритма
                        if (flag2 == true)
                        {
                            for (int i = 0; i < (int)alg.population; i++)
                                e.Graphics.FillEllipse(Brushes.Blue, (float)((alg.individuals[i].coords.vector[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (alg.individuals[i].coords.vector[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);                            
                    
                            e.Graphics.FillEllipse(Brushes.Red, (float)((alg.alfa.coords.vector[0] * k - x1) * w / (x2 - x1) - 4), (float)(h - (alg.alfa.coords.vector[1] * k - y1) * h / (y2 - y1) - 4), 8, 8);
                        }                        

                        //отрисовка Осей
                        for (int i = -6; i < 12; i++)
                        {
                            e.Graphics.DrawLine(p10, (float)((x1 - i*step) * w / (x1 - x2)), h - a - 5, (float)((x1 - i*step) * w / (x1 - x2)), h - a + 5);
                            e.Graphics.DrawLine(p10, a - 5, (float)(h - (y1 - i*step) * h / (y1 - y2)), a + 5, (float)(h - (y1 - i*step) * h / (y1 - y2)));
                            e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                            e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, a - 30, (float)(h -5- (y1 - i * step) * h / (y1 - y2)));
                        }
            }
            
            //Стрелки абцисс и ординат
            p10.EndCap = LineCap.ArrowAnchor;
            e.Graphics.DrawLine(p10, 0, h - a, w - 10, h - a);
            e.Graphics.DrawLine(p10, a, h, a, 0);
            e.Graphics.DrawString("x", font1, Brushes.Black, w - 20, h - a + 5);
            e.Graphics.DrawString("y", font1, Brushes.Black, a - 20, 1);
        }
       
        //Все тествоые функции
        private float function(double x1, double x2, int f)
        { 
            float funct = 0;
            if (f == 0)
                funct = (float)(x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2))));
            else if (f == 1)
                funct = (float)(x1 * Math.Sin(4 * Math.PI * x1) - x2 * Math.Sin(4 * Math.PI * x2 + Math.PI) + 1);
            else if (f == 2)
            {
                double[] c6 = Cpow(x1, x2, 6);
                funct = (float)(1 / (1 + Math.Sqrt((c6[0] - 1) * (c6[0] - 1) + c6[1] * c6[1])));
            }
            else if (f == 3)
                funct = (float)(0.5 - (Math.Pow(Math.Sin(Math.Sqrt(x1 * x1 + x2 * x2)), 2) - 0.5) / (1 + 0.001 * (x1 * x1 + x2 * x2)));
            else if (f == 4)
                funct = (float)((-x1 * x1 + 10 * Math.Cos(2 * Math.PI * x1)) + (-x2 * x2 + 10 * Math.Cos(2 * Math.PI * x2)));
            else if (f == 5)
                funct = (float)(-Math.E + 20 * Math.Exp(-0.2 * Math.Sqrt((x1 * x1 + x2 * x2) / 2)) + Math.Exp((Math.Cos(2 * Math.PI * x1) + Math.Cos(2 * Math.PI * x2)) / 2));
            else if (f == 6)
                funct = (float)(Math.Pow(Math.Cos(2 * x1 * x1) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x1) - 1.2, 2) - Math.Pow(Math.Cos(2 * x2 * x2) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x2) - 1.2, 2));
            else if (f == 7)
                funct = (float)(-Math.Sqrt(Math.Abs(Math.Sin(Math.Sin(Math.Sqrt(Math.Abs(Math.Sin(x1 - 1))) + Math.Sqrt(Math.Abs(Math.Sin(x2 + 2))))))) + 1);
            else if (f == 8)
                funct = (float)(-(1 - x1) * (1 - x1) - 100 * (x2 - x1 * x1) * (x2 - x1 * x1));
            else if (f == 9)
                funct = (float)(-x1 * x1 - x2 * x2);
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Refresh();
        }

        //По шагам
        private void button3_Click(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedIndex != -1) && (comboBoxSelectParams.SelectedIndex != -1))
            {
                obl = new double[2, 2];

                obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
                obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
                obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

                population = Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value);
                MaxIteration = Convert.ToInt32(dataGridView2.Rows[1].Cells[1].Value);

                FormStepByStep form = new FormStepByStep(comboBox1.SelectedIndex, obl, population, MaxIteration, exact)
                {
                    flines = flines,
                    showobl = showobl,
                    Ar = Ar
                };
                form.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e) 
        {
            if (comboBox1.SelectedIndex != -1)
            {
                buttonAnswer.Enabled = true;
                buttonStepByStep.Enabled = true;
                buttonAnalysis.Enabled = true;
            }
        }

        private void comboBoxSelectParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxSelectParams.SelectedIndex)
            {
                case 0:
                    pictureBox4.Image = Properties.Resources.NonSquare;
                    break;
                case 1:
                    pictureBox4.Image = Properties.Resources.Square;
                    break;
                default:
                    break;
            }
            pictureBox4.Refresh();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Process.Start("HelpFile.pdf");
        }

        private void buttonAnalysis_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[0].Cells[1].Value != null &&
                dataGridView1.Rows[0].Cells[2].Value != null &&
                dataGridView1.Rows[1].Cells[1].Value != null &&
                dataGridView1.Rows[1].Cells[2].Value != null)
            {
                if ((comboBox1.SelectedIndex != -1) && (comboBoxSelectParams.SelectedIndex != -1))
                {
                    obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                    obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
                    obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
                    obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

                    List<double> averFuncDeviation = new List<double>();
                    double minDeviation = 0;
                    int successCount = 0;
                    double eps = Math.Max(Math.Abs(obl[0, 0] - obl[0, 1]), Math.Abs(obl[1, 0] - obl[1, 1])) / 1000f;
                    double averDer = 0;
                    double normalDerivation = 0;
                    int z = comboBox1.SelectedIndex;

                    population = Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value);
                    MaxIteration = Convert.ToInt32(dataGridView2.Rows[1].Cells[1].Value);
                    Params param = (comboBoxSelectParams.SelectedIndex == 0) ? Params.Linear : Params.Quadratic;

                    for (int i = 0; i < 100; i++)
                    {
                        alg = new Algoritm();
                        Wolf result = alg.FastStartAlg(population, MaxIteration, obl, z, param);

                        foreach (Vector item in exactPoints)
                        {
                            if ((Math.Abs(result.coords[0] - item[0]) < eps) && (Math.Abs(result.coords[1] - item[1]) < eps)) 
                            {
                                successCount++;
                                break;
                            }
                        }

                        averFuncDeviation.Add(Math.Abs(result.fitness - exact));
                    }

                    double deltaSum = 0;
                    for (int i = 0; i < 100; i++)
                        deltaSum += averFuncDeviation[i];
                    
                    averDer = deltaSum / 100f;

                    averFuncDeviation.Sort();
                    minDeviation = averFuncDeviation[0];
                    
                    double dispersion = 0;
                    for (int i = 0; i < 100; i++)
                        dispersion += Math.Pow(averFuncDeviation[i] - averDer, 2);
                    normalDerivation = Math.Sqrt((dispersion / 100f));

                    FileStream fs = new FileStream("protocol.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter r = new StreamWriter(fs);
                    r.Write(String.Format(@"| {0, 4}          |    {1, 6}        |      {2, 6}         |{3, 22:f6}                  |{4, 20:f6}              |{5, 20:f6}            |{6, 12}         |
|---------------+------------------+---------------------+----------------------------------------+----------------------------------+--------------------------------+---------------------|", z + 1, population, MaxIteration, averDer, minDeviation, normalDerivation, successCount));
                    r.Write("\n");
                    r.Close();
                    fs.Close();
                    Process.Start("protocol.txt");
                }
            }
            else
                MessageBox.Show("Введите корректные параметры", "Ошибка при запуске алгоритма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (File.Exists("protocol.txt"))
                File.Delete("protocol.txt");
        }
    }
}