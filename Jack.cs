using System;
using System.IO;
using System.Collections.Generic;

namespace BlackJack
{
	public class Game
	{
		public static int PlayerBalance = 1000;
		public static int ComputerBalance = 1000;
		public static int Bank, Bet;
		public static int ComputerScore, PlayerScore;

		public  Opponent WinResult ()
		{
			if(ComputerScore > 21 && PlayerScore > 21)
				return Opponent.Nobody;
			if(ComputerScore > 21)
				return Opponent.Player;
			if(PlayerScore > 21)
				return Opponent.Computer;
			if(ComputerScore == PlayerScore)
				return Opponent.Nobody;
			if(ComputerScore > PlayerScore) 
				return Opponent.Computer;
			else
				return Opponent.Player;
		}

		public void PrintWinner(Opponent Winner){
			Console.WriteLine("\n\nОчки компьютера: {0}\t\tВаши очки: {1}",Game.ComputerScore, Game.PlayerScore);
			if (Winner == Opponent.Computer) 
				Console.WriteLine ("\nВы ПРОИГРАЛИ!");
			else if (Winner == Opponent.Player) 
				Console.WriteLine ("\nВы ВЫИГРАЛИ!");
			else 
				Console.WriteLine ("\nНИЧЬЯ!");
		}
	

		public void AskPlayerCard ()
		{	
			// если 21 нужно не спрашивать о карте
			Console.WriteLine ("\nХотите взять еще карту?" +
				"\nНажмите ДА (Y) или НЕТ (N)");
			char PressKey = (char)Console.Read ();
			if (PressKey == 'Y' || PressKey == 'y') {
				GiveCard (Opponent.Player);
				ShowCards (Opponent.Player);
				CalculateResult ();
				if (PlayerScore > 21) {
					return;
				} else {
					AskPlayerCard ();
				}
			} else if (PressKey == 'N' || PressKey == 'n') {
				return;
			} else {
				AskPlayerCard ();
			}

		}

		public void StartGame () 
		{
			PrintBalance ();
			BetToBank ();
			PrintBalance ();
			AddCards (); 			
			GiveStartCards ();		
			ComputerLogic ();		

			ShowCards (Opponent.Player);
			AskPlayerCard ();
			CalculateResult ();

			Console.Clear ();


			ShowCards (Opponent.Player);
			ShowCards (Opponent.Computer);

			Opponent isWinner = WinResult (); 
			MoneyToWinner (isWinner);
			PrintWinner (isWinner);

		}

		public void PrintBalance ()
		{
			Console.WriteLine ("\nВаш баланс: ${0}.\tБаланс комьютера: ${1}", PlayerBalance, ComputerBalance);
		}

		public void BetToBank ()
		{
			for (;;) {

				Console.Write ("Введите вашу ставку цифрами до $100.\nМоя ставка: ");
				try {
					Bet = Convert.ToInt32 (Console.ReadLine ());
				} catch {
					Bet = 0;
					Console.WriteLine ("Не корректный ввод. Попробуйте еще раз.");
				}
				if (Bet <= 0 && Bet > 100) {
					Console.WriteLine ("Попробуйте еще раз. Введите ставку от 1 до 100 цифрами.");
					Bet = 0;
				}
				if (0 < Bet && Bet <= 100) {
					if (Bet > PlayerBalance) {
						Console.WriteLine ("Попробуйте еще раз. Введите ставку меньше или равную балансу.");
						Bet = 0;
					} else {
						break;
					}
				}
			}

			PlayerBalance -= Bet;
			ComputerBalance -= Bet;
			Bank = Bet * 2;
			Console.WriteLine ("\nВаша ставка ${0}.\t\tБанк: ${1}", Bet, Bank);
		}

		public static List <Card> PlayerPack = new List<Card> ();
		public static List <Card> ComputerPack = new List<Card> ();
		public static List <Card> CardsPack = new List<Card> ();

		public void AddCards ()
		{
			CardsPack.Clear ();
			ComputerScore = 0;
			PlayerScore = 0;

			int value = 2;
			for (int i=0; i < 13; i++) {
				for (int j = 0; j < 4; j++) {
					CardsPack.Add (new Card (value, (Color)j));
				}
				if (value < 10)
					value++;
				if (i > 10)
					value = 11;

			}
		}

