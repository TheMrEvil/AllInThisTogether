using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000AE RID: 174
	internal sealed class TypeRefTable : Table<TypeRefTable.Record>
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].ResolutionScope = mr.ReadResolutionScope();
				this.records[i].TypeName = mr.ReadStringIndex();
				this.records[i].TypeNamespace = mr.ReadStringIndex();
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001EA24 File Offset: 0x0001CC24
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteResolutionScope(this.records[i].ResolutionScope);
				mw.WriteStringIndex(this.records[i].TypeName);
				mw.WriteStringIndex(this.records[i].TypeNamespace);
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001EA87 File Offset: 0x0001CC87
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteResolutionScope().WriteStringIndex().WriteStringIndex().Value;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].ResolutionScope);
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001EAD5 File Offset: 0x0001CCD5
		public TypeRefTable()
		{
		}

		// Token: 0x040003CC RID: 972
		internal const int Index = 1;

		// Token: 0x02000345 RID: 837
		internal struct Record
		{
			// Token: 0x04000E9A RID: 3738
			internal int ResolutionScope;

			// Token: 0x04000E9B RID: 3739
			internal int TypeName;

			// Token: 0x04000E9C RID: 3740
			internal int TypeNamespace;
		}
	}
}
