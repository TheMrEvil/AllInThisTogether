using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B6 RID: 182
	internal sealed class InterfaceImplTable : SortedTable<InterfaceImplTable.Record>
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x0001EE90 File Offset: 0x0001D090
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Class = mr.ReadTypeDef();
				this.records[i].Interface = mr.ReadTypeDefOrRef();
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteTypeDef(this.records[i].Class);
				mw.WriteEncodedTypeDefOrRef(this.records[i].Interface);
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001EF2C File Offset: 0x0001D12C
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteTypeDef().WriteTypeDefOrRef().Value;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001EF40 File Offset: 0x0001D140
		internal void Fixup()
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				int num = this.records[i].Interface;
				int num2 = num >> 24;
				switch (num2)
				{
				case 0:
					break;
				case 1:
					num = ((num & 16777215) << 2 | 1);
					break;
				case 2:
					num = ((num & 16777215) << 2 | 0);
					break;
				default:
					if (num2 != 27)
					{
						throw new InvalidOperationException();
					}
					num = ((num & 16777215) << 2 | 2);
					break;
				}
				this.records[i].Interface = num;
			}
			base.Sort();
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001EFD6 File Offset: 0x0001D1D6
		public InterfaceImplTable()
		{
		}

		// Token: 0x040003D5 RID: 981
		internal const int Index = 9;

		// Token: 0x0200034A RID: 842
		internal struct Record : SortedTable<InterfaceImplTable.Record>.IRecord
		{
			// Token: 0x170008A6 RID: 2214
			// (get) Token: 0x0600261C RID: 9756 RVA: 0x000B5630 File Offset: 0x000B3830
			int SortedTable<InterfaceImplTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Class;
				}
			}

			// Token: 0x170008A7 RID: 2215
			// (get) Token: 0x0600261D RID: 9757 RVA: 0x000B5630 File Offset: 0x000B3830
			int SortedTable<InterfaceImplTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Class;
				}
			}

			// Token: 0x04000EAF RID: 3759
			internal int Class;

			// Token: 0x04000EB0 RID: 3760
			internal int Interface;
		}
	}
}
