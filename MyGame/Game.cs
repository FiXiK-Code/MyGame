namespace MyGame
{
	public class Game
	{
        /// <summary>
        /// Игровое поле. Состоит из ячеек поля, принадлежащих одному из игроков. Каждая ячейка содержит информацию налии фишки и ее принадлежности одному из игроков.
        /// </summary>
		private readonly List<Dictionary<int, string>> GameField = new List<Dictionary<int, string>>();

        /// <summary>
        /// Количество игроков. Используется, в том числе, для определения размеров игрового поля.
        /// </summary>
		private readonly int CountPlayers;

        /// <summary>
        /// Список игроков. У каждого игрока есть свои характеристики (см. подробнее в описаании класса Player)
        /// </summary>
		private List<Player> Players { get; set; } = new List<Player>();

        /// <summary>
        /// Индекс игрока, совершающего текущий ход.
        /// </summary>
		private int Walker { get; set; } = -1;

        /// <summary>
        /// Конструктор. Используется для создания (новой) игры.
        /// </summary>
        /// <param name="players">Список никнеймов игроков</param>
        /// <param name="countPieces">Количество фишек, использующихся для игры (не больше четырех)</param>
        public Game(string[] players, int countPieces)
		{
			CountPlayers = players.Count();

            // Заполнение игровых данных: создание списка игроков и генерация игрового поля.
			for (int i = 0; i < CountPlayers; i ++)
			{
				Players.Add(new Player( i, players[i], countPieces ));
				GameField.Add(new Dictionary<int, string>
				{
                    { 1,"non" },
                    { 2,"non" },
                    { 3,"non" },
                    { 30,"non" },
                    { 4,"non" },
                    { 5,"non" },
                    { 6,"non" },
                    { 7,"non" },
                    { 8,"non" },
                    { 9,"non" },
                    { 10,"non" },
                    { 11,"non" },
                    { 12,"non" },
                    { 13,"non" },
                    { 130,"non" },
                    { 14,"non" },
                    { 15,"non" },
                    { 16,"non" },
                    { 17,"non" },
                    { 18,"non" },
                    { 19,"non" },
                    { 20,"non" },
                    { 21,"non" },
                    { 22,"non" },
                });

            }
        }

        #region settings start Game

        /// <summary>
        /// Метод для выдачи права первого хода случайному игроку.
        /// </summary>
        /// <returns>Возвращает индекс игрока</returns>
        public int SetRandomWalker()
		{
			if(Walker == -1)
			{
                Random rnd = new Random();
                Walker = rnd.Next(0, CountPlayers);
                return Walker;
            }
			else return Walker;
        }

        /// <summary>
        /// Метод для выдачи права хода любому из игроков.
        /// </summary>
        /// <param name="playerIndex">Индекс игрока, которму нужно выдать право хода</param>
        /// <returns></returns>
		public int SetWalker(int playerIndex)
		{
            if (Walker == -1)
            {
                Walker = playerIndex;
                return Walker;
            }
            else return Walker;  
        }

        /// <summary>
        /// Метод для изменения порядка ходов.
        /// </summary>
        /// <param name="PlayerIndexes">Список игроков в порядке ходов (от первого к последнему)</param>
        /// <returns></returns>
		public bool SetPlayerIndexes(List<string[]> PlayerIndexes)
		{
			foreach(var player in PlayerIndexes)
			{
                Players.FirstOrDefault(p => p.Name == player[0]).Index = Convert.ToInt32(player[1]);
            }
			return true;
		}
        #endregion

        /// <summary>
        /// Метод для совершения хода
        /// </summary>
        /// <param name="playerIndex">Индекс игрока</param>
        /// <param name="pieceIndex">Индекс фишки, которой ходит игрок</param>
        /// <param name="countMove">Количество полей, которое должна пройти фишка</param>
        /// <returns>Возвращает true/false в зависимости от того, возможно ли совершить ход выбранной фишкой</returns>
        public bool TryMove(int playerIndex, int pieceIndex, int countMove)
        {
            var player = Players.FirstOrDefault(p => p.Index == playerIndex);
            var piece = player.Pieces[pieceIndex];

            if(piece[1] == 30 || piece[1] == 130)
            {
                piece[1] /= 10;
            }

            var oldCell = new int[] { piece[0], piece[1] };

            if (piece[1] < 16)
            {
                if ((piece[1] + countMove) < 8)
                {
                    piece[1] += countMove;
                }
                else if ((piece[1] + countMove) >= 8 && (piece[1] + countMove) <= 16)
                {
                    piece[1] += countMove;
                    piece[0] = piece[0] + 1 != CountPlayers ? piece[0] + 1 : 0;
                }
                else if ((piece[1] + countMove) > 16)
                {
                    if (piece[0] == playerIndex)
                    {
                        piece[1] += countMove;
                    }
                    else
                    {
                        piece[1] += countMove - 16;
                    }
                }
            }
            else if (piece[1] == 16 && piece[0] == playerIndex)
            {
                if ((piece[1] + countMove) <= 22)
                {
                    piece[1] += countMove;
                }
                else
                {
                    return false;
                }
            }
            else if (piece[1] == 16 && (piece[1] + countMove) > 16)
            {
                piece[1] += countMove - 16;
            }

            for(int field = oldCell[0]; field <= piece[0]; field++)
            {
                for (int cell = oldCell[1] + 1; cell <= 16; cell++)
                {
                    if(field == piece[0] && cell < piece[1])
                    {
                        if (GameField[field][cell] != "non") return false;
                    }
                    if(field == piece[0] && cell == piece[1])
                    {
                        if (GameField[field][cell] != "non")
                        {
                            var prisoner = Players.FirstOrDefault(p => p.Name == GameField[piece[0]][piece[1]]);
                            player.Dungeon.Add(prisoner.Index);
                            prisoner.Pieces.Remove(new int[] { piece[0], piece[1] });
                            prisoner.Pieces.Add(new int[] { player.Index, -1 });

                            GameField[oldCell[0]][oldCell[1]] = "non";
                            GameField[piece[0]][piece[1]] = $"{player.Name}";
                        }
                    }
                }
            }

            GameField[oldCell[0]][oldCell[1]] = "non";
            GameField[piece[0]][piece[1]] = $"{player.Name}";

            return true;
		}

    }
}

