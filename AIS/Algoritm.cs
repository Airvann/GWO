using System;
using System.Collections.Generic;
using System.Linq;

namespace GWO
{
    //Тип параметра а: линейная или квадратичная функция
    public enum Params
    {
        Linear,
        Quadratic
    }

    //Класс алгоритма
    public class Algoritm
    {
        public Params param;

        //Рандом
        private Random rand = new Random();

        //Размер популяции волков
        public int population;

        //Номер выбранной функции
        public int f;

        //область определения
        public double[,] D;

        //Максимальное число итераций
        public int MaxCount { get; set; }
        
        //Текущая итерация
        public int currentIteration = 0;

        //3 наиболее приспособленные особи
        public Wolf alfa = new Wolf();  //Альфа-особь
        public Wolf beta = new Wolf();  //Бета-особь
        public Wolf delta = new Wolf(); //Дельта-особь

        //Массив средней приспособленности
        public List<double> averageFitness = new List<double>();

        //Массив лучшей приспособленности
        public List<double> bestFitness = new List<double>();
        
        //Параметр а
        private double a;

        private Vector A_alfa = new Vector();
        private Vector A_beta = new Vector();
        private Vector A_delta = new Vector();

        private Vector C_alfa  = new Vector();
        private Vector C_beta  = new Vector();
        private Vector C_delta = new Vector();

        //Популяция волков
        public List<Wolf> individuals = new List<Wolf>();

        //Конструктор по умолчанию
        public Algoritm(){}

        //Начальное формирование популяции
        public void FormingPopulation()
        {
            for (int i = 0; i < population; i++)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();

                x = (Math.Abs(D[0, 0]) + Math.Abs(D[0, 1])) * x - Math.Abs(D[0, 0]);
                y = (Math.Abs(D[1, 0]) + Math.Abs(D[1, 1])) * y - Math.Abs(D[1, 0]);

                Wolf wolf = new Wolf(x, y, function(x, y, f));
                individuals.Add(wolf);
            }
        }
        public void Selection()
        {
            individuals = individuals.OrderByDescending(s => s.fitness).ToList();

            //Выбираем наиболее приспосоленных волков (сделано так, чтобы была передача значений, а не ссылки) 
            alfa.coords.vector[0] = individuals[0].coords.vector[0];    alfa.coords.vector[1] = individuals[0].coords.vector[1];    alfa.fitness = individuals[0].fitness;
            beta.coords.vector[0] = individuals[1].coords.vector[0];    beta.coords.vector[1] = individuals[1].coords.vector[1];    beta.fitness = individuals[1].fitness;
            delta.coords.vector[0] = individuals[2].coords.vector[0];   delta.coords.vector[1] = individuals[2].coords.vector[1];   delta.fitness = individuals[2].fitness;
        }

        //Формирование новой стаи
        public void NewPackGeneration()
        {
            //Выбор функции изменения параметра а
            if (param == Params.Quadratic)
                a = 2 * (1 - ((currentIteration * currentIteration) / ((double)MaxCount * MaxCount)));
            else
                a = 2 * (1 - currentIteration / (double)(MaxCount));

            for (int k = 0; k < population; k++)
            {

                A_alfa[0] = 2 * a * rand.NextDouble() - a;          A_alfa[1] =  2 * a * rand.NextDouble() - a;
                A_beta[0] = 2 * a * rand.NextDouble() - a;          A_beta[1] =  2 * a * rand.NextDouble() - a;
                A_delta[0] = 2 * a * rand.NextDouble() - a;         A_delta[1] = 2 * a * rand.NextDouble() - a;

                C_alfa[0] = 2 * rand.NextDouble();                  C_alfa[1] = 2 * rand.NextDouble();
                C_beta[0] = 2 * rand.NextDouble();                  C_beta[1] = 2 * rand.NextDouble();
                C_delta[0] = 2 * rand.NextDouble();                 C_delta[1] = 2 * rand.NextDouble();


                Vector D_alfa = Vector.Norm(Vector.HadamardMultiply(C_alfa, alfa.coords) - individuals[k].coords);

                Vector D_beta = Vector.Norm(Vector.HadamardMultiply(C_beta, beta.coords) - individuals[k].coords);

                Vector D_delta = Vector.Norm(Vector.HadamardMultiply(C_delta, delta.coords) - individuals[k].coords);

                individuals[k].coords = ((alfa.coords - Vector.HadamardMultiply(D_alfa, A_alfa)) +
                                             (beta.coords - Vector.HadamardMultiply(D_beta, A_beta)) +
                                             (delta.coords - Vector.HadamardMultiply(D_delta, A_delta))) / 3.0;

                double x = individuals[k].coords[0];
                double y = individuals[k].coords[1];

                //Проверка, не вышли ли мы за границы
                if (x < D[0, 0])
                    individuals[k].coords[0] = D[0, 0];
                if (x > D[0, 1])
                    individuals[k].coords[0] = D[0, 1];
                if (y < D[1, 0])
                    individuals[k].coords[1] = D[1, 0];
                if (y > D[1, 1])
                    individuals[k].coords[1] = D[1, 1];

                individuals[k].fitness = function(individuals[k].coords[0], individuals[k].coords[1], f);
            }
        }

        //Старт алгоритма
        public Wolf FastStartAlg(int population, int MaxCount, double[,] D, int f, Params param) 
        {
            this.param = param;
            this.MaxCount = MaxCount;
            this.population = population;
            this.D = D;
            this.f = f;

            FormingPopulation();

            for (int k = 1; k < MaxCount; k++)
            {
                Selection();
                NewPackGeneration();
                currentIteration++;
            }
            Selection();
            return alfa;
        }
        
        //Все тестовые функции
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

        //Вспомогательная функция
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

        //метод вычисления средней приспособленности
        public double AverageFitness()
        {
            double sum = 0;
            for (int i = 0; i < population; i++)
                sum += individuals[i].fitness;
            double fitness = (sum / population);
            averageFitness.Add(fitness);
            return fitness;
        }
    }
}
