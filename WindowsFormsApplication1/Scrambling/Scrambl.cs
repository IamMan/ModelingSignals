using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1.Scrambling
{
    interface IScrambl
    {
        string Function(string binStr, ref string log);
        string getName();
    }
}
