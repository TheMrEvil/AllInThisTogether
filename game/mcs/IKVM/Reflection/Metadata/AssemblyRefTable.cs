using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000CC RID: 204
	internal sealed class AssemblyRefTable : Table<AssemblyRefTable.Record>
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x00020AA0 File Offset: 0x0001ECA0
		internal int FindOrAddRecord(AssemblyRefTable.Record rec)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				if (this.records[i].Name == rec.Name && this.records[i].MajorVersion == rec.MajorVersion && this.records[i].MinorVersion == rec.MinorVersion && this.records[i].BuildNumber == rec.BuildNumber && this.records[i].RevisionNumber == rec.RevisionNumber && this.records[i].Flags == rec.Flags && this.records[i].PublicKeyOrToken == rec.PublicKeyOrToken && this.records[i].Culture == rec.Culture)
				{
					return i + 1;
				}
			}
			return base.AddRecord(rec);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00020BA0 File Offset: 0x0001EDA0
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].MajorVersion = mr.ReadUInt16();
				this.records[i].MinorVersion = mr.ReadUInt16();
				this.records[i].BuildNumber = mr.ReadUInt16();
				this.records[i].RevisionNumber = mr.ReadUInt16();
				this.records[i].Flags = mr.ReadInt32();
				this.records[i].PublicKeyOrToken = mr.ReadBlobIndex();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].Culture = mr.ReadStringIndex();
				this.records[i].HashValue = mr.ReadBlobIndex();
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00020C98 File Offset: 0x0001EE98
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].MajorVersion);
				mw.Write(this.records[i].MinorVersion);
				mw.Write(this.records[i].BuildNumber);
				mw.Write(this.records[i].RevisionNumber);
				mw.Write(this.records[i].Flags);
				mw.WriteBlobIndex(this.records[i].PublicKeyOrToken);
				mw.WriteStringIndex(this.records[i].Name);
				mw.WriteStringIndex(this.records[i].Culture);
				mw.WriteBlobIndex(this.records[i].HashValue);
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00020D8B File Offset: 0x0001EF8B
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(12).WriteBlobIndex().WriteStringIndex().WriteStringIndex().WriteBlobIndex().Value;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00020DAE File Offset: 0x0001EFAE
		public AssemblyRefTable()
		{
		}

		// Token: 0x040003F1 RID: 1009
		internal const int Index = 35;

		// Token: 0x0200035B RID: 859
		internal struct Record
		{
			// Token: 0x04000EE3 RID: 3811
			internal ushort MajorVersion;

			// Token: 0x04000EE4 RID: 3812
			internal ushort MinorVersion;

			// Token: 0x04000EE5 RID: 3813
			internal ushort BuildNumber;

			// Token: 0x04000EE6 RID: 3814
			internal ushort RevisionNumber;

			// Token: 0x04000EE7 RID: 3815
			internal int Flags;

			// Token: 0x04000EE8 RID: 3816
			internal int PublicKeyOrToken;

			// Token: 0x04000EE9 RID: 3817
			internal int Name;

			// Token: 0x04000EEA RID: 3818
			internal int Culture;

			// Token: 0x04000EEB RID: 3819
			internal int HashValue;
		}
	}
}
