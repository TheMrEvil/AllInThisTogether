using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000CA RID: 202
	internal sealed class FieldRVATable : SortedTable<FieldRVATable.Record>
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x00020754 File Offset: 0x0001E954
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].RVA = mr.ReadInt32();
				this.records[i].Field = mr.ReadField();
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000207A4 File Offset: 0x0001E9A4
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].RVA);
				mw.WriteField(this.records[i].Field);
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(4).WriteField().Value;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000207F0 File Offset: 0x0001E9F0
		internal void Fixup(ModuleBuilder moduleBuilder, int sdataRVA, int cilRVA)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				if (this.records[i].RVA < 0)
				{
					this.records[i].RVA = (this.records[i].RVA & int.MaxValue) + cilRVA;
				}
				else
				{
					FieldRVATable.Record[] records = this.records;
					int num = i;
					records[num].RVA = records[num].RVA + sdataRVA;
				}
				moduleBuilder.FixupPseudoToken(ref this.records[i].Field);
			}
			base.Sort();
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00020881 File Offset: 0x0001EA81
		public FieldRVATable()
		{
		}

		// Token: 0x040003EF RID: 1007
		internal const int Index = 29;

		// Token: 0x02000359 RID: 857
		internal struct Record : SortedTable<FieldRVATable.Record>.IRecord
		{
			// Token: 0x170008BE RID: 2238
			// (get) Token: 0x06002634 RID: 9780 RVA: 0x000B56B7 File Offset: 0x000B38B7
			int SortedTable<FieldRVATable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Field;
				}
			}

			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x06002635 RID: 9781 RVA: 0x000B56B7 File Offset: 0x000B38B7
			int SortedTable<FieldRVATable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Field;
				}
			}

			// Token: 0x04000ED8 RID: 3800
			internal int RVA;

			// Token: 0x04000ED9 RID: 3801
			internal int Field;
		}
	}
}
