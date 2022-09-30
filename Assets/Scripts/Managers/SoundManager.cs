namespace Managers
{
	public class SoundManager : BaseGameManager
	{
		public override void Dispose()
		{
			throw new System.NotImplementedException();
		}

		public override bool Initialize()
		{
			return true;
		}

		public override string ManagerName()
		{
			return "Sound";
		}
	}
}