		public void ShowCards (Opponent WhosePack)
		{
			if (WhosePack == Opponent.Player) {
				Console.WriteLine ("\nВаши карты:");
				for (int i = 0; i < PlayerPack.Count; i++) {
					Console.Write ("\t{0} ", PlayerPack [i].card_score);
					if (PlayerPack [i].CardColor == Color.Clubs)
						Console.Write ("♣");
					if (PlayerPack [i].CardColor == Color.Spades)
						Console.Write ("♠");
					if (PlayerPack [i].CardColor == Color.Hearts)
						Console.Write ("♥");
					if (PlayerPack [i].CardColor == Color.Diamonds)
						Console.Write ("♦"); 
				}
			}
			if (WhosePack == Opponent.Computer) {
				Console.WriteLine ("\nКарты компьютера:");
				for (int i = 0; i < ComputerPack.Count; i++) {
					Console.Write ("\t{0} ", ComputerPack [i].card_score);
					if (ComputerPack [i].CardColor == Color.Clubs)
						Console.Write ("♣");
					if (ComputerPack [i].CardColor == Color.Spades)
						Console.Write ("♠");
					if (ComputerPack [i].CardColor == Color.Hearts)
						Console.Write ("♥");
					if (ComputerPack [i].CardColor == Color.Diamonds)
						Console.Write ("♦"); 
				}
			}
		}

		public void GiveCard (Opponent WhoTakeCard)
		{

			int numberCard = new Random ().Next (0, CardsPack.Count);
			int numberCard2 = new Random ().Next (0, CardsPack.Count);

			if (WhoTakeCard == Opponent.Player) {
				PlayerPack.Add (CardsPack [numberCard]);
				CardsPack.RemoveAt (numberCard);
			} else {
				ComputerPack.Add (CardsPack [numberCard2]);
				CardsPack.RemoveAt (numberCard2);
			}
		}

		public void GiveStartCards ()
		{

			PlayerPack.Clear ();
			ComputerPack.Clear ();

			GiveCard (Opponent.Player);
			GiveCard (Opponent.Computer);
			GiveCard (Opponent.Player);
			GiveCard (Opponent.Computer);
		}

		public void ComputerLogic ()  //Пока что простая и тупая логика
		{
			CalculateResult ();
			if (ComputerScore <= 15) {
				GiveCard (Opponent.Computer);
				ComputerLogic ();
			} else {
				return;
			}

		}

		public void CalculateResult ()
		{
			PlayerScore = 0;
			ComputerScore = 0;

			for (int i = 0; i < PlayerPack.Count; i++) {
				PlayerScore += PlayerPack [i].card_score;
			}
			for (int i = 0; i < ComputerPack.Count; i++) {
				ComputerScore += ComputerPack [i].card_score;
			}
		}

		public void MoneyToWinner (Opponent Winner) // На получение поставить метод WinResult
		{
			if (Winner == Opponent.Player) {
				PlayerBalance += Bank;
			} else if (Winner == Opponent.Computer) {
				ComputerBalance += Bank;
			} else if (Winner == Opponent.Nobody) {
				ComputerBalance += Bet;
				PlayerBalance += Bet;
			}
		}
	}

	public enum Color
	{		Spades,
		Hearts,
		Clubs,
		Diamonds
} 	// Картончные масти
	public enum Opponent {Player, Computer, Nobody}  				// Оппоненты

    public class Card
    {
		public int card_score;
		public Color CardColor;
		public Card (int theCardScore, Color theCardColor) 
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

			Console.WriteLine("♠ ♣ ♥ ♦ Добро пожаловать в Black Jack ♦ ♥ ♣ ♠\n");

			for(;;)
			{	
				GamePlay.StartGame ();
				if (Game.ComputerBalance <= 0) {
					Console.WriteLine ("У Компьютера кончились деньги. ");
					break;
				}
				if (Game.PlayerBalance <= 0) {
					Console.WriteLine ("У Вас кончились деньги.");
					break;
				}

				Console.WriteLine ("\nНажмите любую клавишу, чтобы продолжить игру...");
				Console.ReadKey ();
				Console.Clear ();

			}

            //Завершние программы
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();

        }
    }
}
