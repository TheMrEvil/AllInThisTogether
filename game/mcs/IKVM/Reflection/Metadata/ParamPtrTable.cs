using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B4 RID: 180
	internal sealed class ParamPtrTable : Table<int>
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x0001EDD0 File Offset: 0x0001CFD0
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i] = mr.ReadParam();
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		public ParamPtrTable()
		{
		}

		// Token: 0x040003D3 RID: 979
		internal const int Index = 7;
	}
}
