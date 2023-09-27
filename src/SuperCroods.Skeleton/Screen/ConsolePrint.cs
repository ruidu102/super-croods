using System;

namespace SuperCroods.Skeleton.Screen
{
    public static class ConsolePrint
    {
        public static void Print(object obj)
        {
            Console.WriteLine(obj);
        }

        public static void Print(object obj, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(obj);
        }

        public static void Print()
        {
            Console.WriteLine();
        }

        public static void PrintNB(object obj)
        {
            Console.Write(obj);
        }
    }
}
