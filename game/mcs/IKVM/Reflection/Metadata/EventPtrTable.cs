using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C0 RID: 192
	internal sealed class EventPtrTable : Table<int>
	{
		// Token: 0x0600092F RID: 2351 RVA: 0x0001FDE4 File Offset: 0x0001DFE4
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i] = mr.ReadEvent();
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		public EventPtrTable()
		{
		}

		// Token: 0x040003DF RID: 991
		internal const int Index = 19;
	}
}
