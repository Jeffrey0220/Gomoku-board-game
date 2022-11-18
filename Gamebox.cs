using System;

namespace BoardGame
{
    class Gamebox
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to BoardGames World!");
            string choose;

            do
            {
                Console.WriteLine("Choose a game by entering the number of the game: ");
                Console.WriteLine("1 : Gomoku (Five in a row) ");
                Console.WriteLine("2 : Othello (Reversi)");
                choose = Console.ReadLine();


                Console.WriteLine("\n*************************************\n");
            } while (string.IsNullOrEmpty(choose) || choose != "1" && choose != "2");


            if (choose == "2")
            {
                do
                {
                    Console.WriteLine("Sorry! Othello is not ready, only can choose No.1 Gomoku.");
                    Console.WriteLine("Choose a game by entering the number of the game: ");
                    Console.WriteLine("1 : Gomoku (Five in a row) ");
                    Console.WriteLine("2 : Othello (Reversi) ");
                    choose = Console.ReadLine();
                    Console.WriteLine("\n*************************************\n");
                } while (string.IsNullOrEmpty(choose) || choose != "1");
                

            }

        

            if (choose == "1")
            {
                Game gomoku = new Gomoku();
                gomoku.PlayGame();
            }


        }
    }
}
