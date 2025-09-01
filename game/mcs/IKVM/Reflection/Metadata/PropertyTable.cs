using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C4 RID: 196
	internal sealed class PropertyTable : Table<PropertyTable.Record>
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x0001FFE8 File Offset: 0x0001E1E8
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Flags = mr.ReadInt16();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].Type = mr.ReadBlobIndex();
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00020050 File Offset: 0x0001E250
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Flags);
				mw.WriteStringIndex(this.records[i].Name);
				mw.WriteBlobIndex(this.records[i].Type);
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001EC87 File Offset: 0x0001CE87
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(2).WriteStringIndex().WriteBlobIndex().Value;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000200B3 File Offset: 0x0001E2B3
		public PropertyTable()
		{
		}

		// Token: 0x040003E3 RID: 995
		internal const int Index = 23;

		// Token: 0x02000355 RID: 853
		internal struct Record
		{
			// Token: 0x04000ECB RID: 3787
			internal short Flags;

			// Token: 0x04000ECC RID: 3788
			internal int Name;

			// Token: 0x04000ECD RID: 3789
			internal int Type;
		}
	}
}
