using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A5 RID: 677
	[Serializable]
	internal class StyleProperty
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x0006168C File Offset: 0x0005F88C
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x000616A4 File Offset: 0x0005F8A4
		public string name
		{
			get
			{
				return this.m_Name;
			}
			internal set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x000616B0 File Offset: 0x0005F8B0
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x000616C8 File Offset: 0x0005F8C8
		public int line
		{
			get
			{
				return this.m_Line;
			}
			internal set
			{
				this.m_Line = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x000616D4 File Offset: 0x0005F8D4
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x000616EC File Offset: 0x0005F8EC
		public StyleValueHandle[] values
		{
			get
			{
				return this.m_Values;
			}
			internal set
			{
				this.m_Values = value;
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000020C2 File Offset: 0x000002C2
		public StyleProperty()
		{
		}

		// Token: 0x040009CE RID: 2510
		[SerializeField]
		private string m_Name;

		// Token: 0x040009CF RID: 2511
		[SerializeField]
		private int m_Line;

		// Token: 0x040009D0 RID: 2512
		[SerializeField]
		private StyleValueHandle[] m_Values;

		// Token: 0x040009D1 RID: 2513
		[NonSerialized]
		internal bool isCustomProperty;

		// Token: 0x040009D2 RID: 2514
		[NonSerialized]
		internal bool requireVariableResolve;
	}
}
