using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C3 RID: 195
	internal sealed class PropertyPtrTable : Table<int>
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i] = mr.ReadProperty();
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		public PropertyPtrTable()
		{
		}

		// Token: 0x040003E2 RID: 994
		internal const int Index = 22;
	}
}
