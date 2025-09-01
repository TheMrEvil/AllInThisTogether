using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B7 RID: 183
	internal sealed class MemberRefTable : Table<MemberRefTable.Record>
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x0001EFE0 File Offset: 0x0001D1E0
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Class = mr.ReadMemberRefParent();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].Signature = mr.ReadBlobIndex();
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001F048 File Offset: 0x0001D248
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteMemberRefParent(this.records[i].Class);
				mw.WriteStringIndex(this.records[i].Name);
				mw.WriteBlobIndex(this.records[i].Signature);
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001F0AB File Offset: 0x0001D2AB
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteMemberRefParent().WriteStringIndex().WriteBlobIndex().Value;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
		internal int FindOrAddRecord(MemberRefTable.Record record)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				if (this.records[i].Class == record.Class && this.records[i].Name == record.Name && this.records[i].Signature == record.Signature)
				{
					return i + 1;
				}
			}
			return base.AddRecord(record);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001F138 File Offset: 0x0001D338
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].Class);
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001F16D File Offset: 0x0001D36D
		public MemberRefTable()
		{
		}

		// Token: 0x040003D6 RID: 982
		internal const int Index = 10;

		// Token: 0x0200034B RID: 843
		internal struct Record
		{
			// Token: 0x04000EB1 RID: 3761
			internal int Class;

			// Token: 0x04000EB2 RID: 3762
			internal int Name;

			// Token: 0x04000EB3 RID: 3763
			internal int Signature;
		}
	}
}
