using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000BC RID: 188
	internal sealed class ClassLayoutTable : SortedTable<ClassLayoutTable.Record>
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x0001FA94 File Offset: 0x0001DC94
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].PackingSize = mr.ReadInt16();
				this.records[i].ClassSize = mr.ReadInt32();
				this.records[i].Parent = mr.ReadTypeDef();
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001FAFC File Offset: 0x0001DCFC
		internal override void Write(MetadataWriter mw)
		{
			base.Sort();
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].PackingSize);
				mw.Write(this.records[i].ClassSize);
				mw.WriteTypeDef(this.records[i].Parent);
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001FB65 File Offset: 0x0001DD65
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(6).WriteTypeDef().Value;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001FB78 File Offset: 0x0001DD78
		public ClassLayoutTable()
		{
		}

		// Token: 0x040003DB RID: 987
		internal const int Index = 15;

		// Token: 0x02000350 RID: 848
		internal struct Record : SortedTable<ClassLayoutTable.Record>.IRecord
		{
			// Token: 0x170008B0 RID: 2224
			// (get) Token: 0x06002626 RID: 9766 RVA: 0x000B567F File Offset: 0x000B387F
			int SortedTable<ClassLayoutTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x170008B1 RID: 2225
			// (get) Token: 0x06002627 RID: 9767 RVA: 0x000B567F File Offset: 0x000B387F
			int SortedTable<ClassLayoutTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x04000EBF RID: 3775
			internal short PackingSize;

			// Token: 0x04000EC0 RID: 3776
			internal int ClassSize;

			// Token: 0x04000EC1 RID: 3777
			internal int Parent;
		}
	}
}
