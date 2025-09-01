using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C6 RID: 198
	internal sealed class MethodImplTable : SortedTable<MethodImplTable.Record>
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x000203C0 File Offset: 0x0001E5C0
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Class = mr.ReadTypeDef();
				this.records[i].MethodBody = mr.ReadMethodDefOrRef();
				this.records[i].MethodDeclaration = mr.ReadMethodDefOrRef();
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00020428 File Offset: 0x0001E628
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteTypeDef(this.records[i].Class);
				mw.WriteMethodDefOrRef(this.records[i].MethodBody);
				mw.WriteMethodDefOrRef(this.records[i].MethodDeclaration);
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0002048B File Offset: 0x0001E68B
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteTypeDef().WriteMethodDefOrRef().WriteMethodDefOrRef().Value;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000204A4 File Offset: 0x0001E6A4
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].MethodBody);
				moduleBuilder.FixupPseudoToken(ref this.records[i].MethodDeclaration);
			}
			base.Sort();
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000204F6 File Offset: 0x0001E6F6
		public MethodImplTable()
		{
		}

		// Token: 0x040003EB RID: 1003
		internal const int Index = 25;

		// Token: 0x02000357 RID: 855
		internal struct Record : SortedTable<MethodImplTable.Record>.IRecord
		{
			// Token: 0x170008BA RID: 2234
			// (get) Token: 0x06002630 RID: 9776 RVA: 0x000B56A7 File Offset: 0x000B38A7
			int SortedTable<MethodImplTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Class;
				}
			}

			// Token: 0x170008BB RID: 2235
			// (get) Token: 0x06002631 RID: 9777 RVA: 0x000B56A7 File Offset: 0x000B38A7
			int SortedTable<MethodImplTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Class;
				}
			}

			// Token: 0x04000ED1 RID: 3793
			internal int Class;

			// Token: 0x04000ED2 RID: 3794
			internal int MethodBody;

			// Token: 0x04000ED3 RID: 3795
			internal int MethodDeclaration;
		}
	}
}
