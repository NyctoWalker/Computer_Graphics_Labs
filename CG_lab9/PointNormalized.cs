using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab9
{
    public class PointNormalized
    {
        private double X;
        public double x 
        {
            get { return X; }
            set { X = value; }
        }
        private double Y;
        public double y
        {
            get { return Y; }
            set { Y = value; }
        }
        private double Normalization { get; set; }
        public double N
        {
            get { return Normalization; }
            set { Normalization = value; }
        }

        public PointNormalized(double x, double y, double normalization)
        {
            this.x = x;
            this.y = y;
            this.N = normalization;
        }

        public PointNormalized(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.N = 1;
        }
    }
}
