using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000AF RID: 175
	internal sealed class TypeDefTable : Table<TypeDefTable.Record>
	{
		// Token: 0x060008E2 RID: 2274 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Flags = mr.ReadInt32();
				this.records[i].TypeName = mr.ReadStringIndex();
				this.records[i].TypeNamespace = mr.ReadStringIndex();
				this.records[i].Extends = mr.ReadTypeDefOrRef();
				this.records[i].FieldList = mr.ReadField();
				this.records[i].MethodList = mr.ReadMethodDef();
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001EB90 File Offset: 0x0001CD90
		internal override void Write(MetadataWriter mw)
		{
			mw.ModuleBuilder.WriteTypeDefTable(mw);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001EB9E File Offset: 0x0001CD9E
		internal int AllocToken()
		{
			return 33554432 + base.AddVirtualRecord();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(4).WriteStringIndex().WriteStringIndex().WriteTypeDefOrRef().WriteField().WriteMethodDef().Value;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001EBD3 File Offset: 0x0001CDD3
		public TypeDefTable()
		{
		}

		// Token: 0x040003CD RID: 973
		internal const int Index = 2;

		// Token: 0x02000346 RID: 838
		internal struct Record
		{
			// Token: 0x04000E9D RID: 3741
			internal int Flags;

			// Token: 0x04000E9E RID: 3742
			internal int TypeName;

			// Token: 0x04000E9F RID: 3743
			internal int TypeNamespace;

			// Token: 0x04000EA0 RID: 3744
			internal int Extends;

			// Token: 0x04000EA1 RID: 3745
			internal int FieldList;

			// Token: 0x04000EA2 RID: 3746
			internal int MethodList;
		}
	}
}
