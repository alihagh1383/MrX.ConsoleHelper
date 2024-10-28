using MrX.ConsoleHelper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrX.ConsoleHelper
{
    public class StartPoint
    {
        public bool SetLable(InputData Lable)
        {
            Static.Lable = Lable;
            return true;
        }
        /// <summary>
        /// Value -1 Equal Auto
        /// </summary>
        public bool SetSize(int wight = -1, int Hight = -1)
        {
            Static.Hight = Hight;
            Static.Wight = wight;
            return true;
        }
        public void Init()
        {
            Console.Clear();
        }
        public void Print(string Data, int? X = null, int? Y = null, ConsoleColor? ForegroundColor = null, ConsoleColor? BackgroundColor = null, bool Center = true, bool Whit = false)
        {
            Console.CursorLeft = X ?? Console.CursorLeft;
            Console.CursorTop = Y ?? Console.CursorTop;
            Console.ForegroundColor = ForegroundColor ?? Console.ForegroundColor;
            Console.BackgroundColor = BackgroundColor ?? Console.BackgroundColor;
            string Pading = string.Empty;
            if (Center)
            {
                int len = Console.WindowWidth - Data.Length;
                Pading = string.Join(" ", new string[((int)(len / 2))]);
            }
            Console.WriteLine(Pading + Data + Pading);
            Console.ResetColor();
            if (Whit)
                Console.ReadKey();
        }
        public string SelectString(string Message, int? Hight = null)
        {
            if (Hight == null)
                Hight = (int)((long)Static.Hight - 4);
            Print(Message, 0, Hight);
            string inp = Console.ReadLine() ?? "";
            Print(string.Join(" ", new string[((int)(Message.Length))]), 0, Hight);
            Print(string.Join(" ", new string[((int)(inp.Length + 1))]), 0, Hight + 1, Center: false);
            return inp;
        }
        public bool SelectBool(string Message, int? Hight = null)
        {
            if (Hight == null)
                Hight = (int)((long)Static.Hight);
            if (SelectDic(Message, new Dictionary<int, string>() { { 0, "False" }, { 1, "True" } }) == 1)
            {
                return true;
            }
            return false;
        }
        public int SelectDic(string Message, Dictionary<int, string> data, int? Hight = null)
        {
            if (Hight == null)
                Hight = (int)((long)Static.Hight);
            var count = data.Count - 1;
            bool ShowArrow = false;
            bool ShowEnd = true;
            bool ShowStart = false;
            int MaxLen = 6;
            foreach (var item in data)
                if (item.Value.Length > MaxLen)
                    MaxLen = item.Value.Length;
            if (Message.Length > MaxLen)
                MaxLen = Message.Length;
            if (count > 2)
                ShowArrow = true;
            int select = 0;
            var dataList = data.ToList();
            int baseh = -4;
            if (dataList.Count == 2)
                baseh = -3;
            if (dataList.Count == 1)
                baseh = -2;
            while (true)
            {

                int h = baseh;
                Print(Message, 0, Hight - 2 + h, ConsoleColor.White, ConsoleColor.Black);
                int start = 0, end = 0;
                {

                    if (select == count)
                    {
                        ShowEnd = false;
                        ShowStart = true;
                        start = ((select - 2) >= 0) ? (select - 2) : 0;
                        end = (((select) <= count) ? select : count);
                    }
                    else if (select == 0)
                    {
                        ShowEnd = true;
                        ShowStart = false;
                        start = 0;
                        end = (((select + 1) <= count) ? (((select + 2) <= count) ? select + 2 : select + 1) : count);
                    }
                    else
                    {
                        ShowStart = ShowEnd = true;
                        start = ((select - 1) >= 0) ? select - 1 : 0;
                        end = (((select + 1) <= count) ? select + 1 : count);
                    }
                }
                if (ShowArrow)
                {
                    h--;
                    if (ShowStart)

                        Print(@"/\/\/\", 0, Hight + (h++), ConsoleColor.White, ConsoleColor.Black);
                    else
                        Print("      ", 0, Hight + (h++), ConsoleColor.White, ConsoleColor.Black);
                }
                for (int i = start; i <= end; i++)
                {
                    Print(
                        dataList[i].Value.ToString(),
                        0,
                        Hight + (h++),
                        (i == select) ? ConsoleColor.Black : ConsoleColor.White,
                        (i == select) ? ConsoleColor.White : ConsoleColor.Black
                        );
                }
                if (ShowArrow)
                {
                    if (ShowEnd)
                        Print(@"\/\/\/", 0, Hight + (h++), ConsoleColor.White, ConsoleColor.Black);
                    else
                        Print("      ", 0, Hight + (h++), ConsoleColor.White, ConsoleColor.Black);
                }
                var Key = Console.ReadKey();
                switch (Key.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            select = ((select + 1 > count) ? count : select + 1);
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            select = ((select - 1 < 0) ? 0 : select - 1);
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            string Remove = string.Join(" ", new string[((int)(MaxLen))]);
                            Print(Remove, 0, Hight - 6);
                            Print(Remove, 0, Hight - 5);
                            Print(Remove, 0, Hight - 4);
                            Print(Remove, 0, Hight - 3);
                            Print(Remove, 0, Hight - 2);
                            Print(Remove, 0, Hight - 1);
                            return dataList[select].Key;
                        }
                }
            }
        }
    }
}
