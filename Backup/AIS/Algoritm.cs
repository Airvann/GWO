using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIS
{
    public class Algoritm
    {
        public int Ip;
        public int K;
        public int N;
        public double[,] oblast;
        public List<double[]> Pop = new List<double[]>();
        private List<double> Fitnes = new List<double>();
       // private List<double> NormalFitnes  = new List<double>();
        public int z;
        private double Mf;
        private double Mf2;
        double gamma;
        double eps;
        double sigma;
        int percent;
        private int Klon;
        private double Parkl;
        public List<List<double[]>> Kloni = new List<List<double[]>>();
        private List<List<double>> KlonFitnes = new List<List<double>>();
        public int kpop = 0;
        public double Mfend = 0;
        public double LocalS = 0;
        public List<double> AverF = new List<double>();
        public List<double> BestF = new List<double>();

        Random R = new Random();

        public int Nm;
        int Nm1;
        int m;
        

        public void Init(int ip, int k, double[,] obl, int f, int klon, double parkl, int n, double g, double epsilon, double sigm, int perc)
        {
            Ip = ip;
            K = k;
            oblast = obl;
            z = f;
            Klon = klon;
            Parkl = parkl;
            N = n;
            gamma = g;
            eps = epsilon;
            sigma = sigm;
            percent = perc;
        }

        public void FirstPop()
        {
            Mf = 0;
            for (int i = 0; i < Ip; i++)
            {
                double[] member = new double[4];
                for (int j = 0; j < N; j++ )
                    member[j] = R.NextDouble() * (oblast[j, 1] - oblast[j, 0]) + oblast[j, 0];
               // member[1] = R.NextDouble() * (oblast[1, 1] - oblast[1, 0]) + oblast[1, 0];
                Fitnes.Add(Func(member[0],member[1]));
                member[2] = Func(member[0], member[1]);
                Pop.Add(member);
               // Mf += Fitnes[i];
                Mf += member[2];
            }
            Mf = Mf / Ip;

            Pop.Sort(delegate(double[] p1, double[] p2)
            { return p2[2].CompareTo(p1[2]); });
            
            AverF.Add(Mf);
            BestF.Add(Pop[0][2]);

            Nm = Pop.Count;
            Nm1 = Pop.Count;
            m = 0;
            kpop = 0;

        }

        private double Func(double x1, double x2)
        {
            double func = 0;
            if (z == 0)
                func =  x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2)));
            else if (z == 1)
                func = x1 * Math.Sin(4 * Math.PI * x1) - x2 * Math.Sin(4 * Math.PI * x2+Math.PI)+1;
            else if (z == 2)
            {
                double[] c6 = Cpow(x1, x2, 6);
                func = (float)(1 / (1 + Math.Sqrt((c6[0] - 1) * (c6[0] - 1) + c6[1] * c6[1])));
            }
            else if (z == 3)
            {
                func = (float)(0.5 - (Math.Pow(Math.Sin(Math.Sqrt(x1 * x1 + x2 * x2)), 2) - 0.5) / (1 + 0.001 * (x1 * x1 + x2 * x2)));
            }
            else if (z == 4)
            {
                func = (float)((-x1 * x1 + 10 * Math.Cos(2 * Math.PI * x1)) + (-x2 * x2 + 10 * Math.Cos(Math.PI * x2)));
            }
            else if (z == 5)
            {
                func = (float)(-Math.E + 20 * Math.Exp(-0.2 * Math.Sqrt((x1 * x1 + x2 * x2) / 2)) + Math.Exp((Math.Cos(2 * Math.PI * x1) + Math.Cos(2 * Math.PI * x2)) / 2));
            }
            else if (z == 6)
            {
                func = (float)(Math.Pow(Math.Cos(2 * x1 * x1) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x1) - 1.2, 2) - Math.Pow(Math.Cos(2 * x2 * x2) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x2) - 1.2, 2));
            }
            else if (z == 7)
            {
                func = (float)(-Math.Sqrt(Math.Abs(Math.Sin(Math.Sin(Math.Sqrt(Math.Abs(Math.Sin(x1 - 1))) + Math.Sqrt(Math.Abs(Math.Sin(x2 + 2)))))))+1);
            }
            return func;
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

        public void Work()
        {
            for (int i = 0; i < K; i++)
            {
                Normalization();
                Klonirovanie();
                Mutation();
                Selection(); 

                //if (i > 0)
                {
                    if (LockProw(i)==true)//Math.Abs(Mf - Mf2) <= eps)
                    {
                        Sokrashenie();
                        if (GlobalProw(i)==false)
                        {
                            Dobavlenie();
                        }
                        else if (GlobalProw(i) == true)
                        {
                            kpop = i;
                            break;
                        }
                    }
                    
                }
            }
            if (kpop == 0)
            {
                kpop = K;
                Sokrashenie();
            }
            Mfend = Math.Round(Mf,6);
            LocalS = m+1;
        }

        public void Klonirovanie()
        {
            Kloni.Clear();
            if (Klon == 1)//простое
            {
                    for (int i = 0; i < Pop.Count; i++)
                    {
                        List<double[]> kloni = new List<double[]>();
                       // List<double> klfitn = new List<double>();
                        for (int j = 0; j < Parkl; j++)
                        {
                            double[] kl = new double[3];
                            kl[0] = Pop[i][0];
                            kl[1] = Pop[i][1];
                            kl[2] = Pop[i][2];
                            kloni.Add(kl);
                           // double fit = Pop[i][2];//Fitnes[i];
                           // klfitn.Add(fit);
                        }
                        Kloni.Add(kloni);
                       // KlonFitnes.Add(klfitn); 
                    }
                    
            }
            else if (Klon == 2)//сложное
            {
                for (int i = 0; i < Pop.Count; i++)
                {
                    List<double[]> kloni = new List<double[]>();
                    // List<double> klfitn = new List<double>();
                    for (int j = 0; j < (Parkl*Pop.Count)/(Pop.Count-i); j++)
                    {
                        double[] kl = new double[3];
                        kl[0] = Pop[i][0];
                        kl[1] = Pop[i][1];
                        kl[2] = Pop[i][2];
                        kloni.Add(kl);
                        // double fit = Pop[i][2];//Fitnes[i];
                        // klfitn.Add(fit);
                    }
                    Kloni.Add(kloni);
                    // KlonFitnes.Add(klfitn); 
                }
            }
        
        }

        public void Mutation()
        {
            for (int i = 0; i < Kloni.Count; i++)
            {
                for (int j = 0; j < Kloni[i].Count; j++)
                {
                    for (int n = 0; n < 2; n++)
                    {
                        double norm = Math.Exp(-Pop[i][3])*Math.Sqrt(-2 * Math.Log(R.NextDouble())) * Math.Sin(2*Math.PI*R.NextDouble())/gamma;
                        if ((Kloni[i][j][n] + norm < oblast[n,1]) && (Kloni[i][j][n] + norm > oblast[n,0]))
                        Kloni[i][j][n] = Kloni[i][j][n] + norm;
                    }
                    Kloni[i][j][2] = Func(Kloni[i][j][0], Kloni[i][j][1]);
                    //Kloni[i, 3 * j] = Kloni[i,3*j];
                    //Kloni[i, 3 * j + 1] = Kloni[i, 3 * j + 1];

                }
            }
        }

        public void GradMutation()
        {
            //Kloni.Clear();

            for (int i = 0; i < Kloni.Count; i++)
            {
                for (int j = 0; j < Kloni[i].Count; j++)
                {
                   // for (int n = 0; n < 2; n++)
                   // {
                   //     double norm = Math.Exp(-Pop[i][3]) * Math.Sqrt(-2 * Math.Log(R.NextDouble())) * Math.Sin(2 * Math.PI * R.NextDouble()) / gamma;
                   //     if ((Kloni[i][j][n] + norm < oblast[n, 1]) && (Kloni[i][j][n] + norm > oblast[n, 0]))
                   //         Kloni[i][j][n] = Kloni[i][j][n] + norm;
                   // }
                    double xj = 0.0001;
                   // if ((Kloni[i][j][j] + xj < oblast[n, 1]) && (Kloni[i][j][j] + xj > oblast[n, 0]))
                        Kloni[i][j][j] = Kloni[i][j][j] + xj;
                    
                    Kloni[i][j][2] = Func(Kloni[i][j][0], Kloni[i][j][1]);
                }
            }
        }

        public void GradSelection()
        {
            Mf2 = Mf;
            Mf = 0;
            for (int i = 0; i < Pop.Count; i++)
            {
                double x11 = (Kloni[i][0][2] - Pop[i][2]) / (1 / (1 + gamma * Pop[i][3]));
                double x12 = (Kloni[i][1][2] - Pop[i][2]) / (1 / (1 + gamma * Pop[i][3]));
                double r = Math.Abs(Math.Sqrt(-2 * Math.Log(R.NextDouble())) * Math.Sin(2 * Math.PI * R.NextDouble())) * (1 / (1 + gamma * Pop[i][3]));//(Math.Exp(-gamma*Pop[i][3]));
                double klx = Pop[i][0] + r * x11;
                double kly = Pop[i][1] + r * x12;


                if ((klx < oblast[0, 1]) && (klx > oblast[0, 0]) && (kly < oblast[1, 1]) && (kly > oblast[1, 0]))
                {
                    while ((Func(klx, kly) < Pop[i][2])&&(r>0.00000000000000000000000000001))
                    {
                        r = r / 2;
                        klx = Pop[i][0] + r * x11;
                        kly = Pop[i][1] + r * x12;
                    }
                    if (Func(klx, kly) > Pop[i][2])
                    {
                        // int imax = KlonFitnes[i].IndexOf(max);
                        // Fitnes[i] = KlonFitnes[i][imax];
                        Pop[i][0] = klx;
                        Pop[i][1] = kly;
                        Pop[i][2] = Func(klx, kly);
                    }
                }
                Mf += Pop[i][2];
            }
            Mf = Mf / Pop.Count;

            AverF.Add(Mf);
            BestF.Add(Pop[0][2]);
        }


        public void Normalization()
        {
           // double max = Fitnes.Max();
           // double min = Fitnes.Min();

            Pop.Sort(delegate(double[] p1, double[] p2)
            { return p1[2].CompareTo(p2[2]); });
            Pop.Reverse();

            double max = Pop[0][2];
            double min = Pop[Pop.Count - 1][2];
            
            for (int i = 0; i < Pop.Count; i++)
            {
               // NormalFitnes.Add((Fitnes[i]-min)/(max-min));
                Pop[i][3] = (Pop[i][2]-min)/(max-min);
            }

        }

        public void Selection()
        {
            Mf2 = Mf;
            Mf = 0;
            for (int i = 0; i < Pop.Count; i++)
            {
                Kloni[i].Sort(delegate(double[] k1, double[] k2)
                { return k1[2].CompareTo(k2[2]); });
                Kloni[i].Reverse();
                double max = Kloni[i][0][2];
                if (max > Pop[i][2])
                {
                   // int imax = KlonFitnes[i].IndexOf(max);
                   // Fitnes[i] = KlonFitnes[i][imax];
                    Pop[i][0] = Kloni[i][0][0];
                    Pop[i][1] = Kloni[i][0][1];
                    Pop[i][2] = Kloni[i][0][2];
                }
                Mf += Pop[i][2];
            }
            Mf = Mf / Pop.Count;

            AverF.Add(Mf);
            BestF.Add(Pop[0][2]);
        
        }

        public bool LockProw(int i)
        {
            bool fl = false;
            if (((i<K)&&(Math.Abs(Mf - Mf2) <= eps))) fl = true;
            return fl;
        }

        public bool GlobalProw( int i)
        {
            bool fl = false;
            Nm1 = Nm;
            Nm = Pop.Count;
            if (((m == 0) || (Nm1 != Nm))&&(i<K)) fl = false;
            else fl = true;
            return fl;
        }


        public void Sokrashenie()
        {
            Fitnes.Sort();
            Fitnes.Reverse();
            Pop.Sort(delegate(double[] p1, double[] p2)
            { return p2[2].CompareTo(p1[2]); });
            //Pop.Reverse();

            for (int p = 0; p < Pop.Count; p++)
            {
                for (int s = p + 1; s < Pop.Count; s++)
                {
                    double prov = Math.Sqrt(Math.Pow(Pop[p][0] - Pop[s][0], 2) + Math.Pow(Pop[p][1] - Pop[s][1], 2));
                    if (Math.Sqrt(Math.Pow(Pop[p][0] - Pop[s][0], 2) + Math.Pow(Pop[p][1] - Pop[s][1], 2)) <= sigma)
                    {
                        Pop.RemoveAt(s);
                        s = s - 1;
                    }
                }
            }
            Mf = 0;
            for (int j = 0; j < Pop.Count; j++)
                Mf += Pop[j][2];
            Mf = Mf / Pop.Count;
            AverF.RemoveAt(AverF.Count - 1);
            BestF.RemoveAt(BestF.Count - 1);
            AverF.Add(Mf);
            BestF.Add(Pop[0][2]);
        }


        public void Dobavlenie()
        {
            Mf2 = Mf;
            int Nnew = Convert.ToInt16(Math.Floor((double)Pop.Count * percent / 100));
            Mf = 0;
            for (int j = 0; j < Nnew; j++)
            {
                double[] member = new double[4];
                for (int b = 0; b < N; b++)
                    member[b] = R.NextDouble() * (oblast[b, 1] - oblast[b, 0]) + oblast[b, 0];
                Fitnes.Add(Func(member[0], member[1]));
                member[2] = Func(member[0], member[1]);
                Pop.Add(member);
            }
            Pop.Sort(delegate(double[] p1, double[] p2)
            { return p2[2].CompareTo(p1[2]); });
            for (int j = 0; j < Pop.Count; j++)
                Mf += Pop[j][2];
            Mf = Mf / Pop.Count;
            m++;
            AverF.RemoveAt(AverF.Count - 1);
            BestF.RemoveAt(BestF.Count - 1);
            AverF.Add(Mf);
            BestF.Add(Pop[0][2]);
        }

        public void NewPop()
        { 
        
        
        }


    }
}
