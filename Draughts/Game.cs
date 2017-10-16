using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Mime;

namespace Draughts
{
    public class Game
    {
        
        /*
         * Black = -1 & -2
         * White = 1 & 2
         * Empty cage = 0
         * 2 - king
         * 1 - draught
         * flag of turn true = white
         * flag of turn false = black
         */
        private const int draughtsSize = 8;
        
        private const char kingChar = '¤';
        private const char draughtChar = 'o';  
        private const ConsoleColor blackColor = ConsoleColor.Red;
        private const ConsoleColor whiteColor = ConsoleColor.White;
        private const ConsoleColor whiteCage = ConsoleColor.DarkGray;
        private const ConsoleColor highlightCage = ConsoleColor.DarkYellow;
        
        private List<List<int>> a;
        private bool turnFlag;
        
        public Game()
        {
            a = new List<List<int>>();
            turnFlag = false;
            for (int i = 0; i < draughtsSize; i++)
            {
                a.Add(new List<int>());
                for (int j = 0; j < draughtsSize; j++)
                    a[i].Add(0);
            }
            setDefault();
        }

        // ------- Print block -------
        
        void printInfoAboutTurn()
        {
            Console.Write("    Ходящий цвет: ");
            Console.BackgroundColor = turnFlag ? whiteColor : blackColor;
            Console.Write("   ");
            Console.ResetColor();
        }

        public void print()
        {
            char top = 'A';
            int left = 8;
            
            Console.Clear();
            
            //numbers in top
            for (int j = 0; j < draughtsSize + 1; j++)
                if (j == 0) 
                    Console.Write("  ");
                else
                {
                    Console.Write(" " + top + " ");
                    top++;
                }
            Console.WriteLine();

            //playing area
            for (int i = 0; i < draughtsSize; i++){
                Console.Write(left + " ");
                left--;
                for (int j = 0; j < draughtsSize; j++)
                {
                    if((i + j) % 2 == 0)
                        Console.BackgroundColor = whiteCage;
                    Console.Write( " " + intToChar(a[i][j]) + " "); 
                    Console.ResetColor();
                }
                if (i == 2)
                    printInfoAboutTurn();
                Console.WriteLine(); 
            }
            return;
        }

