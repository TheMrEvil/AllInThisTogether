using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C8 RID: 200
	internal sealed class TypeSpecTable : Table<int>
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x000205A0 File Offset: 0x0001E7A0
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i] = mr.ReadBlobIndex();
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000205D0 File Offset: 0x0001E7D0
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteBlobIndex(this.records[i]);
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001FCE8 File Offset: 0x0001DEE8
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteBlobIndex().Value;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		public TypeSpecTable()
		{
		}

		// Token: 0x040003ED RID: 1005
		internal const int Index = 27;
	}
}
