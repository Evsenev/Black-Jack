using System;
using System.IO;
using System.Collections.Generic;

namespace BlackJack
{

    public class Game
    {
        public static bool PlayerWin = false;
        public static int PlayerBalance= 1000;
        public static int ComputerBalance = 1000;
        public static int Bank, Bet;

        public void Play()
        {
            
        }

        public static void BetToBank()
        {

            Bet = Convert.ToInt32(Console.ReadLine());
            PlayerBalance -= Bank;
            ComputerBalance -= Bank;
            Bank = Bet*2;
    
        }


		public static List <Card> CardsPack = new List<Card>();

		public static void AddCards()
		{
			for (int i=0; i < 13; i++) {
				int value = 2;
				if(value <= 9 )value++;
				if (i > 9)value = 11;
				for (int j = 0; j < 4; j++) {
					CardsPack.Add (new Card (value, Colors.Clubs));
					CardsPack.Add (new Card (value, Colors.Diamonds));
					CardsPack.Add (new Card (value, Colors.Hearts));
					CardsPack.Add (new Card (value, Colors.Spades));
				}
			}
		}



        public static void Info()
		{
			Console.Write("♠ ♣ ♥ ♦ Добро пожаловать в, Black Jack ♠ ♣ ♥ ♦!\n");

			// Console.WriteLine (Game.CardsPack.Count); //Проверка

            Console.Write("Введите ставку от $5 до $100.\nМоя ставка: ");
            BetToBank();

            Console.WriteLine("\nВаш баланс: ${0}.\tБаланс комьютера: ${1}", PlayerBalance, ComputerBalance);
            Console.WriteLine("\nВаша ставка ${0}.\t\tБанк: ${1}", Bet, Bank);

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

	public enum Colors {Spades, Hearts, Clubs, Diamonds } // Картончные масти

    public class Card
    {
		public int card_score;
		public Colors CardColor;
		public Card (int theCardScore, Colors theCardColor) 
		{
			card_score = theCardScore;
			CardColor = theCardColor;
		}
    }


    class MainClass
    {
        public static void Main(string[] args)
        {
			Game.AddCards ();
		

			for(;;)
			{	
				Game.Info ();
				if (Game.ComputerBalance <= 0)
					break;
				if (Game.PlayerBalance <= 0) break;
				Console.WriteLine ("Нажмите любую клавишу, чтобы продолжить...");
				Console.ReadKey ();
				Console.Clear ();

			}

            //Завершние программы
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