        private void print(List<Position> fill)
        {
            char top = 'A';
            int left = 8;
            
            Console.Clear();
            
            //numbers in top
            for (int j = 0; j < draughtsSize + 1; j++)
                if (j == 0) 
                    Console.Write("  ");
                else
                {
                    Console.Write(" " + top + " ");
                    top++;
                }
            Console.WriteLine();

            //playing area
            for (int i = 0; i < draughtsSize; i++){
                Console.Write(left + " ");
                left--;
                for (int j = 0; j < draughtsSize; j++)
                {
                    if((i + j) % 2 == 0)
                        Console.BackgroundColor = whiteCage;
                    
                    fill.Where(g => g.v == i && g.h == j).ToList().ForEach(delegate(Position p)
                    {
                        Console.BackgroundColor = highlightCage;
                    });
                    Console.Write( " " + intToChar(a[i][j]) + " "); 
                    Console.ResetColor();
                }
                if (i == 2)
                    printInfoAboutTurn();
                Console.WriteLine(); 
            }
            return;
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

        //------- Specific block -------
        
        private int charToIndex(char c) //A,B,C,D,E,F,G,H
        {
            return c - 'A';
        }

        private int charIntToIndex(char c) //1,2,3,4,5,6,7,8
        {
            int r = c - '0' - 1;
            return 7 - r;
        }

        public bool isCorrectCage(string s)
        {
            return (s.Length == 2 && charToIndex(s[0]) < 8 && charToIndex(s[0]) >= 0 
                    && charIntToIndex(s[1]) >= 0 && charIntToIndex(s[1]) < 8) ? true : false;
        }

        private bool isCorrectCage(Position p)
        {
            return (p.v < 8 && p.v >= 0 && p.h < 8 && p.h >= 0) ? true : false;
        }
        
        //------- In game start -------

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
        
        //------- Helpful functions -------

        private struct Position
        {
            public int h; //horisontal pos
            public int v; //vertical pos
        }

        private Position setToPositionStructType(string stringPosition)
        {
            Position p = new Position();
            p.h = charToIndex(stringPosition[0]);
            p.v = charIntToIndex(stringPosition[1]);
            return p;
        }

        private int posToInt(Position p)
        {
            return a[p.v][p.h];
        }

        private void move(Position from, Position to)
        {
            a[to.v][to.h] = posToInt(from);
            a[from.v][from.h] = 0;
            return;
        }
        
        private bool isDiagonal(Position from, Position to)
        {
            return Math.Abs(from.v - to.v) == Math.Abs(from.h - to.h) ? true : false;
        }

        private void makeDraughtToKing()
        {
            for (int i = 0; i < 8; i++)
            {
                if (a[0][i] == 1)
                    a[0][i] = 2;
                if (a[7][i] == -1)
                    a[7][i] = -2;
            }
            return;
        }

        private bool canTurn()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Position p;
                    p.v = i;
                    p.h = j;
                    if (a[p.v][p.h] != 0 && !isYourTurn(p)) //can play next player
                        return true;
                }
            }
            return false;
        }
        
        //------- All with draught -------

        private bool canDraughtEatOneMove(Position from, Position to)
        {
            if (posToInt(from) == 0 || posToInt(to) != 0)
                return false;
            Position temp = new Position(); //position between "from" and "to"
            temp.v = from.v + (to.v - from.v) / 2;
            temp.h = from.h + (to.h - from.h) / 2;
            if (!isYourTurn(temp) && a[temp.v][temp.h] != 0)
                return true;
            return false;
        }

        private Position generateTurnInCombo(Position p, int option)
        {
            switch (option)
            {
                case 0:
                    p.v += 2;
                    p.h += 2;
                    break;
                case 1:
                    p.v -= 2;
                    p.h += 2;
                    break;
                case 2:
                    p.v += 2;
                    p.h -= 2;
                    break;
                case 3:
                    p.v -= 2;
                    p.h -= 2;
                    break;
            }
            return p;
        }

        private bool draughtEat(Position from, Position to)
        {
            if (canDraughtEatOneMove(from, to))
            {
                move(from, to);
                a[from.v + (to.v - from.v) / 2][from.h + (to.h - from.h) / 2] = 0; //eat draught
            }
            else
                return false;
            
            from = to;
            bool canIMove = false;
                
            for (int i = 0; i < 4; i++) //Check all turns
                if (isCorrectCage(generateTurnInCombo(from, i))
                    && canDraughtEatOneMove(from, generateTurnInCombo(from, i)))
                {
                    canIMove = true;
                    break;
                }     

            if (!canIMove)
                return true;
            else
            {
                while (true)
                {
                    print();
                    Console.Write("Продолжите комбо: ");
                    string s = Console.ReadLine();
                    Position fromP = setToPositionStructType(s.Substring(0, 2));
                    Position toP = setToPositionStructType(s.Substring(3, 2));

                    if (!isCorrectCage(fromP) || !isCorrectCage(toP) || fromP.v != from.v || fromP.h != from.h
                        || posToInt(fromP) == 0 || posToInt(toP) != 0 || !isYourTurn(fromP))
                        //if the moves incorrectly or different combos checker that was introduced in the console
                        //or the coordinates of the move point to the impossible cells
                        continue;
                    else
                    {
                        draughtEat(fromP, toP); //start the recursion moves
                        return true;
                    }

                }
                    
            }
        }

        private bool draughtTurn(Position from, Position to)
        {
            if (Math.Abs(from.h - to.h) == 1 && Math.Abs(to.v - from.v) == 1) //simple turn
                move(from, to);
            else if (Math.Abs(from.h - to.h) == 2 && Math.Abs(to.v - from.v) == 2) //draught eat
            {
                draughtEat(from, to);
            }
            else
                return false;
            return true;
        }
        
        //------- All with king -------

        private void generateVector(int i, out int v, out int h)
        {
            v = -1;
            h = -1;

            switch (i)
            {
                case 0:
                    v = 1;
                    h = 1;
                    break;
                case 1:
                    v = 1;
                    h = -1;
                    break;
                case 2:
                    v = -1;
                    h = -1;
                    break;
                case 3:
                    v = -1;
                    h = 1;
                    break;
            }
            return;
        }
        
        private bool canTurnInKingCombo(Position from)
        {
            for (int i = 0; i < 4; i++) //Check all turns
            {
                Position newPosition = from;
                int v, h;
                generateVector(i, out v, out h);
                while (true)
                {
                    newPosition.v += v;
                    newPosition.h += h;
                    if (isCorrectCage(newPosition))
                    {
                        if(posToInt(newPosition) == 0)
                            continue;
                        
                        if (isYourTurn(newPosition))
                            break; //friendly figure
                        else
                        {
                            newPosition.v += v;
                            newPosition.h += h;
                            if (isCorrectCage(newPosition) && posToInt(newPosition) == 0)
                                return true; //if I can eating figure
                        }
                    }
                    else
                        break;
                }
            }
            return false;
        }
        
        private Position kingEat(Position from, Position to)
        {
            Position res, fromCopy = from, badTurn;
            res.v = -1; 
            res.h = -1;
            badTurn.h = -1;
            badTurn.v = -1;
            do
            {
                fromCopy.v += (to.v - from.v) / Math.Abs(to.v - from.v); //figure moves down or up
                fromCopy.h += (to.h - from.h) / Math.Abs(to.h - from.h);

                if (posToInt(fromCopy) != 0)
                {
                    if (!isYourTurn(fromCopy))
                    {
                        if (res.v == -1)
                            res = fromCopy;
                        else
                            return badTurn; //two or many figures between
                    }
                    else
                        return badTurn; //friendly figure between
                }
            } while (fromCopy.v != to.v);

            if (res.v == -1) //if it's simple move
                return badTurn;

            move(from, to); //eating
            a[res.v][res.h] = 0; //eating
            from = to;
            
            if (!canTurnInKingCombo(from))
                return res;
            else
            {
                while (true)
                {
                    print();
                    Console.Write("Продолжите комбо: ");
                    string s = Console.ReadLine();
                    Position fromP = setToPositionStructType(s.Substring(0, 2));
                    Position toP = setToPositionStructType(s.Substring(3, 2));

                    if (!isCorrectCage(fromP) || !isCorrectCage(toP) || fromP.v != from.v || fromP.h != from.h
                        || posToInt(fromP) == 0 || posToInt(toP) != 0 || !isYourTurn(fromP))
                        //if the moves incorrectly or different combos checker that was introduced in the console
                        //or the coordinates of the move point to the impossible cells
                        continue;
                    else
                    {
                        return kingEat(fromP, toP); //start the recursion moves
                    }
                }
            }
        }

        private bool simpleMove(Position from, Position to)
        {
            Position fromCopy = from;
            do
            {
                fromCopy.v += (to.v - from.v) / Math.Abs(to.v - from.v); //figure moves down or up
                fromCopy.h += (to.h - from.h) / Math.Abs(to.h - from.h);
                if (posToInt(fromCopy) != 0)
                    return false;
            } while (fromCopy.v != to.v);
            return true;
        }
        
        private bool kingTurn(Position from, Position to)
        {
            if (!isDiagonal(from, to))
                return false;
            
            if (kingEat(from, to).v == -1)
            {
                if (simpleMove(from, to))
                    move(from, to);
                else
                    return false;
            }
            return true;
        }

        //------- Turns -------

        private bool isYourTurn(Position p)
        {
            return (posToInt(p) > 0) == turnFlag ? true : false;
        }
        
        public bool turn(string fromStr, string toStr)
        {
            if (!isCorrectCage(fromStr) || !isCorrectCage(toStr))
                return false;

            Position from = setToPositionStructType(fromStr);
            Position to = setToPositionStructType(toStr);
            
            if (posToInt(from) == 0 || posToInt(to) != 0 || !isYourTurn(from))
                return false;
            
            //rules for draughts
            if (Math.Abs(posToInt(from)) == 1)
                if (!draughtTurn(from, to))
                    return false;
            
            makeDraughtToKing();
            
            //rules for kings
            if (Math.Abs(posToInt(from)) == 2)
                if (!kingTurn(from, to))
                    return false;

            if (!canTurn())
            {
                Console.WriteLine("Конец игры!");
                Console.ReadKey();
                System.Environment.Exit(0);
            }
            
            turnFlag = !turnFlag; //next player turn
            return true;
        }
    }
}