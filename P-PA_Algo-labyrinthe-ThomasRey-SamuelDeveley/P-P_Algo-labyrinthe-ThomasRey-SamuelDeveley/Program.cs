using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace P_P_Algo_labyrinthe_ThomasRey_SamuelDeveley
{
    class Program
    {
        const int LABYRINTHELENGTHX = 100,
                 LABYRINTHELENGTHY = 100;
        static Random r = new Random();
        static Box[,] tabBox = new Box[LABYRINTHELENGTHX, LABYRINTHELENGTHY];

        static Stack stackBox = new Stack(LABYRINTHELENGTHX * LABYRINTHELENGTHY + 1);
        static Stack stackSoluce = new Stack(LABYRINTHELENGTHX * LABYRINTHELENGTHY + 1);

        static bool isFirstLoop = true;

        //static int[,] test = { {1, 2, 3},
        //                       {4, 5, 6},
        //                       {7, 8, 9} }; 
        static void Main(string[] args)
        {
            for (int y = 0; y < LABYRINTHELENGTHY; y++)
            {
                for (int x = 0; x < LABYRINTHELENGTHX; x++)
                {
                    Box box = new Box(x + "," + y, x, y);
                    tabBox[x, y] = box;

                    if (x == LABYRINTHELENGTHX - 1 && y == LABYRINTHELENGTHY - 1)
                    {
                        box.South = false;
                    }
                }
            }

            CreateLabyrinthe(tabBox[0, 0], "South");
        }

        public static void CreateLabyrinthe(Box currentBox, string cardinalPoint)
        { 
            List<Box> listBoxTemp = new List<Box>();

            if (!stackBox.IsEmpty() || isFirstLoop)
            {
                isFirstLoop = false;

                //ouvrir le mur de côté opposé à la lastBox (même mur ouvert les 2 box (current et last))
                switch (cardinalPoint)
                {
                    case"North":
                        currentBox.South = false;
                        break;

                    case "East":
                        currentBox.West = false;
                        break;

                    case "South":
                        currentBox.North = false;
                        break;

                    case "West":
                        currentBox.East = false;
                        break;
                    case "":
                        break;
                }


                //check la/les prochaine case libre à partir de cette position et ajout des cases libre
                if (currentBox.LocationX + 1 < LABYRINTHELENGTHX && !tabBox[currentBox.LocationX + 1, currentBox.LocationY].IsChecked)
                {
                    listBoxTemp.Add(tabBox[currentBox.LocationX + 1, currentBox.LocationY]);
                }

                if (currentBox.LocationX - 1 >= 0 && !tabBox[currentBox.LocationX - 1, currentBox.LocationY].IsChecked)
                {
                    listBoxTemp.Add(tabBox[currentBox.LocationX - 1, currentBox.LocationY]);
                }

                if (currentBox.LocationY - 1 >= 0 && !tabBox[currentBox.LocationX, currentBox.LocationY - 1].IsChecked)
                {
                    listBoxTemp.Add(tabBox[currentBox.LocationX, currentBox.LocationY - 1]);
                }

                if (currentBox.LocationY + 1 < LABYRINTHELENGTHY && !tabBox[currentBox.LocationX, currentBox.LocationY + 1].IsChecked)
                {
                    listBoxTemp.Add(tabBox[currentBox.LocationX, currentBox.LocationY + 1]);
                }
                

                if (!currentBox.IsChecked)
                {
                    stackBox.Push(currentBox);
                    stackSoluce.Push(currentBox);

                    currentBox.IsChecked = true;
                }
                

                if (listBoxTemp.Count != 0)
                {
                    //si il y a une/des cases libre alors tirer un random entre toutes ces cases
                    int randomResult = r.Next(0, listBoxTemp.Count);

                    //Ouvrir le mur en direction de la case libre et relancer la méthode avec la prochaine case en argument et le prochain côté à ouvrir
                    if (listBoxTemp[randomResult].LocationX == currentBox.LocationX && listBoxTemp[randomResult].LocationY == currentBox.LocationY + 1)
                    {
                        //next au sud
                        currentBox.South = false;
                        CreateLabyrinthe(listBoxTemp[randomResult], "South");
                    }
                    else if (listBoxTemp[randomResult].LocationX == currentBox.LocationX && listBoxTemp[randomResult].LocationY == currentBox.LocationY - 1)
                    {
                        //next au nord
                        currentBox.North = false;
                        CreateLabyrinthe(listBoxTemp[randomResult], "North");
                    }
                    else if (listBoxTemp[randomResult].LocationX == currentBox.LocationX + 1 && listBoxTemp[randomResult].LocationY == currentBox.LocationY)
                    {
                        //next a est
                        currentBox.East = false;
                        CreateLabyrinthe(listBoxTemp[randomResult], "East");
                    }
                    else if (listBoxTemp[randomResult].LocationX == currentBox.LocationX - 1 && listBoxTemp[randomResult].LocationY == currentBox.LocationY)
                    {
                        //next a ouest
                        currentBox.West = false;
                        CreateLabyrinthe(listBoxTemp[randomResult], "West");
                    }  
                }
                else
                {
                    //si il y a pas de case libre alors dépiler si il n'y a pas de case de libre
                    if (!stackSoluce.IsEmpty() && stackSoluce.Top().ID != ((LABYRINTHELENGTHX - 1).ToString() + "," + (LABYRINTHELENGTHY - 1).ToString()))
                    {
                        stackSoluce.Pop();
                    }
                    stackBox.Pop();
                    CreateLabyrinthe(stackBox.Top(), "");
                }
            } 
            else
            {
                #region chemin à l'endroit
                while (!stackSoluce.IsEmpty())
                {
                    stackBox.Push(stackSoluce.Pop());
                }
                #endregion
                DisplayLabyrinthe();
                SolveLabyrinthe();
            }
        }

        public static void DisplayLabyrinthe()
        {
            int positionX = 0;
            int positionY = 0;

            for (int y = 0; y < LABYRINTHELENGTHY; y++)
            {
                for (int x = 0; x < LABYRINTHELENGTHX; x++)
                {
                    if (!tabBox[x, y].East && !tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("     ");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("███");
                    }
                    else if (!tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("█    ");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("█ █");
                    }
                    else if (!tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("█    ");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("███");
                    }
                    else if (!tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && !tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("███");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("     ");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("█ █");
                    }
                    else if (!tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("███");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("     ");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("███");
                    }
                    else if (!tabBox[x, y].East && tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("███");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("█    ");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("█ █");
                    }
                    else if (!tabBox[x, y].East && tabBox[x, y].North && tabBox[x, y].West && tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("███");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("█    ");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("███");
                    }
                    else if (tabBox[x, y].East && !tabBox[x, y].North && !tabBox[x, y].West && !tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("  █");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("█ █");
                    }
                    else if (tabBox[x, y].East && !tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("  █");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("███");
                    }
                    else if (tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("█ █");
                    }
                    else if (tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("███");
                    }
                    else if (tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && !tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("███");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("  █");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("█ █");
                    }
                    else if (tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("███");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("  █");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("███");
                    }
                    else if (tabBox[x, y].East && tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South)
                    {
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("███");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("█ █");
                        Console.SetCursorPosition(positionX, positionY + 2);
                        Console.Write("█ █");
                    }
                    positionX += 3;
                }
                positionX = 0;
                positionY += 2;
            }
            Console.ReadLine();
        }

        public static void SolveLabyrinthe()
        {
            #region chemin à l'envers
            /*int positionX = 26;
            int positionY = 18;
            Console.ForegroundColor = ConsoleColor.Green;
            while (!stackSoluce.IsEmpty())
            {
                for (int y = LABYRINTHELENGTHY - 1; y >= 0; y--)
                {
                    for (int x = LABYRINTHELENGTHX - 1; x >= 0; x--)
                    {
                        if (!tabBox[x, y].East && !tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 2, positionY);
                            Console.Write("█");
                            Console.SetCursorPosition(positionX, positionY + 1);
                            Console.Write("███");
                            stackSoluce.Pop();
                        }
                        else if (!tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 2, positionY);
                            Console.Write("█");
                            Console.SetCursorPosition(positionX + 2, positionY + 1);
                            Console.Write("███");
                            Console.SetCursorPosition(positionX + 2, positionY + 2);
                            Console.Write("█");
                            stackSoluce.Pop();
                        }
                        else if (!tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 2, positionY);
                            Console.Write("█");
                            Console.SetCursorPosition(positionX + 2, positionY + 1);
                            Console.Write("███");
                            stackSoluce.Pop();
                        }
                        else if (!tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX, positionY + 1);
                            Console.Write("███");
                            Console.SetCursorPosition(positionX + 2, positionY + 2);
                            Console.Write("█");
                            stackSoluce.Pop();
                        }
                        else if (!tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX, positionY + 1);
                            Console.Write("███");
                            stackSoluce.Pop();
                        }
                        else if (!tabBox[x, y].East && tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 2, positionY + 1);
                            Console.Write("███");
                            Console.SetCursorPosition(positionX + 2, positionY + 2);
                            Console.Write("█");
                            stackSoluce.Pop();
                        }
                        else if (!tabBox[x, y].East && tabBox[x, y].North && tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 1, positionY + 1);
                            Console.Write("███");
                            stackSoluce.Pop();
                        }
                        else if (tabBox[x, y].East && !tabBox[x, y].North && !tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 2, positionY);
                            Console.Write("█");
                            Console.SetCursorPosition(positionX, positionY + 1);
                            Console.Write("███");
                            Console.SetCursorPosition(positionX + 2, positionY + 2);
                            Console.Write("█");
                            stackSoluce.Pop();
                        }
                        else if (tabBox[x, y].East && !tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 2, positionY);
                            Console.Write("█");
                            Console.SetCursorPosition(positionX, positionY + 1);
                            Console.Write("███");
                            stackSoluce.Pop();
                        }
                        else if (tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 2, positionY);
                            Console.Write("█");
                            Console.SetCursorPosition(positionX + 2, positionY + 1);
                            Console.Write("█");
                            Console.SetCursorPosition(positionX + 2, positionY + 2);
                            Console.Write("█");
                            stackSoluce.Pop();
                        }
                        else if (tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 2, positionY);
                            Console.Write("█");
                            Console.SetCursorPosition(positionX + 2, positionY + 1);
                            Console.Write("█");
                            stackSoluce.Pop();
                        }
                        else if (tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX, positionY + 1);
                            Console.Write("███");
                            Console.SetCursorPosition(positionX + 2, positionY + 2);
                            Console.Write("█");
                            stackSoluce.Pop();
                        }
                        else if (tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX, positionY + 1);
                            Console.Write("███");
                            stackSoluce.Pop();
                        }
                        else if (tabBox[x, y].East && tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackSoluce.Top().ID)
                        {
                            Console.SetCursorPosition(positionX + 2, positionY + 1);
                            Console.Write("█");
                            Console.SetCursorPosition(positionX + 2, positionY + 2);
                            Console.Write("█");
                            stackSoluce.Pop();
                        }
                        positionX -= 3;
                    }
                    positionX = 26;
                    if (!stackSoluce.IsEmpty())
                    {
                        positionY = stackSoluce.Top().LocationY * 2;
                    }
                    Thread.Sleep(10);
                }
            }
            Console.ReadLine();*/
            #endregion

            #region chemin à l'endroit
            int positionX = 0;
            int positionY = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            for (int y = 0; y < LABYRINTHELENGTHY; y++)
            {
                for (int x = 0; x < LABYRINTHELENGTHX; x++)
                {
                    if (!tabBox[x, y].East && !tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY);
                        Console.Write("█");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("████");
                        stackBox.Pop();
                    }
                    else if (!tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY);
                        Console.Write("█");
                        Console.SetCursorPosition(positionX + 1, positionY + 1);
                        Console.Write("██");
                        Console.SetCursorPosition(positionX + 1, positionY + 2);
                        Console.Write("█");
                        stackBox.Pop();
                    }
                    else if (!tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY);
                        Console.Write("█");
                        Console.SetCursorPosition(positionX + 1, positionY + 1);
                        Console.Write("██");
                        stackBox.Pop();
                    }
                    else if (!tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("████");
                        Console.SetCursorPosition(positionX + 1, positionY + 2);
                        Console.Write("█");
                        stackBox.Pop();
                    }
                    else if (!tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("████");
                        stackBox.Pop();
                    }
                    else if (!tabBox[x, y].East && tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY + 1);
                        Console.Write("██");
                        Console.SetCursorPosition(positionX + 1, positionY + 2);
                        Console.Write("█");
                        stackBox.Pop();
                    }
                    else if (!tabBox[x, y].East && tabBox[x, y].North && tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY + 1);
                        Console.Write("██");
                        stackBox.Pop();
                    }
                    else if (tabBox[x, y].East && !tabBox[x, y].North && !tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY);
                        Console.Write("█");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("██");
                        Console.SetCursorPosition(positionX + 1, positionY + 2);
                        Console.Write("█");
                        stackBox.Pop();
                    }
                    else if (tabBox[x, y].East && !tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY);
                        Console.Write("█");
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("██");
                        stackBox.Pop();
                    }
                    else if (tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY);
                        Console.Write("█");
                        Console.SetCursorPosition(positionX + 1, positionY + 1);
                        Console.Write("█");
                        Console.SetCursorPosition(positionX + 1, positionY + 2);
                        Console.Write("█");
                        stackBox.Pop();
                    }
                    else if (tabBox[x, y].East && !tabBox[x, y].North && tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY);
                        Console.Write("█");
                        Console.SetCursorPosition(positionX + 1, positionY + 1);
                        Console.Write("█");
                        stackBox.Pop();
                    }
                    else if (tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("██");
                        Console.SetCursorPosition(positionX + 1, positionY + 2);
                        Console.Write("█");
                        stackBox.Pop();
                    }
                    else if (tabBox[x, y].East && tabBox[x, y].North && !tabBox[x, y].West && tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX, positionY + 1);
                        Console.Write("██");
                        stackBox.Pop();
                    }
                    else if (tabBox[x, y].East && tabBox[x, y].North && tabBox[x, y].West && !tabBox[x, y].South && tabBox[x, y].ID == stackBox.Top().ID)
                    {
                        Console.SetCursorPosition(positionX + 1, positionY + 1);
                        Console.Write("█");
                        Console.SetCursorPosition(positionX + 1, positionY + 2);
                        Console.Write("█");
                        stackBox.Pop();
                    }

                    if (!stackBox.IsEmpty())
                    {
                        positionX = stackBox.Top().LocationX * 3;

                        if (x > stackBox.Top().LocationX && stackBox.Top().LocationY == y)
                        {
                            x -= 2;
                        }
                    }
                }
                positionX = 0;
                if (!stackBox.IsEmpty())
                {
                    positionY = stackBox.Top().LocationY * 2;

                    if (y > stackBox.Top().LocationY)
                    {
                        y -= 2;
                    }
                }
                Thread.Sleep(10);
            }
            Console.ReadLine();
            #endregion
        }
    }
}
