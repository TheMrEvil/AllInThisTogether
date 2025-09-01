using System;

namespace UnityEngine
{
	// Token: 0x020001F0 RID: 496
	public sealed class AddComponentMenu : Attribute
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x00023EA2 File Offset: 0x000220A2
		public AddComponentMenu(string menuName)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = 0;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00023EBA File Offset: 0x000220BA
		public AddComponentMenu(string menuName, int order)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = order;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x00023ED4 File Offset: 0x000220D4
		public string componentMenu
		{
			get
			{
				return this.m_AddComponentMenu;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00023EEC File Offset: 0x000220EC
		public int componentOrder
		{
			get
			{
				return this.m_Ordering;
			}
		}

		// Token: 0x040007D2 RID: 2002
		private string m_AddComponentMenu;

		// Token: 0x040007D3 RID: 2003
		private int m_Ordering;
	}
}
