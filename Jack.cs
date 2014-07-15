using System;
using System.IO;

namespace BlackJack
{
	

	public class Moneys
	{	

		int bank_computer = 1000;
		int bank_player = 1000;






		public int BetBank ()
		{	
			int bet = Convert.ToInt32 (Console.ReadLine ());
			bet *= 2;

			return bet;
		}
	
	}




	public class Cards
	{

	}


	class MainClass
	{
		public static void Main (string[] args)

		{
			Console.Write (" Добро пожаловать в, Black Jack!\n");

			Moneys play = new Moneys();

			Console.Write ("Введите ставку от $5 до $100: ");
			int bank = play.BetBank ();

			Console.WriteLine ("Ваша ставка ${0}.\tБанк: ${1}", (bank / 2), bank);
		}
	}
}
