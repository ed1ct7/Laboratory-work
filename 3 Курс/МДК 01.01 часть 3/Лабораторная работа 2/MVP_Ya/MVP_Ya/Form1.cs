using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MVP_Ya
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
        public string Sq
        {
            get => label1.Text;
            set { label1.Text = value; }
        }

        public double InputA
        {
            get => Convert.ToDouble(textBox1.Text);
            set { textBox1.Text = value.ToString(); }
        }

        public double InputB
        {
            get => Convert.ToDouble(textBox2.Text);
            set { textBox2.Text = value.ToString(); }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (SetB != null) SetB(this, EventArgs.Empty);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (SetA != null) SetA(this, EventArgs.Empty);
        }
    }

}
