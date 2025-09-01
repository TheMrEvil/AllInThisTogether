using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000BF RID: 191
	internal sealed class EventMapTable : SortedTable<EventMapTable.Record>
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Parent = mr.ReadTypeDef();
				this.records[i].EventList = mr.ReadEvent();
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteTypeDef(this.records[i].Parent);
				mw.WriteEvent(this.records[i].EventList);
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteTypeDef().WriteEvent().Value;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001FDDA File Offset: 0x0001DFDA
		public EventMapTable()
		{
		}

		// Token: 0x040003DE RID: 990
		internal const int Index = 18;

		// Token: 0x02000352 RID: 850
		internal struct Record : SortedTable<EventMapTable.Record>.IRecord
		{
			// Token: 0x170008B4 RID: 2228
			// (get) Token: 0x0600262A RID: 9770 RVA: 0x000B568F File Offset: 0x000B388F
			int SortedTable<EventMapTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x170008B5 RID: 2229
			// (get) Token: 0x0600262B RID: 9771 RVA: 0x000B568F File Offset: 0x000B388F
			int SortedTable<EventMapTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x04000EC4 RID: 3780
			internal int Parent;

			// Token: 0x04000EC5 RID: 3781
			internal int EventList;
		}
	}
}
