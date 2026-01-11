using Interface_example_project;
using Интерфейсы;

namespace Interface_example
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IGeometrical> figures = new List<IGeometrical>();
            figures.Add(new Rectangle());
            figures.Add(new Circle());
            Type myType;
            Type[] list;
            foreach (IGeometrical figure in figures)
            {
                figure.GetArea();
                figure.GetPerimeter();
                myType = figure.GetType();
                list = myType.GetInterfaces();
                Console.WriteLine("Тип - " +myType);
                foreach (Type item in list)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("________________");

            foreach (IDrawable figure in figures)
            {
                figure.Draw();
            }
        }
    }
}
