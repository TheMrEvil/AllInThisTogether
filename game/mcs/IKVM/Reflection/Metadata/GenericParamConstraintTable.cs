using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000D3 RID: 211
	internal sealed class GenericParamConstraintTable : SortedTable<GenericParamConstraintTable.Record>
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x000216F0 File Offset: 0x0001F8F0
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Owner = mr.ReadGenericParam();
				this.records[i].Constraint = mr.ReadTypeDefOrRef();
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00021740 File Offset: 0x0001F940
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteGenericParam(this.records[i].Owner);
				mw.WriteTypeDefOrRef(this.records[i].Constraint);
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002178C File Offset: 0x0001F98C
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteGenericParam().WriteTypeDefOrRef().Value;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x000217A0 File Offset: 0x0001F9A0
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			int[] indexFixup = moduleBuilder.GenericParam.GetIndexFixup();
			for (int i = 0; i < this.rowCount; i++)
			{
				this.records[i].Owner = indexFixup[this.records[i].Owner - 1] + 1;
			}
			base.Sort();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000217F8 File Offset: 0x0001F9F8
		public GenericParamConstraintTable()
		{
		}

		// Token: 0x040003F8 RID: 1016
		internal const int Index = 44;

		// Token: 0x02000362 RID: 866
		internal struct Record : SortedTable<GenericParamConstraintTable.Record>.IRecord
		{
			// Token: 0x170008C4 RID: 2244
			// (get) Token: 0x0600263A RID: 9786 RVA: 0x000B56CF File Offset: 0x000B38CF
			int SortedTable<GenericParamConstraintTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Owner;
				}
			}

			// Token: 0x170008C5 RID: 2245
			// (get) Token: 0x0600263B RID: 9787 RVA: 0x000B56CF File Offset: 0x000B38CF
			int SortedTable<GenericParamConstraintTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Owner;
				}
			}

			// Token: 0x04000F01 RID: 3841
			internal int Owner;

			// Token: 0x04000F02 RID: 3842
			internal int Constraint;
		}
	}
}
