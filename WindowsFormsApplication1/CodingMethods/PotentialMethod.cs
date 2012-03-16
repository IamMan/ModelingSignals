using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class PotentialMethod : CodingMethod
    {
        public readonly string Name = "Потенциальный код  (без возврата к нулю-NRZ)";

        public PotentialMethod(int step)
            : base(step)
        {
        }
        public override string getName()
        {
            return Name;
        }
        public override MethodResult calcSignal(string signal, int c)
        {
            if (signal.Length == 1)
            {
                return new MethodResult(0, 0, 0, 0, 0);
            }
            IDictionary<int, int> terms = new SortedDictionary<int, int>();
            int term00 = 0;
            int term11 = 2;
            var nowTerm = -1;
            for (var i = 0; i < signal.Length; i++)
            {
                if (signal[i] == '0')
                {
                    if (nowTerm != 0)
                    {
                        if (term00 > 0)
                        {
                            term11 *= 2;
                            if (term11 > 0 && !terms.ContainsKey(term11))
                            {
                                terms.Add(term11, 0);
                            }
                            terms[term11]++;
                        }
                        term00 = 0;
                        nowTerm = 0;
                    }
                    term00++;
                    continue;
                }
                if (signal[i] == '1')
                {
                    if (nowTerm != 3)
                    {
                        if (term00 > 0)
                        {
                            term00 *= 2;
                            if (!terms.ContainsKey(term00))
                            {
                                terms.Add(term00, 0);
                            }
                            terms[term00]++;
                        }
                        term00 = 0;
                        nowTerm = 3;
                    }
                    term11++;
                }
            }
            double bit = 1.0 / c ;
            double fa = terms.Sum(term => 1.0 / (term.Key * bit) * term.Value) / terms.Sum(term => term.Value);
            double f0 = 1.0 / terms.Keys.Min() / bit;
            double fn = 1.0 / terms.Keys.Max() / bit;
            double fb = 7.0 * f0;
            return new MethodResult(f0, fn, fb, fb - fn, fa);
        }

        protected override void drawImage(Image can, Graphics g, string signal, int x, int minLevel, int height)
        {
            var linePen = new Pen((Color.Black), 1);
            minLevel = can.Height - minLevel;
            int maxLevel = minLevel - height;
            int midleLevel = minLevel - height / 2;
            int prevLevel = midleLevel;
            g.Clear(Color.White);
            for (int i = 0; i < signal.Length; i++)
            {
                int beginX = x + i * STEP;
                if (signal[i] == '0')
                {
                    g.DrawLine(linePen, beginX, prevLevel, beginX, midleLevel);
                    g.DrawLine(linePen, beginX, midleLevel, beginX + STEP, midleLevel);
                    prevLevel = midleLevel;

                }
                if (signal[i] == '1')
                {
                    g.DrawLine(linePen, beginX, prevLevel, beginX, maxLevel);
                    g.DrawLine(linePen, beginX, maxLevel, beginX + STEP, maxLevel);
                    prevLevel = maxLevel;
                }
                drawLines(g, beginX, height);
            }
        }
    }
}
