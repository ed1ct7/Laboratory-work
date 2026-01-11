using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Triangle
{
    public interface IView
    {
        string Data { get; set; }
        double InputA { get; set; }
        double InputB { get; set; }
        double InputC { get; set; }

        event EventHandler<EventArgs> SetA;
        event EventHandler<EventArgs> SetB;
        event EventHandler<EventArgs> SetC;
    }
}
