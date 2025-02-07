namespace TextRPGTeam30
{
	internal class Dungeon
	{
		public int stage;
		public int rewardExp;
		public int rewardGold;
		List<Monster> monsters;
		List<Item> items;

		public Dungeon(int _stage, List<Monster> _monsters)
		{
			stage = _stage;
			monsters = _monsters;
		}
		public void StartDungeon()
		{

		}
		public void DungeonSuccess()
		{

		}

		public void DungeonFail()
		{

		}
	}
}
