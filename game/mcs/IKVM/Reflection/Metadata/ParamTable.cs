using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B5 RID: 181
	internal sealed class ParamTable : Table<ParamTable.Record>
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x0001EE00 File Offset: 0x0001D000
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Flags = mr.ReadInt16();
				this.records[i].Sequence = mr.ReadInt16();
				this.records[i].Name = mr.ReadStringIndex();
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001EE65 File Offset: 0x0001D065
		internal override void Write(MetadataWriter mw)
		{
			mw.ModuleBuilder.WriteParamTable(mw);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001EE73 File Offset: 0x0001D073
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(4).WriteStringIndex().Value;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001EE86 File Offset: 0x0001D086
		public ParamTable()
		{
		}

		// Token: 0x040003D4 RID: 980
		internal const int Index = 8;

		// Token: 0x02000349 RID: 841
		internal struct Record
		{
			// Token: 0x04000EAC RID: 3756
			internal short Flags;

			// Token: 0x04000EAD RID: 3757
			internal short Sequence;

			// Token: 0x04000EAE RID: 3758
			internal int Name;
		}
	}
}
