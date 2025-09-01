using System;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x02000008 RID: 8
	public class ClipperRegistry
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000028B6 File Offset: 0x00000AB6
		protected ClipperRegistry()
		{
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028C9 File Offset: 0x00000AC9
		public static ClipperRegistry instance
		{
			get
			{
				if (ClipperRegistry.s_Instance == null)
				{
					ClipperRegistry.s_Instance = new ClipperRegistry();
				}
				return ClipperRegistry.s_Instance;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028E4 File Offset: 0x00000AE4
		public void Cull()
		{
			int count = this.m_Clippers.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_Clippers[i].PerformClipping();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000291A File Offset: 0x00000B1A
		public static void Register(IClipper c)
		{
			if (c == null)
			{
				return;
			}
			ClipperRegistry.instance.m_Clippers.AddUnique(c, true);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002932 File Offset: 0x00000B32
		public static void Unregister(IClipper c)
		{
			ClipperRegistry.instance.m_Clippers.Remove(c);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002945 File Offset: 0x00000B45
		public static void Disable(IClipper c)
		{
			ClipperRegistry.instance.m_Clippers.DisableItem(c);
		}

		// Token: 0x04000023 RID: 35
		private static ClipperRegistry s_Instance;

		// Token: 0x04000024 RID: 36
		private readonly IndexedSet<IClipper> m_Clippers = new IndexedSet<IClipper>();
	}
}
