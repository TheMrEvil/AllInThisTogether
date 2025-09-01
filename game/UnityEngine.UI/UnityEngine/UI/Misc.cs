using System;

namespace UnityEngine.UI
{
	// Token: 0x0200002D RID: 45
	internal static class Misc
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000FE12 File Offset: 0x0000E012
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				if (Application.isPlaying)
				{
					if (obj is GameObject)
					{
						(obj as GameObject).transform.parent = null;
					}
					Object.Destroy(obj);
					return;
				}
				Object.DestroyImmediate(obj);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000FE4A File Offset: 0x0000E04A
		public static void DestroyImmediate(Object obj)
		{
			if (obj != null)
			{
				if (Application.isEditor)
				{
					Object.DestroyImmediate(obj);
					return;
				}
				Object.Destroy(obj);
			}
		}
	}
}
