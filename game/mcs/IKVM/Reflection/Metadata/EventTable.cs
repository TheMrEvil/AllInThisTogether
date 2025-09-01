using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C1 RID: 193
	internal sealed class EventTable : Table<EventTable.Record>
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x0001FE14 File Offset: 0x0001E014
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].EventFlags = mr.ReadInt16();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].EventType = mr.ReadTypeDefOrRef();
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001FE7C File Offset: 0x0001E07C
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].EventFlags);
				mw.WriteStringIndex(this.records[i].Name);
				mw.WriteTypeDefOrRef(this.records[i].EventType);
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001FEDF File Offset: 0x0001E0DF
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(2).WriteStringIndex().WriteTypeDefOrRef().Value;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001FEF7 File Offset: 0x0001E0F7
		public EventTable()
		{
		}

		// Token: 0x040003E0 RID: 992
		internal const int Index = 20;

		// Token: 0x02000353 RID: 851
		internal struct Record
		{
			// Token: 0x04000EC6 RID: 3782
			internal short EventFlags;

			// Token: 0x04000EC7 RID: 3783
			internal int Name;

			// Token: 0x04000EC8 RID: 3784
			internal int EventType;
		}
	}
}
