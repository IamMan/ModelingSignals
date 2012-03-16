using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using WindowsFormsApplication1.Scrambling;
using Word = Microsoft.Office.Interop.Word;

namespace WindowsFormsApplication1
{
    class SuperMegaClass
    {
        private string Fio;
        private IList<CodingMethod> codingMethods;
        string studName;
        string initialMessege;
        string methodName;
        string methodDiagrame;
        string speed;
        string f0;
        string fn;
        string fb;
        string s;
        string fa;
        string scremblingProcess;
        string conclusion; 
        string mainConclusion;
        string c;
        
        Word.Application wordApp;
        Document mainDoc;
        int v;
        string initialBinStr;
        public SuperMegaClass()
        {
        }
        
        private string ToHex(char c) 
        {
            string ans = "";
            int num = 0;
            if (c < 'Р') {
                ans = "C";
                num = c - 'А';
            } else {
                ans = "D";
                num = c - 'Р';
            }
            if (num >= 0 && num <= 9)
            {
                ans += (char)('0' + num);
            }
            ans += (char)('A' + (num - 10));
            return ans;
        }
        private string FioToHex(string fio) 
        {
            string ans = "";
            foreach(char c in fio) {
                switch (c) {
                    case '.':
                        ans += "2E";
                        break;
                    default:
                        ans += ToHex(c);
                        break;
                }
            }
            Console.WriteLine(ans);
            return ans;
        }
        private string textType(string str)
        {
            string type = "";
            foreach (char c in str)
            {
                if (c == '0' || c == '1' && type != "hex")
                {
                    type = "bin";
                    continue;
                }
                if (('A' <= c && c <= 'F') || ('0' <= c && c <= '9'))
                {
                    type = "hex";
                }
                else
                {
                    return "bad";
                }
            }
            return type;
        }
        private string HexToBin(string hex)
        {
            string ans = "";
            foreach (char c in hex)
            {
                switch (c)
                {
                    case '0':
                        ans += "0000";
                        break;
                    case '1':
                        ans += "0001";
                        break;
                    case '2':
                        ans += "0010";
                        break;
                    case '3':
                        ans += "0011";
                        break;
                    case '4':
                        ans += "0100";
                        break;
                    case '5':
                        ans += "0101";
                        break;
                    case '6':
                        ans += "0110";
                        break;
                    case '7':
                        ans += "0111";
                        break;
                    case '8':
                        ans += "1000";
                        break;
                    case '9':
                        ans += "1001";
                        break;
                    case 'A':
                        ans += "1010";
                        break;
                    case 'B':
                        ans += "1011";
                        break;
                    case 'C':
                        ans += "1100";
                        break;
                    case 'D':
                        ans += "1101";
                        break;
                    case 'E':
                        ans += "1110";
                        break;
                    case 'F':
                        ans += "1111";
                        break;
                }
            }
            return ans;
        }
        private string BinToHex(string bin)
        {
            string tmp = "";
            string ans = "";
            for (int i = bin.Length - 1; i >= 0; i--)
            {
                tmp = bin[i] + tmp;
                if (tmp.Length == 4)
                {
                    ans = BinToChar(tmp) + ans;
                    tmp = "";
                }
            }
            while (tmp.Length < 4 && tmp.Length != 0)
            {
                tmp = '0' + tmp;
            }
            if (tmp.Length != 0) ans += BinToChar(tmp) + ans;
            return ans;
        }
        private char BinToChar(string bin)
        {
            Int32 num = 0;
            for (int i = 3; i >= 0; i--)
            {
                if (bin[i] == '1')
                {
                    num += (int)Math.Pow(2, 3 - i);
                }
            }
            if (num >= 0 && num <= 9)
            {
                return (char)('0' + num);
            }

            return (char)('A' + (num - 10));

        }
        private string B4B5(string src)
        {
            string ans = "";
            switch (src)
            {
                case "0000":
                    ans = "11110";
                    break;
                case "0001":
                    ans = "01001";
                    break;
                case "0010":
                    ans = "10100";
                    break;
                case "0011":
                    ans = "10101";
                    break;
                case "0100":
                    ans = "01010";
                    break;
                case "0101":
                    ans = "01011";
                    break;
                case "0110":
                    ans = "01110";
                    break;
                case "0111":
                    ans = "01111";
                    break;
                case "1000":
                    ans = "10010";
                    break;
                case "1001":
                    ans = "10011";
                    break;
                case "1010":
                    ans = "10110";
                    break;
                case "1011":
                    ans = "10111";
                    break;
                case "1100":
                    ans = "11010";
                    break;
                case "1101":
                    ans = "11011";
                    break;
                case "1110":
                    ans = "11100";
                    break;
                case "1111":
                    ans = "11101";
                    break;
            }
            return ans;
        }
        public string coding4B5B(string bin)
        {
            string tmp = "";
            string ans = "";
            for (int i = bin.Length - 1; i >= 0; i--)
            {
                tmp = bin[i] + tmp;
                if (tmp.Length == 4)
                {
                    ans = B4B5(tmp) + ans;
                    tmp = "";
                }
            }
            while (tmp.Length < 4 && tmp.Length != 0)
            {
                tmp = '0' + tmp;
            }
            if (tmp.Length != 0) ans += B4B5(tmp) + ans;
            return ans;
        }
        private int BytesLength(string bin, bool isHex = false)
        {
            if (isHex) 
            {
                return bin.Length * 8;
            }
            return bin.Length;
        }
        private string MessageLength(string bin, bool isHex = false)
        {
            if (isHex)
            {
                return String.Format("{0} байт({1} бит)", bin.Length, bin.Length * 8);
            }
            return String.Format("{0:0.00} байт({1} бит)", bin.Length / 8.0, bin.Length);
        }
        private string NameToFio(string Name)
        {
            String[] fios = Name.Split(' ');
            string ans = "";
            ans += fios[0][0] + ".";
            ans += fios[1][0] + ".";
            ans += fios[2][0] + ".";
            return ans;
        }

