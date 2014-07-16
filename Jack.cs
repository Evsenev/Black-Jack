using System;
using System.IO;
using System.Collections;

namespace BlackJack
{

    public class Game
    {
        public static bool PlayerWin = false;
        public static int PlayerBalance= 1000;
        public static int ComputerBalance = 1000;
        public static int Bank;

        public void Play()
        {
            
        }

        public static void BetToBank()
        {


            Bank = Convert.ToInt32(Console.ReadLine());
            PlayerBalance -= Bank;
            ComputerBalance -= Bank;
            Bank *= 2;

            
        }


        public static void Info()
        {
            Console.Write(" Добро пожаловать в, Black Jack!\n");



            Console.Write("Введите ставку от $5 до $100.\nМоя ставка: ");
            BetToBank();


            Console.WriteLine("\nВаш баланс: ${0}.\tБаланс комьютера: ${1}", PlayerBalance, ComputerBalance);
            Console.WriteLine("\nВаша ставка ${0}.\t\tБанк: ${1}", (Bank / 2), Bank);

        }

        public static void MoneyToWinner(int PlayerBalance, int ComputerBalance, int Bank)
		{
            if (PlayerWin == true) 
            {
                PlayerBalance += Bank;
            }
            if (PlayerWin == false) 
            {
                ComputerBalance += Bank;
            }
            
        }
    }


    public class Cards
    {

    }


    class MainClass
    {
        public static void Main(string[] args)
        {
			for(;;)
			{	
				Game.Info ();
				if (Game.ComputerBalance <= 0)
					break;
				if (Game.PlayerBalance <= 0) break;
				Console.WriteLine ("Нажмите любую клавишу, чтобы проодолжить...");
				Console.ReadKey ();
				Console.Clear ();
			}



            //Завершние программы
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
