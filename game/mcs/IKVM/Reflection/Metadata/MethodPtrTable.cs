using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B2 RID: 178
	internal sealed class MethodPtrTable : Table<int>
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i] = mr.ReadMethodDef();
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		public MethodPtrTable()
		{
		}

		// Token: 0x040003D0 RID: 976
		internal const int Index = 5;
	}
}
