using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GWO
{
    public partial class FormStepByStep : Form
    {
        public FormStepByStep(int z, double[,] obl, int PopulationCount, int MaxIteration, double exact)
        {
            InitializeComponent();
            InitDataGridView();


            this.z = z;
            this.obl = obl;
            this.exact = exact;
            this.PopulationCount = PopulationCount;
            this.MaxIteration = MaxIteration;
        }

        private int PopulationCount;
        private int MaxIteration;
        public double exact;
        public Algoritm algst = new Algoritm();

        public double[,] showobl = new double[2, 2];
        public int z;

        public float[] Ar = new float[8];
        public bool[] flines = new bool[8];
        public float[] A = new float[8];
        public double[,] showoblbase = new double[2, 2];
        public double[,] oblbase = new double[2, 2];
        public double[,] obl;
        public int stepsCount = 5;

        bool flag = false;
        bool[] Red = new bool[5];

        private void InitDataGridView()
        {
            dataGridViewAnswer.Columns[0].DefaultCellStyle.Font = new Font("Times new roman", 12, FontStyle.Italic);
            dataGridViewAnswer.RowCount = 3;
            dataGridViewAnswer.Rows[0].Cells[0].Value = "x";
            dataGridViewAnswer.Rows[1].Cells[0].Value = "y";
            dataGridViewAnswer.Rows[2].Cells[0].Value = "f*";

            dataGridViewIterationInfo.RowCount = 6;
            dataGridViewIterationInfo.Rows[0].Cells[0].Value = "Номер популяции:";
            dataGridViewIterationInfo.Rows[1].Cells[0].Value = "Размер популяции:";
            dataGridViewIterationInfo.Rows[2].Cells[0].Value = "Количество итераций:";
            dataGridViewIterationInfo.Rows[3].Cells[0].Value = "Волк с лучшей приспособленностью:";
            dataGridViewIterationInfo.Rows[4].Cells[0].Value = "Приспособленность лучшего волка:";
            dataGridViewIterationInfo.Rows[5].Cells[0].Value = "Средняя приспособленность популяции:";
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            Pen p1 = new Pen(Color.Black, 2);
            Pen p2 = new Pen(Color.Gray, 2);
            Pen p3 = new Pen(Color.Red, 2);
            Font f1 = new Font("TimesNewRoman", 12,FontStyle.Bold);

            //Создание начальной популяции -> Селекция (черная)
            e.Graphics.DrawLine(p1, 70, 30, 70, 119);
            e.Graphics.DrawLine(p1, 70, 115, 75, 105);
            e.Graphics.DrawLine(p1, 69, 115, 64, 105);

            if (Red[0] == true)
            {
                e.Graphics.DrawLine(p3, 70, 30, 70, 119);
                e.Graphics.DrawLine(p3, 70, 115, 75, 105);
                e.Graphics.DrawLine(p3, 69, 115, 64, 105);
            }

            //Селекция ->  перемещение (черная)
            e.Graphics.DrawLine(p1, 45, 155, 195, 155);
            e.Graphics.DrawLine(p1, 188, 154, 178, 149);
            e.Graphics.DrawLine(p1, 188, 155, 178, 160);

            if (Red[1] == true)
            {
                e.Graphics.DrawLine(p3, 45, 155, 195, 155);
                e.Graphics.DrawLine(p3, 188, 154, 178, 149);
                e.Graphics.DrawLine(p3, 188, 155, 178, 160);
            }

            e.Graphics.DrawLine(p1, 295, 155, 295, 390);
            e.Graphics.DrawLine(p1, 250, 390, 295, 390);
            e.Graphics.DrawLine(p1, 250, 390, 260, 395);
            e.Graphics.DrawLine(p1, 250, 389, 260, 384);

            if (Red[2] == true)
            {
                e.Graphics.DrawLine(p3, 295, 155, 295, 390);
                e.Graphics.DrawLine(p3, 250, 390, 295, 390);
                e.Graphics.DrawLine(p3, 250, 390, 260, 395);
                e.Graphics.DrawLine(p3, 250, 389, 260, 384);
            }

            //Проверка условия окончания ->  Селекиця 
            e.Graphics.DrawLine(p1, 70, 390, 220, 390);
            e.Graphics.DrawLine(p1, 70, 390, 70, 155);
            e.Graphics.DrawLine(p1, 70, 195, 75, 205);
            e.Graphics.DrawLine(p1, 69, 195, 64, 205);

            if (Red[3] == true)
            {
                e.Graphics.DrawLine(p3, 70, 390, 220, 390);
                e.Graphics.DrawLine(p3, 70, 390, 70, 155);
                e.Graphics.DrawLine(p3, 70, 195, 75, 205);
                e.Graphics.DrawLine(p3, 69, 195, 64, 205);
            }

            e.Graphics.DrawLine(p1, 180, 390, 180, 500);
            e.Graphics.DrawLine(p1, 180, 485, 185, 475);
            e.Graphics.DrawLine(p1, 179, 485, 174, 475);

            //Провекра условия -> Выход 
            if (Red[4] == true)
            {
                e.Graphics.DrawLine(p3, 180, 390, 180, 500);
                e.Graphics.DrawLine(p3, 180, 485, 185, 475);
                e.Graphics.DrawLine(p3, 179, 485, 174, 475);
            }

            e.Graphics.DrawString("да", f1, Brushes.Black, 150, 435);
            e.Graphics.DrawString("нет", f1, Brushes.Black, 70, 400);
            
            e.Graphics.DrawLine(p2, 0, 580, 400, 580);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                //заполнение массива состояний
                Red[0] = true;
                for (int i = 1; i < stepsCount; i++)
                    Red[i] = false;

                flag = true;    //Начало работы алгоритма

                algst = new Algoritm
                {
                    MaxCount = MaxIteration,
                    population = PopulationCount,
                    f = z,
                    D = obl
                };

                algst.FormingPopulation();

                algst.currentIteration = 1;                        // Счетчик итераций
                dataGridViewIterationInfo.Rows[0].Cells[1].Value = algst.currentIteration;
                dataGridViewIterationInfo.Rows[1].Cells[1].Value = algst.population;
                dataGridViewIterationInfo.Rows[2].Cells[1].Value = algst.MaxCount;
                dataGridViewIterationInfo.Refresh();
                pictureBoxDiagramm.Refresh();
                pictureBox1.Refresh();
            }
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

            Pen p12 = new Pen(Color.DarkOrange, 2);
            Pen p13 = new Pen(Color.DarkGreen, 2);
            Pen p14 = new Pen(Color.Red, 2);

            Font font1 = new Font("TimesNewRoman", 10, FontStyle.Bold);
            Font font2 = new Font("TimesNewRoman", 8);

            pictureBox1.BackColor = Color.White;
         
            //TODO: ShowObl == Obl?
            double x1 = showobl[0, 0];
            double x2 = showobl[0, 1];
            double y1 = showobl[1, 0];
            double y2 = showobl[1, 1];

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
                for (int i = 0; i < algst.population; i++)
                    e.Graphics.FillEllipse(Brushes.Blue, (float)((algst.individuals[i].coords.vector[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algst.individuals[i].coords.vector[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);
                e.Graphics.FillEllipse(Brushes.Red, (float)((algst.individuals[0].coords.vector[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algst.individuals[0].coords.vector[1] * k - y1) * h / (y2 - y1) - 3), 8, 8);
            }

            if((Red[1] == true)||(Red[2] == true)||(Red[3] == true))
            {
                e.Graphics.DrawEllipse(p12, (float)((algst.individuals[1].coords.vector[0] * k - x1) * w / (x2 - x1) - 3) - 7, (float)(h - (algst.individuals[1].coords.vector[1] * k - y1) * h / (y2 - y1) - 3) - 7, 20, 20);
                e.Graphics.DrawEllipse(p13, (float)((algst.individuals[2].coords.vector[0] * k - x1) * w / (x2 - x1) - 3) - 7, (float)(h - (algst.individuals[2].coords.vector[1] * k - y1) * h / (y2 - y1) - 3) - 7, 20, 20);
                e.Graphics.DrawEllipse(p14, (float)((algst.individuals[0].coords.vector[0] * k - x1) * w / (x2 - x1) - 2) - 7, (float)(h - (algst.individuals[0].coords.vector[1] * k - y1) * h / (y2 - y1) - 2) - 7, 20, 20);
            }

            for (int i = -6; i < 12; i++)
            {
                e.Graphics.DrawLine(p10, (float)((x1 - i * step) * w / (x1 - x2)), h - a - 5, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                e.Graphics.DrawLine(p10, a - 5, (float)(h - (y1 - i * step) * h / (y1 - y2)), a + 5, (float)(h - (y1 - i * step) * h / (y1 - y2)));
                e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, a - 30, (float)(h - 5 - (y1 - i * step) * h / (y1 - y2)));
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
                funct = (float)((-x1 * x1 + 10 * Math.Cos(2 * Math.PI * x1)) + (-x2 * x2 + 10 * Math.Cos(2 * Math.PI * x2)));
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
            else if (f == 8)
            {
                funct = (float)(-(1 - x1) * (1 - x1) - 100 * (x2 - x1 * x1) * (x2 - x1 * x1));
            }
            else if (f == 9)
            {
                funct = (float)(-x1 * x1 - x2 * x2);
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

        private void buttonMove_Click(object sender, EventArgs e)
        {
            if (Red[1] == true)
            {
                Red[1] = false;
                Red[2] = true;
                algst.NewPackGeneration();

                pictureBoxDiagramm.Refresh();
                pictureBox1.Refresh();
            }
        }

        private void buttonBest_Click(object sender, EventArgs e)
        {
            buttonAnswer.Enabled = false;
            buttonNext.Enabled = false;

            if (Red[4] == false) 
            {
                if (Red[3] == true)
                {
                    Red[3] = false;
                    Red[1] = true;
                }   

                if (Red[0] == true) 
                {
                    Red[0] = false;
                    Red[1] = true;
                }
                    
                //селекция
                algst.Selection();
                algst.bestFitness.Add(algst.alfa.fitness);
                algst.AverageFitness();
                UpdateIterationInfo();

                pictureBoxDiagramm.Refresh();
                pictureBox1.Refresh();
                pictureBoxGraph.Refresh();
            }
        }

        private void UpdateIterationInfo()
        {
            dataGridViewIterationInfo.Rows[0].Cells[1].Value = String.Format($"{algst.currentIteration}");
            dataGridViewIterationInfo.Rows[3].Cells[1].Value = String.Format($"{algst.alfa.coords.vector[0]:F2}   {algst.alfa.coords.vector[1]:F2}");
            dataGridViewIterationInfo.Rows[4].Cells[1].Value = String.Format($"{algst.alfa.fitness:F2}");
            dataGridViewIterationInfo.Rows[5].Cells[1].Value = String.Format($"{algst.averageFitness[algst.averageFitness.Count - 1]:F7}");
            dataGridViewIterationInfo.Refresh();
        }

        private void UpdateAnswer()
        {
            dataGridViewAnswer.Rows[0].Cells[1].Value = string.Format($"{algst.alfa.coords[0]:F8}");
            dataGridViewAnswer.Rows[1].Cells[1].Value = string.Format($"{algst.alfa.coords[1]:F8}");
            dataGridViewAnswer.Rows[2].Cells[1].Value = string.Format($"{algst.alfa.fitness:F8}");
        }

        private void buttonEndVerify_Click(object sender, EventArgs e)
        {
            //условия глобального поиска
            if (Red[2] == true)
            {
                if (algst.currentIteration < algst.MaxCount)
                {
                    algst.currentIteration++;
                    UpdateIterationInfo();
                    Red[3] = true;
                    Red[2] = false;
                    buttonAnswer.Enabled = true;
                    buttonNext.Enabled = true;
                }

                else 
                {
                    Red[2] = false;
                    Red[4] = true;
                }
                pictureBoxDiagramm.Refresh();
            }
        }
        private void buttonEnd_Click(object sender, EventArgs e)
        {
            //ответ
            if (Red[4] == true)
            {
                Red[3] = false;
                algst.Selection();

                UpdateAnswer();
                UpdateIterationInfo();

                pictureBox1.Refresh();               
                dataGridViewAnswer.Refresh();
                pictureBoxDiagramm.Refresh();
                flag = false; 
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (Red[3] == true)
            {
                if (algst.currentIteration < algst.MaxCount)
                {
                    algst.Selection();
                    algst.NewPackGeneration();

                    algst.AverageFitness();
                    algst.bestFitness.Add(algst.alfa.fitness);
                    algst.currentIteration++;

                    UpdateIterationInfo();
                }
                else
                {
                    Red[3] = false;
                    Red[4] = true;
                }
                pictureBox1.Refresh();
                pictureBoxGraph.Refresh();
                pictureBoxDiagramm.Refresh();
            }
        }
        private void pictureBoxGraph_Paint(object sender, PaintEventArgs e)
        {
            if (flag == true)
            {
                float w = pictureBoxGraph.Width;
                float h = pictureBoxGraph.Height;
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

                float mx = (w - 60) / (algst.currentIteration + 5);
                float mh = 0;
                try { mh = (float)((h - 60) / ((1.1 * exact - Math.Min(0, algst.averageFitness[0])))); }
                catch { mh = (float)((h - 60) / (1.1 * exact)); }

                double a = 1;


                if (algst.currentIteration < 31) a = 2;
                else if (algst.currentIteration < 101) a = 5;
                else if (algst.currentIteration < 151) a = 10;
                else if (algst.currentIteration < 301) a = 20;
                else if (algst.currentIteration < 501) a = 50;
                else if (algst.currentIteration < 1001) a = 100;
                else if (algst.currentIteration < 2001) a = 200;
                else a = 1000;

                double b = 0;
                try { b = 1.1 * exact - Math.Min(0, algst.averageFitness[0]); }
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

                for (int i = 0; i < algst.population; i++)
                {

                    //float s = i / a;
                    if (Math.Floor((decimal)(i / a)) - (decimal)(i / a) == 0)
                    {
                        e.Graphics.DrawLine(p1, (float)(x0 + (mx) * (i)), y0 + 2, (float)(x0 + mx * (i)), y0 - 2);
                        e.Graphics.DrawString(Convert.ToString(i), f1, Brushes.Black, (float)(x0 + mx * (i)), (float)(y0 + 4));

                    }
                }

                if (Math.Floor((decimal)((algst.MaxCount) / a)) - (decimal)((algst.MaxCount) / a) == 0)
                {
                    e.Graphics.DrawLine(p1, (float)(x0 + (mx) * (algst.MaxCount)), y0 + 2, (float)(x0 + mx * (algst.MaxCount)), y0 - 2);
                    e.Graphics.DrawString(Convert.ToString(algst.MaxCount), f1, Brushes.Black, (float)(x0 + mx * (algst.MaxCount)), (float)(y0 + 4));
                }

                if (flag == true)
                {
                    e.Graphics.FillEllipse(Brushes.Green, (float)(x0), (float)(y0 - 1 - mh * (algst.averageFitness[0] - Math.Min(0, algst.averageFitness[0]))), 3, 3);
                    e.Graphics.FillEllipse(Brushes.Blue, (float)(x0), (float)(y0 - 1 - mh * (algst.alfa.coords[0] - Math.Min(0, algst.averageFitness[0]))), 3, 3);


                    if (algst.bestFitness.Count >= 2 && algst.averageFitness.Count >= 2)
                    for (int i = 0; i < algst.averageFitness.Count - 1; i++)
                    {

                        {
                            e.Graphics.DrawLine(p2, (float)(x0 + mx * i), (float)(y0 - mh * (algst.averageFitness[i] - Math.Min(0, algst.averageFitness[0]))), (float)(x0 + mx * (i + 1)), (float)(y0 - mh * (algst.averageFitness[i + 1] - Math.Min(0, algst.averageFitness[0]))));
                            e.Graphics.DrawLine(p3, (float)(x0 + mx * i), (float)(y0 - mh * (algst.bestFitness[i] - Math.Min(0, algst.averageFitness[0]))), (float)(x0 + mx * (i + 1)), (float)(y0 - mh * (algst.bestFitness[i + 1] - Math.Min(0, algst.averageFitness[0]))));
                        }
                    }
                }

                float zero = 0;
                try { zero = (float)(y0 + mh * Math.Min(0, algst.averageFitness[0])); }
                catch { zero = (float)(y0); }

                for (int i = -6; i < 12; i++)
                {
                    e.Graphics.DrawLine(p1, (float)(x0 + 2), (float)(zero - mh * c * i), (float)(x0 - 2), (float)(zero - mh * c * i));
                    if ((zero - mh * c * i - 8 > 11) && (zero - mh * c * i - 8 < h - 20)) e.Graphics.DrawString(Convert.ToString((c * i)), f1, Brushes.Black, (float)(x0 - 24), (float)(zero - mh * c * i - 8));
                }
                e.Graphics.DrawString("k", f2, Brushes.Black, (float)(w - 15), (float)(y0 + 4));
                e.Graphics.DrawString("f", f2, Brushes.Black, (float)(x0 - 24), (float)(2));
            }
        }
        private void buttonAnswer_Click(object sender, EventArgs e)
        {
            if (Red[3] == true) 
            {
                Red[3] = false;
                for (int i = 1; algst.currentIteration < MaxIteration; i++)
                {
                    algst.Selection();
                    algst.NewPackGeneration();

                    algst.bestFitness.Add(algst.alfa.fitness);
                    algst.AverageFitness();

                    algst.currentIteration++;
                }

                algst.Selection();

                UpdateAnswer();
                UpdateIterationInfo();

                pictureBox1.Refresh();
                dataGridViewIterationInfo.Refresh();
                dataGridViewAnswer.Refresh();
                pictureBoxGraph.Refresh();
                pictureBoxDiagramm.Refresh();
                flag = false;
            }
        }
    }
}