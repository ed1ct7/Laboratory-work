using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle_and_Matrix_classes.Interfaces
{
    public interface Matrix
    {
        void Draw();
    }
    public interface IGeometrical
    {
        void GetPerimeter();
        void GetArea();
    }
}
