﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIS
{
    public class Wolf
    {
        public Vector coords;
        //TODO: сделать геттер
        public double fitness = 0;
        public Wolf() { coords = new Vector(0, 0); fitness = 0; }
        public Wolf(double x, double y, double fitness)
        {
            coords = new Vector(x, y);
            this.fitness = fitness;
        }
    }
}