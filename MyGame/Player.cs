using System;
namespace MyGame
{
	/// <summary>
	/// Класс, представляющий данные об игроке
	/// </summary>
	public class Player
	{
		/// <summary>
		/// Индекс игрока, для идентификации во время игры
		/// </summary>
		public int Index;

		/// <summary>
		/// Никнейм игрока
		/// </summary>
		public string Name;

		/// <summary>
		/// Фишки игрока. Содержит информацию о положении каждой фишки игрока на поле (сектор игрового поля и номер ячейки)
		/// </summary>
		public List<int[]> Pieces = new List<int[]>();

		/// <summary>
		/// Темница. Отражает чьи фишки находятся в плену у данного игрока.
		/// </summary>
		public List<int> Dungeon = new List<int>();

		/// <summary>
		/// Контсрукор для создания игрока.
		/// </summary>
		/// <param name="gameIndex">Индекс в игре</param>
		/// <param name="name">Никнейм</param>
		/// <param name="countPieces">Количество ипользуемых в игре фишек</param>
		public Player(int gameIndex, string name, int countPieces)
		{
			Index = gameIndex;
			Name = name;

			for(int i = 0; i < countPieces; i++)
			{
                Pieces.Add(new int[] { gameIndex, -1});
            }
		}
	}
}

