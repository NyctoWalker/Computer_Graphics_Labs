using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab10
{
    public class PointNormalized3D
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
        private double Z;
        public double z
        {
            get { return Z; }
            set { Z = value; }
        }
        private double Normalization { get; set; }
        public double N
        {
            get { return Normalization; }
            set { Normalization = value; }
        }

        public PointNormalized3D(double x, double y, double z, double normalization)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.N = normalization;
        }

        public PointNormalized3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.N = 1;
        }
    }
}
