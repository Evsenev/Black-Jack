using System;
using System.IO;
using System.Collections.Generic;

namespace BlackJack
{

    public class Game
    {
        public static int PlayerBalance= 1000;
        public static int ComputerBalance = 1000;
        public static int Bank, Bet;
		public static int ComputerScore, PlayerScore, PlayerCardsCount, ComputerCardsCount;

        public  Opponents WinResult()
        {
			if (ComputerScore > 21 || PlayerScore > ComputerScore) 
				return Opponents.Player;
			if (PlayerScore > 21 || ComputerScore > PlayerScore)
				return Opponents.Computer;
			else
				return Opponents.Computer;
			            
        }

		public void AskPlayerCard ()
		{	

			Console.WriteLine("\nХотите взять еще карту?" +
			                  "\nНажмите <Y> (ДА) или <N> (Нет) и нажмите ENTER... ");
			char PressKey = (char)Console.Read();
			if (PressKey == 'Y' || PressKey == 'y') {
				GiveCard (Opponents.Player);
				ShowCards (Opponents.Player);
			} else if (PressKey == 'N' || PressKey == 'n') {
				return;
			} else {
				AskPlayerCard ();
			}

		}

		public void StartGame () //Начало игры
		{
			Info ();
			AddCards (); 			//Обновляем колоду
			GiveStartCards ();		//Получаем стартовые карты
			CalculateResult ();		//Подсчитываем карты
			ComputerLogic ();		//Включяем логику компьютера

			Console.WriteLine ("Ваши карты:");
			ShowCards (Opponents.Player);

			AskPlayerCard ();
			CalculateResult ();
			WinResult ();

			Console.WriteLine ("Карты компьютера:");
			ShowCards (Opponents.Computer);
		}

        public void BetToBank()
        {
            Bet = Convert.ToInt32(Console.ReadLine());
            PlayerBalance -= Bank;
            ComputerBalance -= Bank;
            Bank = Bet*2;
        }

		public static List <Card> PlayerPack = new List<Card>();
		public static List <Card> ComputerPack = new List<Card>();
		public static List <Card> CardsPack = new List<Card>();

		public void AddCards()
		{
			CardsPack.Clear();
			ComputerScore = 0;
			PlayerScore = 0;

			int value = 2;
			for (int i=0; i < 13; i++) {
				for (int j = 0; j < 4; j++) {
					CardsPack.Add (new Card (value, (Colors)j));
				}
				if(value < 10 )value++;
				if (i > 10)value = 11;

			}
		}

		public void ShowCards (Opponents WhosePack)
		{
			if (WhosePack == Opponents.Player) 
			{
				Console.WriteLine ();
				for (int i = 0; i < PlayerCardsCount; i++) 
				{
					Console.Write ("\t{0} ", PlayerPack [i].card_score);
					if (PlayerPack [i].CardColor == Colors.Clubs) Console.Write ("♣");
					if (PlayerPack [i].CardColor == Colors.Spades) Console.Write ("♠");
					if (PlayerPack [i].CardColor == Colors.Hearts) Console.Write ("♥");
					if (PlayerPack [i].CardColor == Colors.Diamonds) Console.Write ("♦"); 
				}
			}
			if (WhosePack == Opponents.Computer) 
			{
				Console.WriteLine ();
				for (int i = 0; i < 2; i++) 
				{
					Console.Write ("\t{0} ", ComputerPack [i].card_score);
					if (ComputerPack [i].CardColor == Colors.Clubs) Console.Write ("♣");
					if (ComputerPack [i].CardColor == Colors.Spades) Console.Write ("♠");
					if (ComputerPack [i].CardColor == Colors.Hearts) Console.Write ("♥");
					if (ComputerPack [i].CardColor == Colors.Diamonds) Console.Write ("♦"); 
				}
			}
		}



		public void GiveCard (Opponents WhoTakeCard)
		{
			int cardsInPack = 52;
			int numberCard = new Random ().Next (0, cardsInPack);
			int numberCard2 = new Random ().Next (0, cardsInPack);


			if (WhoTakeCard == Opponents.Player) {
				PlayerPack.Add (CardsPack [numberCard]);
				CardsPack.RemoveAt (numberCard);
				cardsInPack--;
				PlayerCardsCount++;
			} else {
				ComputerPack.Add (CardsPack [numberCard2]);
				CardsPack.RemoveAt (numberCard2);
				cardsInPack--;
				ComputerCardsCount++;
			}
		}

		public void GiveStartCards()
		{
			PlayerCardsCount = 2;
			ComputerCardsCount = 2;

			PlayerPack.Clear();
			ComputerPack.Clear();

			GiveCard (Opponents.Player);
			GiveCard (Opponents.Computer);
			GiveCard (Opponents.Player);
			GiveCard (Opponents.Computer);
		}

		public void ComputerLogic ()  //Пока что простая и тупая логика
		{
			if (ComputerScore <= 15) 
			{
				GiveCard (Opponents.Computer);
			}

		}

		public void CalculateResult()
		{
			PlayerScore = 0;
			ComputerScore = 0;

			for (int i = 0; i < PlayerCardsCount-1; i++) 
			{
				PlayerScore += PlayerPack [i].card_score;
			}
			for (int i = 0; i < ComputerCardsCount-1; i++)
			{
			PlayerScore += ComputerPack[i].card_score;
			}
		}

        public void Info()
		{
			Console.Write("♠ ♣ ♥ ♦ Добро пожаловать в, Black Jack ♦ ♥ ♣ ♠\n");

			Console.Write("Введите ставку от $5 до $100.\nМоя ставка: ");
			BetToBank();
			Console.WriteLine("\nВаш баланс: ${0}.\tБаланс комьютера: ${1}", PlayerBalance, ComputerBalance);
            Console.WriteLine("\nВаша ставка ${0}.\t\tБанк: ${1}", Bet, Bank);


        }

        public void MoneyToWinner(Opponents Winner) // На получение поставить метод WinResult
		{
			if (Winner == Opponents.Player) {
				PlayerBalance += Bank;
			} else {
				ComputerBalance += Bank;
			} 
        }
    }

	public enum Colors {Spades, Hearts, Clubs, Diamonds } 	// Картончные масти
	public enum Opponents {Player, Computer}  				// Оппоненты

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
			Game GamePlay = new Game ();


			for(;;)
			{	
				GamePlay.StartGame ();
				if (Game.ComputerBalance <= 0)
					break;
				if (Game.PlayerBalance <= 0) 
					break;
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
