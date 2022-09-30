using UnityEngine;

namespace Managers
{
	public class LogerManager : BaseGameManager
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
			return "Loger";
		}
	}
}
