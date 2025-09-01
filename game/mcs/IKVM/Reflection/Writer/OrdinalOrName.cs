using System;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200008D RID: 141
	internal struct OrdinalOrName
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x000170C7 File Offset: 0x000152C7
		internal OrdinalOrName(ushort value)
		{
			this.Ordinal = value;
			this.Name = null;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000170D7 File Offset: 0x000152D7
		internal OrdinalOrName(string value)
		{
			this.Ordinal = ushort.MaxValue;
			this.Name = value;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000170EB File Offset: 0x000152EB
		internal bool IsGreaterThan(OrdinalOrName other)
		{
			if (this.Name != null)
			{
				return string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase) > 0;
			}
			return this.Ordinal > other.Ordinal;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00017119 File Offset: 0x00015319
		internal bool IsEqual(OrdinalOrName other)
		{
			if (this.Name != null)
			{
				return string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return this.Ordinal == other.Ordinal;
		}

		// Token: 0x040002E4 RID: 740
		internal readonly ushort Ordinal;

		// Token: 0x040002E5 RID: 741
		internal readonly string Name;
	}
}
