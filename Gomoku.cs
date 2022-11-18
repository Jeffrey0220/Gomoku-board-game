using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace BoardGame
{
    class Gomoku : Game
    {

        string[] rows = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };
        string[] columns = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15" };
        int check = 0;
        protected override void ModeSystem()
        {
            
            Console.WriteLine("Choose a game mode by entering the number of the mode: ");
            Console.WriteLine("1 : Human VS Human ");
            Console.WriteLine("2 : Human VS Computer ");
            string choose = Console.ReadLine();
            while (!string.IsNullOrEmpty(choose) && choose != "1" && choose != "2")
            {
                Console.WriteLine("Invalid entering!");
                Console.WriteLine("Choose a game mode by entering the number of the mode: ");
                Console.WriteLine("1 : Human VS Human ");
                Console.WriteLine("2 : Human VS Computer ");
                choose = Console.ReadLine();
            }
            if (choose == "1")
            {
                humanVShuman();
            }
            if (choose == "2")
            {
                humanVScomputer();
            }
        }

        protected override void PromptSystem(int playerNum, string mode,int step)
        {
            

            string a= "Hello !";
            string b = "Welcome to play Five In A Row!";
            string c = $"This is {mode} mode.";
            string d1 = "Player 1: X";
            string d2 = "You are Player 1: X";
            string e1 = "Player 2: O";
            string e2 = "Computer is Player 2: O";
            string f1 = $"Step{step}: Player {playerNum} to move, select a position to put your piece.";
            string f2 = $"Step{step}: Player {playerNum} to move, select a position to put your piece, Computer will automatically put his piece in next step.";
            string n = "\n";
            if (mode == "Human VS Human")
            {

                Console.WriteLine(a);
                ArchiveSystem(step, a,ref check);
                Console.WriteLine(b);
                ArchiveSystem( step,b, ref check);
                Console.WriteLine(c);
                ArchiveSystem( step,c, ref check);
                Console.WriteLine(d1);
                ArchiveSystem(step,d1, ref check);
                Console.WriteLine(e1);
                ArchiveSystem(step,d2, ref check);
                Console.WriteLine();
                ArchiveSystem( step,n, ref check);
                Console.WriteLine(f1);
                ArchiveSystem( step,f1, ref check);
            }
            else if (mode == "Human VS Computer")
            {
                Console.WriteLine(a);
                ArchiveSystem(step,a, ref check);
                Console.WriteLine(b);
                ArchiveSystem(step,b, ref check);
                Console.WriteLine(c);
                ArchiveSystem(step,c, ref check);
                Console.WriteLine(d2);
                ArchiveSystem(step,d2, ref check);
                Console.WriteLine(e2);
                ArchiveSystem( step,e2, ref check);
                Console.WriteLine();
                ArchiveSystem( step,n, ref check);
                Console.WriteLine(f2);
                ArchiveSystem(step,f2, ref check);
            }           
        }


        protected override string[,] BoardRowsColumns()
        {
            string[,] boardRowsColumns = new string[rows.Length, columns.Length];
            
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < columns.Length; j++)
                {
                    
                    
                    boardRowsColumns[i, j] = rows[i] + columns[j];
                    
                }
            }

            return boardRowsColumns;
        }


        protected override string[] BoardCoordinates()
        {                        
            string[] boardCoordinates = new string[0];
            int amount = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < columns.Length; j++)
                {
                    amount = amount + 1;
                    Array.Resize<string>(ref boardCoordinates, amount);
                    
                    boardCoordinates[amount - 1] = rows[i] + columns[j];
                }
            }

            return boardCoordinates;
        }


        protected override void DisplayGameBoard(string[] boardCoordinates,int step)
        {          

            string border = " +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+";
            string EachRow1="";
            string EachRow2 = "";

            int d = 0;
            Console.WriteLine(border);
            ArchiveSystem(step,border, ref check);

            for (int i = 0; i < 15; i++)
            {
                
                string A = " |";
                Console.Write(A);
                EachRow1 =EachRow1+ A;


                for (int j = 0; j < 15; j++)
                {

                    string B = $"{boardCoordinates[d]}|";
                    Console.Write(B);
                    EachRow1= EachRow1 + B;
                    d++;
                   
                    
                }

                ArchiveSystem(step, EachRow1, ref check);
                EachRow1 = "";
                Console.WriteLine();
                

                string C = " +";
                Console.Write(C);
                EachRow2 = EachRow2 +C;
                for (int l = 0; l < 15; l++)
                {
                    string D = "---+";
                    Console.Write(D);
                    EachRow2 = EachRow2 + D;
                    
                }

                ArchiveSystem(step, EachRow2, ref check);
                EachRow2 = "";

                Console.WriteLine();
                
            }


            check = 0;
            
        }



        

        protected override void HumanPlayerMove(string[] boardCoordinates, string piece, string[,] boardRowsColumns, int playerNum, string mode,int step)
        {
            bool notValidMove = true;
            Console.WriteLine("Enter a coordinate combine with one letter from A to O and two digits number from 01 to 15) to put your piece:");
            Console.WriteLine("(Enter H to get online help)");

            do
            {
                
                string playerInput = Console.ReadLine();

                if (playerInput.ToUpper() == "H"|| playerInput.ToUpper() == "U")
                {
                    if (playerInput.ToUpper() == "H")
                    {
                        HelpSystem();
                        Console.Clear();
                        PromptSystem(playerNum, mode, step);
                        DisplayGameBoard(boardCoordinates, step);
                        Console.WriteLine("Enter a coordinate combine with one letter from A to O and two digits number from 01 to 15) to put your piece:(Enter Y to get online help)");
                        playerInput = Console.ReadLine();
                    }
                
                    
                }

                
                if (!string.IsNullOrEmpty(playerInput) && boardCoordinates.Contains(playerInput.ToUpper()))
                {

                    Console.Clear();

                    var index = Array.FindIndex(boardCoordinates, x => x == playerInput.ToUpper());

                    string currentMove = boardCoordinates[index];
                    

                    if (currentMove.Equals("O") || currentMove.Equals("X"))
                    {
                        Console.WriteLine("There is already a piece, please select another coordinate.");
                    }
                    else
                    {
                        boardCoordinates[index] = piece;

                        int d = 0;
                        for (int i = 0; i < rows.Length; i++)
                        {
                            for (int j = 0; j < columns.Length; j++)
                            {

                                boardRowsColumns[i, j] = boardCoordinates[d];
                                d++;
                            }
                        }
                       
                        notValidMove = false;

                    }
                }
                else
                {
                    Console.WriteLine("Invalid move please select another coordinate.");
                }
            } while (notValidMove);
        }

        protected override void ComputerPlayerMove(string[] boardCoordinates, string piece, string[,] boardRowsColumns)
        {
            Random computerMove = new Random();
            int index;
            string currentMove;
            do
            {

                index = computerMove.Next(boardCoordinates.Length);
                currentMove = boardCoordinates[index];
            }
            while (currentMove.Equals("O") || currentMove.Equals("X"));

            boardCoordinates[index] = piece;
        }

        protected override int GameOverSystem(string[,] boardRowsCloumns, string piece)
        {
                

            int check_count = 0;
            check_count = check_count + 1;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (boardRowsCloumns[i, j] == piece &&
                        boardRowsCloumns[i, j + 1] == piece &&
                        boardRowsCloumns[i, j + 2] == piece &&
                        boardRowsCloumns[i, j + 3] == piece &&
                        boardRowsCloumns[i, j + 4] == piece)
                    {
                        return 1;
                    }

                }
            }
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (boardRowsCloumns[i, j] == piece &&
                        boardRowsCloumns[i + 1, j] == piece &&
                        boardRowsCloumns[i + 2, j] == piece &&
                        boardRowsCloumns[i + 3, j] == piece &&
                        boardRowsCloumns[i + 4, j] == piece)
                    {
                        return 1;
                    }

                }
            }

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (boardRowsCloumns[i, j] == piece &&
                        boardRowsCloumns[i + 1, j + 1] == piece &&
                        boardRowsCloumns[i + 2, j + 2] == piece &&
                        boardRowsCloumns[i + 3, j + 3] == piece &&
                        boardRowsCloumns[i + 4, j + 4] == piece)
                    {
                        return 1;
                    }

                }
            }

            for (int i = 4; i < 15; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (boardRowsCloumns[i, j] == piece &&
                        boardRowsCloumns[i - 1, j + 1] == piece &&
                        boardRowsCloumns[i - 2, j + 2] == piece &&
                        boardRowsCloumns[i - 3, j + 3] == piece &&
                        boardRowsCloumns[i - 4, j + 4] == piece)
                    {
                        return 1;
                    }

                }
            }

            if (check_count == boardRowsCloumns.Length)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        protected override void HelpSystem()
        {


            const string FILENAME = "OnlineHelp.txt";


            StreamReader reader = new StreamReader(FILENAME);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine("Enter Q to exit:");
            string enter2 = Console.ReadLine();
            while (enter2.ToUpper() != "Q")
            {
                Console.WriteLine("Enter Q to exit:");
                enter2 = Console.ReadLine();
            }
            reader.Close();
            Console.Clear();
        }
            
            
       
        


    }
}
