using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B0 RID: 176
	internal sealed class FieldPtrTable : Table<int>
	{
		// Token: 0x060008E7 RID: 2279 RVA: 0x0001EBDC File Offset: 0x0001CDDC
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i] = mr.ReadField();
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		public FieldPtrTable()
		{
		}

		// Token: 0x040003CE RID: 974
		internal const int Index = 3;
	}
}
