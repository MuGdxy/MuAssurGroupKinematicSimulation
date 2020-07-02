using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAGKS
{
    public abstract class Bar
    {
        public abstract I_Output GetOutput(double t);
    }
}
