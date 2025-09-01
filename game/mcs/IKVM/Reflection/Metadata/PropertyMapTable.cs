using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C2 RID: 194
	internal sealed class PropertyMapTable : SortedTable<PropertyMapTable.Record>
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x0001FF00 File Offset: 0x0001E100
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Parent = mr.ReadTypeDef();
				this.records[i].PropertyList = mr.ReadProperty();
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001FF50 File Offset: 0x0001E150
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteTypeDef(this.records[i].Parent);
				mw.WriteProperty(this.records[i].PropertyList);
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001FF9C File Offset: 0x0001E19C
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteTypeDef().WriteProperty().Value;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001FFAE File Offset: 0x0001E1AE
		public PropertyMapTable()
		{
		}

		// Token: 0x040003E1 RID: 993
		internal const int Index = 21;

		// Token: 0x02000354 RID: 852
		internal struct Record : SortedTable<PropertyMapTable.Record>.IRecord
		{
			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x0600262C RID: 9772 RVA: 0x000B5697 File Offset: 0x000B3897
			int SortedTable<PropertyMapTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x170008B7 RID: 2231
			// (get) Token: 0x0600262D RID: 9773 RVA: 0x000B5697 File Offset: 0x000B3897
			int SortedTable<PropertyMapTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x04000EC9 RID: 3785
			internal int Parent;

			// Token: 0x04000ECA RID: 3786
			internal int PropertyList;
		}
	}
}
