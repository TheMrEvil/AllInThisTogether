using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B1 RID: 177
	internal sealed class FieldTable : Table<FieldTable.Record>
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x0001EC14 File Offset: 0x0001CE14
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Flags = mr.ReadInt16();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].Signature = mr.ReadBlobIndex();
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001EC79 File Offset: 0x0001CE79
		internal override void Write(MetadataWriter mw)
		{
			mw.ModuleBuilder.WriteFieldTable(mw);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001EC87 File Offset: 0x0001CE87
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(2).WriteStringIndex().WriteBlobIndex().Value;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001EC9F File Offset: 0x0001CE9F
		public FieldTable()
		{
		}

		// Token: 0x040003CF RID: 975
		internal const int Index = 4;

		// Token: 0x02000347 RID: 839
		internal struct Record
		{
			// Token: 0x04000EA3 RID: 3747
			internal short Flags;

			// Token: 0x04000EA4 RID: 3748
			internal int Name;

			// Token: 0x04000EA5 RID: 3749
			internal int Signature;
		}
	}
}
