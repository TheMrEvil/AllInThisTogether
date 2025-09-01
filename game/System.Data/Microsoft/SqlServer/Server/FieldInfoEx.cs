using System;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000041 RID: 65
	internal sealed class FieldInfoEx : IComparable
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000FA50 File Offset: 0x0000DC50
		internal FieldInfoEx(FieldInfo fi, int offset, Normalizer normalizer)
		{
			this.FieldInfo = fi;
			this.Offset = offset;
			this.Normalizer = normalizer;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000FA70 File Offset: 0x0000DC70
		public int CompareTo(object other)
		{
			FieldInfoEx fieldInfoEx = other as FieldInfoEx;
			if (fieldInfoEx == null)
			{
				return -1;
			}
			return this.Offset.CompareTo(fieldInfoEx.Offset);
		}

		// Token: 0x0400052B RID: 1323
		internal readonly int Offset;

		// Token: 0x0400052C RID: 1324
		internal readonly FieldInfo FieldInfo;

		// Token: 0x0400052D RID: 1325
		internal readonly Normalizer Normalizer;
	}
}
