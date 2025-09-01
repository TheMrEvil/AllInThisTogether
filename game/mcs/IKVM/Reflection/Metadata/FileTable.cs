using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000CD RID: 205
	internal sealed class FileTable : Table<FileTable.Record>
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x00020DB8 File Offset: 0x0001EFB8
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Flags = mr.ReadInt32();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].HashValue = mr.ReadBlobIndex();
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00020E20 File Offset: 0x0001F020
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Flags);
				mw.WriteStringIndex(this.records[i].Name);
				mw.WriteBlobIndex(this.records[i].HashValue);
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00020E83 File Offset: 0x0001F083
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(4).WriteStringIndex().WriteBlobIndex().Value;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00020E9B File Offset: 0x0001F09B
		public FileTable()
		{
		}

		// Token: 0x040003F2 RID: 1010
		internal const int Index = 38;

		// Token: 0x0200035C RID: 860
		internal struct Record
		{
			// Token: 0x04000EEC RID: 3820
			internal int Flags;

			// Token: 0x04000EED RID: 3821
			internal int Name;

			// Token: 0x04000EEE RID: 3822
			internal int HashValue;
		}
	}
}
