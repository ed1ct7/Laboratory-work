using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Ya
{
    public class Model
    {
        private double a;
        private double b;

        public double A
        {
            get { return a; }
            set { a = value; }
        }
        public double B
        {
            get { return b; }
            set { b = value; }
        }

        public double square()
        {
            return a * b;
        }
    }
}
