using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B3 RID: 179
	internal sealed class MethodDefTable : Table<MethodDefTable.Record>
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].RVA = mr.ReadInt32();
				this.records[i].ImplFlags = mr.ReadInt16();
				this.records[i].Flags = mr.ReadInt16();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].Signature = mr.ReadBlobIndex();
				this.records[i].ParamList = mr.ReadParam();
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001ED88 File Offset: 0x0001CF88
		internal override void Write(MetadataWriter mw)
		{
			mw.ModuleBuilder.WriteMethodDefTable(this.baseRVA, mw);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001ED9C File Offset: 0x0001CF9C
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(8).WriteStringIndex().WriteBlobIndex().WriteParam().Value;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001EDB9 File Offset: 0x0001CFB9
		internal void Fixup(TextSection code)
		{
			this.baseRVA = (int)code.MethodBodiesRVA;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001EDC7 File Offset: 0x0001CFC7
		public MethodDefTable()
		{
		}

		// Token: 0x040003D1 RID: 977
		internal const int Index = 6;

		// Token: 0x040003D2 RID: 978
		private int baseRVA;

		// Token: 0x02000348 RID: 840
		internal struct Record
		{
			// Token: 0x04000EA6 RID: 3750
			internal int RVA;

			// Token: 0x04000EA7 RID: 3751
			internal short ImplFlags;

			// Token: 0x04000EA8 RID: 3752
			internal short Flags;

			// Token: 0x04000EA9 RID: 3753
			internal int Name;

			// Token: 0x04000EAA RID: 3754
			internal int Signature;

			// Token: 0x04000EAB RID: 3755
			internal int ParamList;
		}
	}
}
