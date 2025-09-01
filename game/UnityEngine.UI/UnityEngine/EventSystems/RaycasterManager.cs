using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006E RID: 110
	internal static class RaycasterManager
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x0001B0D8 File Offset: 0x000192D8
		public static void AddRaycaster(BaseRaycaster baseRaycaster)
		{
			if (RaycasterManager.s_Raycasters.Contains(baseRaycaster))
			{
				return;
			}
			RaycasterManager.s_Raycasters.Add(baseRaycaster);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001B0F3 File Offset: 0x000192F3
		public static List<BaseRaycaster> GetRaycasters()
		{
			return RaycasterManager.s_Raycasters;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001B0FA File Offset: 0x000192FA
		public static void RemoveRaycasters(BaseRaycaster baseRaycaster)
		{
			if (!RaycasterManager.s_Raycasters.Contains(baseRaycaster))
			{
				return;
			}
			RaycasterManager.s_Raycasters.Remove(baseRaycaster);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001B116 File Offset: 0x00019316
		// Note: this type is marked as 'beforefieldinit'.
		static RaycasterManager()
		{
		}

		// Token: 0x04000228 RID: 552
		private static readonly List<BaseRaycaster> s_Raycasters = new List<BaseRaycaster>();
	}
}
