using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002F0 RID: 752
	public abstract class UxmlTypeRestriction : IEquatable<UxmlTypeRestriction>
	{
		// Token: 0x060018EF RID: 6383 RVA: 0x00065D20 File Offset: 0x00063F20
		public virtual bool Equals(UxmlTypeRestriction other)
		{
			return this == other;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000020C2 File Offset: 0x000002C2
		protected UxmlTypeRestriction()
		{
		}
	}
}
