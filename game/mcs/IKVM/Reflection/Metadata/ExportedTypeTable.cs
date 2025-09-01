using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000CE RID: 206
	internal sealed class ExportedTypeTable : Table<ExportedTypeTable.Record>
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x00020EA4 File Offset: 0x0001F0A4
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Flags = mr.ReadInt32();
				this.records[i].TypeDefId = mr.ReadInt32();
				this.records[i].TypeName = mr.ReadStringIndex();
				this.records[i].TypeNamespace = mr.ReadStringIndex();
				this.records[i].Implementation = mr.ReadImplementation();
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00020F3C File Offset: 0x0001F13C
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Flags);
				mw.Write(this.records[i].TypeDefId);
				mw.WriteStringIndex(this.records[i].TypeName);
				mw.WriteStringIndex(this.records[i].TypeNamespace);
				mw.WriteImplementation(this.records[i].Implementation);
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00020FCD File Offset: 0x0001F1CD
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(8).WriteStringIndex().WriteStringIndex().WriteImplementation().Value;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00020FEC File Offset: 0x0001F1EC
		internal int FindOrAddRecord(ExportedTypeTable.Record rec)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				if (this.records[i].Implementation == rec.Implementation && this.records[i].TypeName == rec.TypeName && this.records[i].TypeNamespace == rec.TypeNamespace)
				{
					return i + 1;
				}
			}
			return base.AddRecord(rec);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00021060 File Offset: 0x0001F260
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].Implementation);
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00021095 File Offset: 0x0001F295
		public ExportedTypeTable()
		{
		}

		// Token: 0x040003F3 RID: 1011
		internal const int Index = 39;

		// Token: 0x0200035D RID: 861
		internal struct Record
		{
			// Token: 0x04000EEF RID: 3823
			internal int Flags;

			// Token: 0x04000EF0 RID: 3824
			internal int TypeDefId;

			// Token: 0x04000EF1 RID: 3825
			internal int TypeName;

			// Token: 0x04000EF2 RID: 3826
			internal int TypeNamespace;

			// Token: 0x04000EF3 RID: 3827
			internal int Implementation;
		}
	}
}
