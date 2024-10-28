using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrX.ConsoleHelper.Data
{
    internal static class Static
    {
        private static int Wight_ { get; set; } = -1;
        public static int Wight
        {
            get
            {
                if (Wight_ > 0)
                    return Wight_;
                else 
                    return Console.WindowWidth;
            }
            set { Wight_ = value; }
        }
        public static int Hight_ { get; set; } = -1;

        public static int Hight
        {
            get
            {
                if (Hight_ > 0)
                    return Hight_;
                else
                    return Console.WindowHeight;
            }
            set { Hight_ = value; }
        } 
        public static InputData Lable { get; set; }
    }
}
