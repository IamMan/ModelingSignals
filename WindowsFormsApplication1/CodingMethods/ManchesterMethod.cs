using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class ManchesterMethod : CodingMethod
    {
        public readonly string Name = "Манчестерский код";
        
        public ManchesterMethod(int step) : base(step)
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
            int term00 = 1;
            int term01 = 2;
            var nowTerm = -1;
            for (var i = 1; i < signal.Length; i++)
            {
                if ((signal[i] == '1' && signal[i - 1] == '0') || (signal[i] == '0' && signal[i - 1] == '1'))
                {
                    if (nowTerm != 1)
                    {
                        if (!terms.ContainsKey(term01))
                        {
                            terms.Add(term01, 0);
                        }
                        terms[term01]++;
                        nowTerm = 1;
                    }
                    continue;
                }
                if ((signal[i] == '0' && signal[i - 1] == '0') || (signal[i] == '1' && signal[i - 1] == '1'))
                {
                    if (nowTerm != 0)
                    {
                        if (!terms.ContainsKey(term00))
                        {
                            terms.Add(term00, 0);
                        }
                        terms[term00]++;
                        nowTerm = 0;
                    }
                    continue;
                }
            }
            double bit = 1.0 / c;
            double fa = terms.Sum(term => 1.0 / (term.Key * bit) * term.Value)/terms.Sum(term => term.Value);
            double f0 = 1.0 / terms.Keys.Min() / bit;
            double fn = 1.0 / terms.Keys.Max() / bit;
            double fb = 7.0 * f0;
            return new MethodResult(f0, fn, fb, fb - fn, fa);
        }

        protected override void drawImage(Image can, Graphics g, string signal, int x, int minLevel, int height)
        {
            var linePen = new Pen((Color.Black), 1);
            minLevel = can.Height - minLevel;
            int prevLevel = minLevel;
            int maxLevel = minLevel - height;
            g.Clear(Color.White);
            for (int i = 0; i < signal.Length; i++)
            {
                int beginX = x + i * STEP;
                if (signal[i] == '1')
                {
                    g.DrawLine(linePen, beginX, prevLevel, beginX, minLevel);
                    g.DrawLine(linePen, beginX, minLevel, beginX + STEP / 2, minLevel);
                    g.DrawLine(linePen, beginX + STEP / 2, minLevel, beginX + STEP / 2, maxLevel);
                    g.DrawLine(linePen, beginX + STEP / 2, maxLevel, beginX + STEP, maxLevel);
                    prevLevel = maxLevel;
                }
                if (signal[i] == '0')
                {
                    g.DrawLine(linePen, beginX, prevLevel, beginX, maxLevel);
                    g.DrawLine(linePen, beginX, maxLevel, beginX + STEP / 2, maxLevel);
                    g.DrawLine(linePen, beginX + STEP / 2, maxLevel, beginX + STEP / 2, minLevel);
                    g.DrawLine(linePen, beginX + STEP / 2, minLevel, beginX + STEP, minLevel);
                    prevLevel = minLevel;
                }
                drawLines(g, beginX, can.Height);
            }
        }
    }
}
