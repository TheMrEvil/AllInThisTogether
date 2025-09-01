using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000BA RID: 186
	internal sealed class FieldMarshalTable : SortedTable<FieldMarshalTable.Record>
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Parent = mr.ReadHasFieldMarshal();
				this.records[i].NativeType = mr.ReadBlobIndex();
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001F824 File Offset: 0x0001DA24
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteHasFieldMarshal(this.records[i].Parent);
				mw.WriteBlobIndex(this.records[i].NativeType);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001F870 File Offset: 0x0001DA70
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteHasFieldMarshal().WriteBlobIndex().Value;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001F884 File Offset: 0x0001DA84
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				this.records[i].Parent = moduleBuilder.ResolvePseudoToken(this.records[i].Parent);
			}
			base.Sort();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		internal static int EncodeHasFieldMarshal(int token)
		{
			int num = token >> 24;
			if (num == 4)
			{
				return (token & 16777215) << 1 | 0;
			}
			if (num != 8)
			{
				throw new InvalidOperationException();
			}
			return (token & 16777215) << 1 | 1;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001F909 File Offset: 0x0001DB09
		public FieldMarshalTable()
		{
		}

		// Token: 0x040003D9 RID: 985
		internal const int Index = 13;

		// Token: 0x0200034E RID: 846
		internal struct Record : SortedTable<FieldMarshalTable.Record>.IRecord
		{
			// Token: 0x170008AC RID: 2220
			// (get) Token: 0x06002622 RID: 9762 RVA: 0x000B5662 File Offset: 0x000B3862
			int SortedTable<FieldMarshalTable.Record>.IRecord.SortKey
			{
				get
				{
					return FieldMarshalTable.EncodeHasFieldMarshal(this.Parent);
				}
			}

			// Token: 0x170008AD RID: 2221
			// (get) Token: 0x06002623 RID: 9763 RVA: 0x000B566F File Offset: 0x000B386F
			int SortedTable<FieldMarshalTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x04000EBA RID: 3770
			internal int Parent;

			// Token: 0x04000EBB RID: 3771
			internal int NativeType;
		}
	}
}
