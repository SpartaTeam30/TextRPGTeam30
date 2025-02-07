namespace TextRPGTeam30
{
	internal class Dungeon
	{
		public int stage;
		public int rewardExp;
		public int rewardGold;
		public List<Monster> monsters;
		public List<Item> items;

		public Dungeon(int _stage, List<Monster> _monsters)
		{
			stage = _stage;
			rewardExp = 0;
			monsters = new List<Monster>();
			rewardGold = 0;

			for (int i = 0; i < 3; i++)
			{
				Monster monster = _monsters[new Random().Next(0, _monsters.Count)];
				monster.SetLevel(new Random().Next(stage / 5 + 1, stage / 5 + 6));
				monsters.Add(monster);
			}
		}
		public void DungeonSuccess()
		{
			foreach (Monster monster in monsters)
			{
				rewardExp += monster.Level;
			}
			rewardGold = new Random().Next(stage * 100, stage * 200);
			//item
        }

		public void DungeonFail()
		{
			
		}
	}
}
