using System;
using System.Collections.Generic;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000D0 RID: 208
	internal sealed class NestedClassTable : SortedTable<NestedClassTable.Record>
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x000211F0 File Offset: 0x0001F3F0
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].NestedClass = mr.ReadTypeDef();
				this.records[i].EnclosingClass = mr.ReadTypeDef();
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00021240 File Offset: 0x0001F440
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteTypeDef(this.records[i].NestedClass);
				mw.WriteTypeDef(this.records[i].EnclosingClass);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002128C File Offset: 0x0001F48C
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteTypeDef().WriteTypeDef().Value;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000212A0 File Offset: 0x0001F4A0
		internal List<int> GetNestedClasses(int enclosingClass)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.rowCount; i++)
			{
				if (this.records[i].EnclosingClass == enclosingClass)
				{
					list.Add(this.records[i].NestedClass);
				}
			}
			return list;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000212F0 File Offset: 0x0001F4F0
		public NestedClassTable()
		{
		}

		// Token: 0x040003F5 RID: 1013
		internal const int Index = 41;

		// Token: 0x0200035F RID: 863
		internal struct Record : SortedTable<NestedClassTable.Record>.IRecord
		{
			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x06002636 RID: 9782 RVA: 0x000B56BF File Offset: 0x000B38BF
			int SortedTable<NestedClassTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.NestedClass;
				}
			}

			// Token: 0x170008C1 RID: 2241
			// (get) Token: 0x06002637 RID: 9783 RVA: 0x000B56BF File Offset: 0x000B38BF
			int SortedTable<NestedClassTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.NestedClass;
				}
			}

			// Token: 0x04000EF8 RID: 3832
			internal int NestedClass;

			// Token: 0x04000EF9 RID: 3833
			internal int EnclosingClass;
		}
	}
}
