namespace Abstract_class_example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cat_model cat1 = new HomeCat(5, "Васька");
            cat1.View();
            Cat_model cat2 = new HomeCat();
            cat2.View();
            Cat_model cat3 = new HomeCat(-4, "Васька");
            cat3.View();
        }
    }
}
