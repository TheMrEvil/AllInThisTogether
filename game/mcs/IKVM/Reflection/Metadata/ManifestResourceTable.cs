using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000CF RID: 207
	internal sealed class ManifestResourceTable : Table<ManifestResourceTable.Record>
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x000210A0 File Offset: 0x0001F2A0
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Offset = mr.ReadInt32();
				this.records[i].Flags = mr.ReadInt32();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].Implementation = mr.ReadImplementation();
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002111C File Offset: 0x0001F31C
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Offset);
				mw.Write(this.records[i].Flags);
				mw.WriteStringIndex(this.records[i].Name);
				mw.WriteImplementation(this.records[i].Implementation);
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00021196 File Offset: 0x0001F396
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(8).WriteStringIndex().WriteImplementation().Value;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000211B0 File Offset: 0x0001F3B0
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].Implementation);
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000211E5 File Offset: 0x0001F3E5
		public ManifestResourceTable()
		{
		}

		// Token: 0x040003F4 RID: 1012
		internal const int Index = 40;

		// Token: 0x0200035E RID: 862
		internal struct Record
		{
			// Token: 0x04000EF4 RID: 3828
			internal int Offset;

			// Token: 0x04000EF5 RID: 3829
			internal int Flags;

			// Token: 0x04000EF6 RID: 3830
			internal int Name;

			// Token: 0x04000EF7 RID: 3831
			internal int Implementation;
		}
	}
}
