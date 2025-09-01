using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000BD RID: 189
	internal sealed class FieldLayoutTable : SortedTable<FieldLayoutTable.Record>
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x0001FB80 File Offset: 0x0001DD80
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Offset = mr.ReadInt32();
				this.records[i].Field = mr.ReadField();
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001FBD0 File Offset: 0x0001DDD0
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Offset);
				mw.WriteField(this.records[i].Field);
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(4).WriteField().Value;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001FC30 File Offset: 0x0001DE30
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				this.records[i].Field = (moduleBuilder.ResolvePseudoToken(this.records[i].Field) & 16777215);
			}
			base.Sort();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001FC82 File Offset: 0x0001DE82
		public FieldLayoutTable()
		{
		}

		// Token: 0x040003DC RID: 988
		internal const int Index = 16;

		// Token: 0x02000351 RID: 849
		internal struct Record : SortedTable<FieldLayoutTable.Record>.IRecord
		{
			// Token: 0x170008B2 RID: 2226
			// (get) Token: 0x06002628 RID: 9768 RVA: 0x000B5687 File Offset: 0x000B3887
			int SortedTable<FieldLayoutTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Field;
				}
			}

			// Token: 0x170008B3 RID: 2227
			// (get) Token: 0x06002629 RID: 9769 RVA: 0x000B5687 File Offset: 0x000B3887
			int SortedTable<FieldLayoutTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Field;
				}
			}

			// Token: 0x04000EC2 RID: 3778
			internal int Offset;

			// Token: 0x04000EC3 RID: 3779
			internal int Field;
		}
	}
}
