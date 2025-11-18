using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Ya
{
    public class Presenter
    {
        private Model _model = new Model();
        private IView _view;

        public Presenter(IView view)
        {
            _view = view;
            _view.SetA += new EventHandler<EventArgs>(OnSetA);
            _view.SetB += new EventHandler<EventArgs>(OnSetB);
            RefreshView();
        }

        public void OnSetA(object sender, EventArgs e)
        {
            _model.A = _view.InputA;
            RefreshView();
        }

        public void OnSetB(object sender, EventArgs e)
        {
            _model.B = _view.InputB;
            RefreshView();
        }

        public void RefreshView()
        {
            _view.Sq = _model.square().ToString();
        }
    }
}
