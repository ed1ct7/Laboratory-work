using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Triangle
{
    public class Parameters 
    {
        private double _a;
        public double A
        {
            get { return _a; }
            set
            {
                _a = value;
            }
        }
        private double _b;
        public double B
        {
            get { return _b; }
            set
            {
                _b = value;
            }
        }
        private double _c;
        public double C
        {
            get { return _c; }
            set
            {
                _c = value;
            }
        }

        public override string ToString()
        {
            return $"A: {_a:F2}, B: {_b:F2}, C: {_c:F2}";
        }
    }

    public class Triangle
    {
        public Triangle(double a, double b, double c)
        {
            _sides = new Parameters { A = a, B = b, C = c };
            CalculateAngles();
        }

        public Triangle()
        {
            _sides = new Parameters { A = 1, B = 1, C = 1 };
            CalculateAngles();
        }

        public bool Isosceles
        {
            get => Math.Abs(Sides.A - Sides.B) < double.Epsilon ||
                   Math.Abs(Sides.A - Sides.C) < double.Epsilon ||
                   Math.Abs(Sides.B - Sides.C) < double.Epsilon;
        }

        private Parameters _sides;
        public Parameters Sides
        {
            get { return _sides; }
            set
            {
                _sides = value;
                CalculateAngles();
            }
        }

        private Parameters _angles;
        public Parameters Angles
        {
            get { return _angles; }
            set
            {
                _angles = value;
            }
        }

        public bool IsExist()
        {
            double a = Sides.A;
            double b = Sides.B;
            double c = Sides.C;

            return a > 0 && b > 0 && c > 0 &&
                   a + b > c &&
                   a + c > b &&
                   b + c > a;
        }

        public double Perimeter()
        {
            if (!IsExist()) return 0;
            return Sides.A + Sides.B + Sides.C;
        }

        public double Area()
        {
            if (!IsExist()) return 0;

            // Using Heron's formula
            double p = Perimeter() / 2;
            double a = Sides.A;
            double b = Sides.B;
            double c = Sides.C;

            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public void CalculateAngles()
        {
            if (!IsExist())
            {
                Angles = new Parameters { A = 0, B = 0, C = 0 };
                return;
            }

            double a = Sides.A;
            double b = Sides.B;
            double c = Sides.C;

            double angleA = Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * (180 / Math.PI);

            double angleB = Math.Acos((a * a + c * c - b * b) / (2 * a * c)) * (180 / Math.PI);

            double angleC = Math.Acos((a * a + b * b - c * c) / (2 * a * b)) * (180 / Math.PI);

            double sum = angleA + angleB + angleC;
            if (Math.Abs(sum - 180) > 0.001)
            {
                double correction = (180 - sum) / 3;
                angleA += correction;
                angleB += correction;
                angleC += correction;
            }

            Angles = new Parameters { A = angleA, B = angleB, C = angleC };
        }

        public override string ToString()
        {
            CalculateAngles();
            if (IsExist() == true) {
                return "Triangle doesn't exist";
            }
            else {
                return $"Triangle with sides: {Sides}\nAngles: {Angles}\nPerimeter: {Perimeter():F2}\nArea: {Area():F2}\nIsosceles: {Isosceles}";
            }
        }
    }

    public class IsoscelesTriangle : Triangle
    {
        public new bool IsIsosceles()
        {
            if (!IsExist()) return false;

            double a = Sides.A;
            double b = Sides.B;
            double c = Sides.C;

            return Math.Abs(a - b) < double.Epsilon ||
                   Math.Abs(a - c) < double.Epsilon ||
                   Math.Abs(b - c) < double.Epsilon;
        }

        public override string ToString()
        {
            return $"Isosceles Triangle with sides: {Sides}\nAngles: {Angles}\nPerimeter: {Perimeter():F2}\nArea: {Area():F2}";
        }
    }
}