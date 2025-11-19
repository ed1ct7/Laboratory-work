using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVP_Triangle
{
    public partial class Form1 : Form, IView
    {
        Presenter presenter;
        public Form1()
        {
            InitializeComponent();
            presenter = new Presenter(this);
        }

        public event EventHandler<EventArgs> SetA;
        public event EventHandler<EventArgs> SetB;
        public event EventHandler<EventArgs> SetC;

        public string Data
        {
            get => label5.Text;
            set => label5.Text = value;
        }

        public T TryFuncCheck<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double InputA
        {
            get => TryFuncCheck(() => Convert.ToDouble(textBox1.Text));
            set { textBox1.Text = value.ToString(); }
        }

        public double InputB
        {
            get => TryFuncCheck(() => Convert.ToDouble(textBox2.Text));
            set { textBox2.Text = value.ToString(); }
        }

        public double InputC
        {
            get => TryFuncCheck(() => Convert.ToDouble(textBox3.Text));
            set { textBox3.Text = value.ToString(); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(SetA != null) SetA(this, EventArgs.Empty);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (SetB != null) SetB(this, EventArgs.Empty);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (SetC != null) SetC(this, EventArgs.Empty);
        }
    }
}
