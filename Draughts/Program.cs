using System;
using System.Collections.Generic;

namespace Draughts
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Draughts d = new Draughts();
            d.print();
            while (true)
            {
                Console.Write("Ход: ");
                string s = Console.ReadLine();
                d.turn(s.Substring(0, 2), s.Substring(3, 2));
                Console.Clear();
                d.print();
            }
            
        }
    }
}