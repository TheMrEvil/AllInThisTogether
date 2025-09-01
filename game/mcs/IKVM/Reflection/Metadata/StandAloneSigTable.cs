using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000BE RID: 190
	internal sealed class StandAloneSigTable : Table<int>
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x0001FC8C File Offset: 0x0001DE8C
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i] = mr.ReadBlobIndex();
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001FCBC File Offset: 0x0001DEBC
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteBlobIndex(this.records[i]);
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001FCE8 File Offset: 0x0001DEE8
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteBlobIndex().Value;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		internal int FindOrAddRecord(int blob)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				if (this.records[i] == blob)
				{
					return i + 1;
				}
			}
			return base.AddRecord(blob);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		public StandAloneSigTable()
		{
		}

		// Token: 0x040003DD RID: 989
		internal const int Index = 17;
	}
}
