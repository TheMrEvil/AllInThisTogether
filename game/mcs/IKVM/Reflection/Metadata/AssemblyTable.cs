using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000CB RID: 203
	internal sealed class AssemblyTable : Table<AssemblyTable.Record>
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x0002088C File Offset: 0x0001EA8C
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].HashAlgId = mr.ReadInt32();
				this.records[i].MajorVersion = mr.ReadUInt16();
				this.records[i].MinorVersion = mr.ReadUInt16();
				this.records[i].BuildNumber = mr.ReadUInt16();
				this.records[i].RevisionNumber = mr.ReadUInt16();
				this.records[i].Flags = mr.ReadInt32();
				this.records[i].PublicKey = mr.ReadBlobIndex();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].Culture = mr.ReadStringIndex();
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00020984 File Offset: 0x0001EB84
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].HashAlgId);
				mw.Write(this.records[i].MajorVersion);
				mw.Write(this.records[i].MinorVersion);
				mw.Write(this.records[i].BuildNumber);
				mw.Write(this.records[i].RevisionNumber);
				mw.Write(this.records[i].Flags);
				mw.WriteBlobIndex(this.records[i].PublicKey);
				mw.WriteStringIndex(this.records[i].Name);
				mw.WriteStringIndex(this.records[i].Culture);
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00020A77 File Offset: 0x0001EC77
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(16).WriteBlobIndex().WriteStringIndex().WriteStringIndex().Value;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00020A95 File Offset: 0x0001EC95
		public AssemblyTable()
		{
		}

		// Token: 0x040003F0 RID: 1008
		internal const int Index = 32;

		// Token: 0x0200035A RID: 858
		internal struct Record
		{
			// Token: 0x04000EDA RID: 3802
			internal int HashAlgId;

			// Token: 0x04000EDB RID: 3803
			internal ushort MajorVersion;

			// Token: 0x04000EDC RID: 3804
			internal ushort MinorVersion;

			// Token: 0x04000EDD RID: 3805
			internal ushort BuildNumber;

			// Token: 0x04000EDE RID: 3806
			internal ushort RevisionNumber;

			// Token: 0x04000EDF RID: 3807
			internal int Flags;

			// Token: 0x04000EE0 RID: 3808
			internal int PublicKey;

			// Token: 0x04000EE1 RID: 3809
			internal int Name;

			// Token: 0x04000EE2 RID: 3810
			internal int Culture;
		}
	}
}
