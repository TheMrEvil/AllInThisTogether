using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000D2 RID: 210
	internal sealed class MethodSpecTable : Table<MethodSpecTable.Record>
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x000215A4 File Offset: 0x0001F7A4
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Method = mr.ReadMethodDefOrRef();
				this.records[i].Instantiation = mr.ReadBlobIndex();
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000215F4 File Offset: 0x0001F7F4
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteMethodDefOrRef(this.records[i].Method);
				mw.WriteBlobIndex(this.records[i].Instantiation);
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00021640 File Offset: 0x0001F840
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteMethodDefOrRef().WriteBlobIndex().Value;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00021654 File Offset: 0x0001F854
		internal int FindOrAddRecord(MethodSpecTable.Record record)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				if (this.records[i].Method == record.Method && this.records[i].Instantiation == record.Instantiation)
				{
					return i + 1;
				}
			}
			return base.AddRecord(record);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000216B0 File Offset: 0x0001F8B0
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].Method);
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000216E5 File Offset: 0x0001F8E5
		public MethodSpecTable()
		{
		}

		// Token: 0x040003F7 RID: 1015
		internal const int Index = 43;

		// Token: 0x02000361 RID: 865
		internal struct Record
		{
			// Token: 0x04000EFF RID: 3839
			internal int Method;

			// Token: 0x04000F00 RID: 3840
			internal int Instantiation;
		}
	}
}
