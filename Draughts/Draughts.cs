using System;
using System.Collections.Generic;

namespace Draughts
{
    public class Draughts
    {
        
        /*
         * Черные = -1 и -2
         * Белые = 1 и 2
         * Пустая клетка = 0
         * 2 - дамки
         * 1 - пешки
         */
        private const int draughtsSize = 8;
        
        private const char kingChar = '¤';
        private const char draughtChar = 'o';  
        private const ConsoleColor blackColor = ConsoleColor.Red;
        
        private List<List<int>> a;
        private bool turnFlag;
        
        public Draughts()
        {
            a = new List<List<int>>();
            turnFlag = true;
            for (int i = 0; i < draughtsSize; i++)
            {
                a.Add(new List<int>());
                for (int j = 0; j < draughtsSize; j++)
                    a[i].Add(0);
            }
            setDefault();
        }

        public void print()
        {
            char top = 'A';
            int left = 8;
            
            for (int j = 0; j < draughtsSize + 1; j++)
                if (j == 0) 
                    Console.Write("  ");
                else
                {
                    Console.Write(" " + top + " ");
                    top++;
                }
            Console.WriteLine();

            for (int i = 0; i < draughtsSize; i++){
                Console.Write(left + " ");
                left--;
                for (int j = 0; j < draughtsSize; j++)
                {
                    if((i + j) % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write( " " + intToChar(a[i][j]) + " "); 
                    Console.ResetColor();
                }
                Console.WriteLine(); 
            }
        }

        private char intToChar(int number)
        {
            switch (number)
            {
                case 1: return draughtChar;
                case -1: Console.ForegroundColor = blackColor; return draughtChar;
                case 2: return kingChar;
                case -2: Console.ForegroundColor = blackColor; return kingChar;
                default: return ' ';
            }
        }

        private int charToIndex(char c) //A,B,C,D,E,F,G,H
        {
            int r = c - 'A';
            return r;
        }

        private int charIntToIndex(char c) //1,2,3,4,5,6,7,8
        {
            int r = c - '0' - 1;
            return 7 - r; //because array begin in zero
        }

        public bool isCorrectTurn(string s)
        {
            if (s.Length == 2 && charToIndex(s[0]) < 8 && charToIndex(s[0]) >= 0
                && charIntToIndex(s[1]) >= 0 && charIntToIndex(s[1]) < 8)
                return true;
            else
                return false;
        }

        private void setDefault()
        {
            for (int i = 0; i < draughtsSize; i++)
                for (int j = 0; j < draughtsSize; j++)
                    if ((i + j) % 2 != 0)
                    {
                        switch (i)
                        {
                           case 0:
                           case 1:
                           case 2:
                               a[i][j] = -1;
                               break;
                           case 5:
                           case 6:
                           case 7:
                               a[i][j] = 1;
                               break;
                           default:
                               a[i][j] = 0;
                               break;
                        }
                    }
        }

        public bool turn(string from, string to)
        {
            if (!isCorrectTurn(from) || !isCorrectTurn(to))
                return false;

            int i0 = charIntToIndex(from[1]);
            int j0 = charToIndex(from[0]);
            int i1 = charIntToIndex(to[1]);
            int j1 = charToIndex(to[0]);
            
            a[i1][j1] = a[i0][j0];
            a[i0][j0] = 0;

            return true;
        }
    }
}