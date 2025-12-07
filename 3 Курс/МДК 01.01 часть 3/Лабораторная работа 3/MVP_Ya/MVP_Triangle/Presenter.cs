using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Triangle
{
    public class Presenter
    {
        private Triangle _triangle = new Triangle();
        private IView _view;

        public Presenter(IView view)
        { 
            _view = view;
            _view.SetA += new EventHandler<EventArgs>(OnSetA);
            _view.SetB += new EventHandler<EventArgs>(OnSetB);
            _view.SetC += new EventHandler<EventArgs>(OnSetC);
            RefreshView();
        }

        public void OnSetA(object sender, EventArgs e)
        {
            _triangle.Sides.A=_view.InputA;
            RefreshView();
        }
        public void OnSetB(object sender, EventArgs e)
        {
            _triangle.Sides.B = _view.InputB;
            RefreshView();
        }
        public void OnSetC(object sender, EventArgs e)
        {
            _triangle.Sides.C = _view.InputC;
            RefreshView();
        }

        public void RefreshView()
        {
            _view.Data = _triangle.ToString();
        }
    }
}
