using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication1
{
    public class MethodResult
    {
        private readonly double _f0; //main garmonic f0 = 1 / Tmin
        private readonly double _fn; //down bound freq fn = 1 / Tmax
        private readonly double _fb; //up bound freq fb = 7 * f0
        private readonly double _s; //spectr s = fb - fn
        private readonly double _fa; //average freq
        public Bitmap img;

        public double F0
        {
            get { return _f0; }
        }

        public double Fn
        {
            get { return _fn; }
        }

        public double Fb
        {
            get { return _fb; }
        }

        public double S
        {
            get { return _s; }
        }

        public double Fa
        {
            get { return _fa; }
        }

        public MethodResult(double f0, double fn, double fb, double s, double fa)
        {
            this._fb = fb;
            this._fn = fn;
            this._f0 = f0;
            this._s = s;
            this._fa = fa;
        }
    }
}
