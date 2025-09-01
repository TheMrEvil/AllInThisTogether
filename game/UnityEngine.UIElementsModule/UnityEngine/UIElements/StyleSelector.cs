using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A7 RID: 679
	[Serializable]
	internal class StyleSelector
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0006171C File Offset: 0x0005F91C
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x00061734 File Offset: 0x0005F934
		public StyleSelectorPart[] parts
		{
			get
			{
				return this.m_Parts;
			}
			internal set
			{
				this.m_Parts = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00061740 File Offset: 0x0005F940
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x00061758 File Offset: 0x0005F958
		public StyleSelectorRelationship previousRelationship
		{
			get
			{
				return this.m_PreviousRelationship;
			}
			internal set
			{
				this.m_PreviousRelationship = value;
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00061764 File Offset: 0x0005F964
		public override string ToString()
		{
			return string.Join(", ", (from p in this.parts
			select p.ToString()).ToArray<string>());
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000617AF File Offset: 0x0005F9AF
		public StyleSelector()
		{
		}

		// Token: 0x040009D6 RID: 2518
		[SerializeField]
		private StyleSelectorPart[] m_Parts;

		// Token: 0x040009D7 RID: 2519
		[SerializeField]
		private StyleSelectorRelationship m_PreviousRelationship;

		// Token: 0x040009D8 RID: 2520
		internal int pseudoStateMask = -1;

		// Token: 0x040009D9 RID: 2521
		internal int negatedPseudoStateMask = -1;

		// Token: 0x020002A8 RID: 680
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001741 RID: 5953 RVA: 0x000617C6 File Offset: 0x0005F9C6
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001742 RID: 5954 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001743 RID: 5955 RVA: 0x000617D2 File Offset: 0x0005F9D2
			internal string <ToString>b__10_0(StyleSelectorPart p)
			{
				return p.ToString();
			}

			// Token: 0x040009DA RID: 2522
			public static readonly StyleSelector.<>c <>9 = new StyleSelector.<>c();

			// Token: 0x040009DB RID: 2523
			public static Func<StyleSelectorPart, string> <>9__10_0;
		}
	}
}
