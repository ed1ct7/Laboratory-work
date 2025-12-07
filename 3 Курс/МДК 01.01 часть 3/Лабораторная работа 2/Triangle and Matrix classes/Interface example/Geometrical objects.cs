using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Интерфейсы;

namespace Interface_example_project
{
    class Rectangle : IGeometrical, IDrawable
    {
        public void GetPerimeter()
        {
            Console.WriteLine("(a + b) * 2");
        }

        public void GetArea()
        {
            Console.WriteLine("a * b");
        }

        public void Draw()
        {
            Console.WriteLine("Rectangle");
        }
    }

    class Circle : IGeometrical, IDrawable
    {
        public void GetPerimeter()
        {
            Console.WriteLine("2 * pi * r");
        }
        public void GetArea()
        {
            Console.WriteLine("pi * r ^ 2");
        }

        public void Draw()
        {
            Console.WriteLine("Circle");
        }
    }

}
