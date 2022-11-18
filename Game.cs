using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BoardGame
{
    public abstract class Game
    {
        private int playerNum = 0;
        


        protected abstract void ModeSystem();
        protected abstract void PromptSystem(int playerNum, string mode, int step);
        protected abstract string[,] BoardRowsColumns();
        protected abstract string[] BoardCoordinates(); 
        protected abstract void DisplayGameBoard(string[] boardCoordinates, int step);
        protected abstract void HumanPlayerMove(string[] boardCoordinates, string piece,string[,] boardRowsColumns,int playerNum, string mode,int step);
        protected abstract void ComputerPlayerMove(string[] boardCoordinates, string piece, string[,] boardRowsColumns);
        protected abstract int GameOverSystem(string[,] boardRowsColumns, string piece);
        protected abstract void HelpSystem();


        //Choose play mode.
        public void PlayGame()
        {
            ModeSystem();

        }


        //Play human vs human mode
        public void humanVShuman()
        {
            string mode = "Human VS Human";
            int gameStatus = 0;
            int step = 0;
            
            
            
            string[] boardCoordinates = BoardCoordinates();
            string[,] boardRowsColumns = BoardRowsColumns();
            do
            {
                Console.Clear();

                step++;
                playerNum = GetPlayerNum(step);

                PromptSystem(playerNum,mode,step);
                DisplayGameBoard(boardCoordinates,step);


                string piece = PickPiece(playerNum);
                
                HumanPlayerMove(boardCoordinates, piece, boardRowsColumns,playerNum,mode,step);

                gameStatus = GameOverSystem(boardRowsColumns, piece);

            } while (gameStatus.Equals(0));


            
            if (gameStatus.Equals(1))
            {
                Console.Clear();
                DisplayGameBoard(boardCoordinates, step);
                Console.WriteLine($"Player {playerNum} is the winner!");
                Console.WriteLine("Play again? Y/N");
                string p = Console.ReadLine();
                if (p.ToUpper() == "Y")
                {
                    ModeSystem();
                }
                else
                {
                    Console.WriteLine("SEE YOU LATER!");
                }


            }
            if (gameStatus.Equals(2))
            {
                Console.Clear();
                DisplayGameBoard(boardCoordinates, step);
                Console.WriteLine($"The game is a draw!");
                Console.WriteLine("Play again? Y/N");
                string p = Console.ReadLine();
                if (p.ToUpper() == "Y")
                {
                    ModeSystem();
                }
                else
                {
                    Console.WriteLine("SEE YOU LATER!");
                }
            }


        }


        //Play human vs computer mode
        public void humanVScomputer()
        {
            string mode = "Human VS Computer";
            int gameStatus = 0;
            int step = 0;


            string[] boardCoordinates = BoardCoordinates();
            string[,] boardRowsColumns = BoardRowsColumns();
            do
            {
                Console.Clear();

                step++;
                playerNum = GetPlayerNum(step);

                PromptSystem(playerNum, mode, step);
                DisplayGameBoard(boardCoordinates, step);


                string piece = PickPiece(playerNum);

                HumanPlayerMove(boardCoordinates, piece, boardRowsColumns, playerNum, mode, step);

                gameStatus = GameOverSystem(boardRowsColumns, piece);

                if (gameStatus.Equals(0))
                {
                    step++;
                    playerNum = GetPlayerNum(step);
                    piece = PickPiece(playerNum);
                    ComputerPlayerMove(boardCoordinates, piece, boardRowsColumns);                   

                    gameStatus = GameOverSystem(boardRowsColumns, piece);
                }


            } while (gameStatus.Equals(0));





            if (gameStatus.Equals(1))
            {
                Console.Clear();
                DisplayGameBoard(boardCoordinates, step);
                Console.WriteLine($"Player {playerNum} is the winner!");
                Console.WriteLine("Play again? Y/N");
                string p=Console.ReadLine();
                if (p.ToUpper()=="Y")
                {
                    ModeSystem();
                }
                else
                {
                    Console.WriteLine("SEE YOU LATER!");
                }


            }
            if (gameStatus.Equals(2))
            {
                Console.Clear();
                DisplayGameBoard(boardCoordinates, step);
                Console.WriteLine($"The game is a draw!");
                Console.WriteLine("Play again? Y/N");
                string p = Console.ReadLine();
                if (p.ToUpper() == "Y")
                {
                    ModeSystem();
                }
                else
                {
                    Console.WriteLine("SEE YOU LATER!");
                }

            }
        }


        //Use this function to get current player number.
        private static int GetPlayerNum(int step)
        {
            {
                if (step%2 == 1)
                {
                    return 1;
                }
                else
                {
                    return 2;

                }
            }
        }

        //Use this function to get player's pieces.
        private static string PickPiece(int playerNum)
        {
            if (playerNum % 2 == 0)
            {
                return " O ";
            }
            else
            {
                return " X ";
            }
        }

        // Use this function to record each step into a text file.
        public static void ArchiveSystem(int step,string content,ref int check)
        {

           
                string FILENAME = $"STEP{step}.txt";
            if (check != step)
            {

                FileStream ArchiveFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);

                StreamWriter writer = new StreamWriter(ArchiveFile);

                writer.WriteLine(content);
                writer.Close();
                ArchiveFile.Close();

                check = step;
            }
            else
            {

                StreamWriter writer = File.AppendText(FILENAME);

                writer.WriteLine(content);

                writer.Close();
            }
                   

        }

        // Didn't implement redo/undo function 
        public void RedoSystem(string piece, string[,] boardrowscolumns, string[] boardcoordinates, int playernum, string mode, ref int step)
        {
            Console.WriteLine("************************************************************************************************************");
            Console.WriteLine("*(enter r to redo your move, you can do it again)                                                          *");
            Console.WriteLine("*(enter u to undo your move, each time step back to your last move, the furthest is back to your fist move)*");
            Console.WriteLine("*(tapping enter button to confirm your move, go to next player )                                           *");
            Console.WriteLine("************************************************************************************************************");
            string enter = Console.ReadLine();

                if (enter.ToUpper() == "r" && step > 0)
                {

                    string FILENAME = $"STEP{step}.txt";
                    StreamReader sr = new StreamReader(FILENAME);
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }


                    step--;


                }
                else if (enter.ToUpper() == "u" && step > 2)
                {
                  step = step - 2;
                  string FILENAME = $"STEP{step}.txt";
                  StreamReader sr = new StreamReader(FILENAME);
                  string line;

                  while ((line = sr.ReadLine()) != null)
                  {
                    Console.WriteLine(line);
                  }


                   step--;
                }
            
           

            


        }





    }
}
