using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A6 RID: 678
	[Serializable]
	internal class StyleRule
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x000616F8 File Offset: 0x0005F8F8
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x00061710 File Offset: 0x0005F910
		public StyleProperty[] properties
		{
			get
			{
				return this.m_Properties;
			}
			internal set
			{
				this.m_Properties = value;
			}
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000020C2 File Offset: 0x000002C2
		public StyleRule()
		{
		}

		// Token: 0x040009D3 RID: 2515
		[SerializeField]
		private StyleProperty[] m_Properties;

		// Token: 0x040009D4 RID: 2516
		[SerializeField]
		internal int line;

		// Token: 0x040009D5 RID: 2517
		[NonSerialized]
		internal int customPropertiesCount;
	}
}
