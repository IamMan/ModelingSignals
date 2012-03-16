using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1.Scrambling
{
    class ExampleScrambl:IScrambl
    {
        private readonly string Name = "AioBi-3oBi-5";
        public string getName() 
        {
            return Name;
        }
        public ExampleScrambl():base(){
        }
        
        public string Function(string binStr, ref string log)
        {
            string ans = "";
            for (int i = 0; i < binStr.Length; i++)
            {
                if (i < 3) {
                    ans += binStr[i];
                    Console.WriteLine(String.Format("B[{0}] = A[{0}] = {1}", i, ans[i]));
                    if (log != null)
                    {
                        log += String.Format("B[{0}] = A[{0}] = {1}\n", i, ans[i]);
                    }
                    continue;
                }
                if (i >= 3 && i < 5)
                {
                    ans += ((binStr[i] - '0') ^ (ans[i - 3] - '0'));
                    Console.WriteLine(String.Format("B[{0}] = A[{0}] ^ B[{1}] = {2}", i, i - 3, ans[i]));
                    if (log != null)
                    {
                        log += String.Format("B[{0}] = A[{0}] ^ B[{1}] = {2}\n", i, i - 3, ans[i]);
                    }
                    continue;
                }
                ans += ((binStr[i] - '0') ^ (ans[i - 3] - '0') ^ (ans[i - 5] - '0'));
                Console.WriteLine(String.Format("B[{0}] = A[{0}] ^ B[{1}] ^ B[{2}] = {3}", i, i - 3, i - 5, ans[i]));
                if (log != null)
                {
                    log += String.Format("B[{0}] = A[{0}] ^ B[{1}] ^ B[{2}] = {3}\n", i, i - 3, i - 5, ans[i]);
                }
            }
            return ans.ToString();
        }
    }
}