        private void initialise(int STEP, string docName)
        {
            codingMethods = new List<CodingMethod>();
            codingMethods.Add(new ManchesterMethod(STEP));
            codingMethods.Add(new AmiMethod(STEP));
            codingMethods.Add(new PotentialMethod(STEP));
            codingMethods.Add(new BipolarRZ(STEP));

            studName = "<<<StudName";
            initialMessege = "<<< InitialMessege";
            methodName = "<<<MethodName";
            methodDiagrame = "<<<MethodDiagram";
            speed = "<<<Speed";
            f0 = "<<<F0";
            fn = "<<<FN";
            fb = "<<<FB";
            s = "<<<S";
            fa = "<<<FA";
            scremblingProcess = "<<<ScremblingProcess";
            conclusion = "<<<Conclusion";
            mainConclusion = "<<<MainConclusion>>>";
            c = ">>>";
            
            wordApp = new Word.Application();
            mainDoc = MSWordFunctionsProvider.OpenDocument(wordApp, docName);
            if (mainDoc == null)
            {
                throw new Exception("Cannot open template");
            }
        }

        public void DoAllForMePleas(string Name, int step, string tempName) 
        {
            this.Fio = NameToFio(Name);
            initialise(step, tempName);
            MSWordFunctionsProvider.FindAndRepalce(wordApp, studName + c, Name);

            Random a = new Random();
            int[] methods = new int[4];
            methods[2] = a.Next(1, 3);
            methods[3] = methods[2];
            while (methods[2] == methods[3])
            {
                methods[3] = a.Next(1, 3);
            }
            methods[1] = 0;

            //----------------------Begin Coding --------------------------//
            BeginCoding(methods);
            //----------------------More Coding 4B5B --------------------------//
            MoreCoding(methods);
            //----------------------Scrambling --------------------------//
            ScramblingCoding(methods);
             
        }

        private void PutResults(string binString, int[] methods, string type)
        {
            for (int i = 1; i <= 3; i++)
            {
                MethodResult mr = codingMethods[methods[i]].drawSignal(binString, 0, 5, 85, v);
                mr.img.Save("D:\\image" + i + type + ".jpg");
                MSWordFunctionsProvider.FindAndRepalce(wordApp, methodName + i + type + c, codingMethods[methods[i]].getName());
                MSWordFunctionsProvider.InsertImageTo("D:\\image" + i + type +".jpg", mainDoc, methodDiagrame + i + type + c);
                MSWordFunctionsProvider.FindAndRepalce(wordApp, f0 + i + type + c, String.Format("{0:0.00}", mr.F0 / 1000000.0));
                MSWordFunctionsProvider.FindAndRepalce(wordApp, fn + i + type + c, String.Format("{0:0.00}", mr.Fn / 1000000.0));
                MSWordFunctionsProvider.FindAndRepalce(wordApp, fb + i + type + c, String.Format("{0:0.00}", mr.Fb / 1000000.0));
                MSWordFunctionsProvider.FindAndRepalce(wordApp, s + i + type + c, String.Format("{0:0.00}", mr.S / 1000000.0));
                MSWordFunctionsProvider.FindAndRepalce(wordApp, fa + i + type + c, String.Format("{0:0.00}", mr.Fa / 1000000.0));
            }
        }

        private void BeginCoding(int[] methods)
        {
            string hexString = FioToHex(Fio);
            string binString = HexToBin(hexString);
            initialBinStr = binString;
            if (new Random().Next(0,1) == 0) {
                v = 10000000;
            } else {
                v = 100000000;
            }
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + c, Fio);
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + "16" + c, hexString);
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + "2" + c, binString);
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + "L" + c, MessageLength(binString));

            PutResults(binString, methods, "");
        }
       
        private void MoreCoding(int[] methods)
        {
            string binString = coding4B5B(initialBinStr);
            string hexString = BinToHex(binString);
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + "1645" + c, hexString);
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + "245" + c, binString);
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + "L45" + c, MessageLength(binString));
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + "More" + c, String.Format("{0} бит", BytesLength(binString) - BytesLength(initialBinStr)));

            PutResults(binString, methods, "45");
        }

        private void ScramblingCoding(int[] methods)
        {
            IScrambl scram = new ExampleScrambl();
            MSWordFunctionsProvider.FindAndRepalce(wordApp, "<<<Polynomial>>>" + c, scram.getName());
            string log = "";
            string binString = scram.Function(initialBinStr, ref log);
            //MSWordFunctionsProvider.FindAndRepalce(wordApp, scremblingProcess + c, log);
            string hexString = BinToHex(binString);
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + "16P" + c, hexString);
            MSWordFunctionsProvider.FindAndRepalce(wordApp, initialMessege + "2P" + c, binString);

            PutResults(binString, methods, "P");
        }
    }
}
