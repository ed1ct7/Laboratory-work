using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract_class_example
{
    public abstract class Cat_model
    {
		private int _age;
		public int Age
		{
			get { return _age; }
			set {
				if(value > 0) _age = value;
				else _age = 0; }
		}

		public abstract void View();
		public Cat_model(){ Age = 0; }
		public Cat_model(int age) { 
			Age = age;
		}
	}
	public class HomeCat : Cat_model
	{
		private String _name;
		public String Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public HomeCat() : base()
		{
			Name = "no name";
		}
        public HomeCat(int age, String name) : base(age)
        {
            Name = name;
        }
        public override void View()
        {
            Console.WriteLine("Домашняя кошка - " + Name + ", возраст - " + Age);
        }
	}
}
