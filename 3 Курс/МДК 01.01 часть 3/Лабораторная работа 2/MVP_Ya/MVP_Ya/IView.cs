using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Ya
{
    public interface IView
    {
        string Sq { get; set; }
        double InputA { get; set; }
        double InputB { get; set; }

        event EventHandler<EventArgs> SetA;
        event EventHandler<EventArgs> SetB;
    }
}
