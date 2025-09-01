using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000BB RID: 187
	internal sealed class DeclSecurityTable : SortedTable<DeclSecurityTable.Record>
	{
		// Token: 0x06000918 RID: 2328 RVA: 0x0001F914 File Offset: 0x0001DB14
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Action = mr.ReadInt16();
				this.records[i].Parent = mr.ReadHasDeclSecurity();
				this.records[i].PermissionSet = mr.ReadBlobIndex();
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001F97C File Offset: 0x0001DB7C
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Action);
				mw.WriteHasDeclSecurity(this.records[i].Parent);
				mw.WriteBlobIndex(this.records[i].PermissionSet);
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001F9DF File Offset: 0x0001DBDF
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(2).WriteHasDeclSecurity().WriteBlobIndex().Value;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001F9F8 File Offset: 0x0001DBF8
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				int num = this.records[i].Parent;
				moduleBuilder.FixupPseudoToken(ref num);
				int num2 = num >> 24;
				if (num2 != 2)
				{
					if (num2 != 6)
					{
						if (num2 != 32)
						{
							throw new InvalidOperationException();
						}
						num = ((num & 16777215) << 2 | 2);
					}
					else
					{
						num = ((num & 16777215) << 2 | 1);
					}
				}
				else
				{
					num = ((num & 16777215) << 2 | 0);
				}
				this.records[i].Parent = num;
			}
			base.Sort();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		public DeclSecurityTable()
		{
		}

		// Token: 0x040003DA RID: 986
		internal const int Index = 14;

		// Token: 0x0200034F RID: 847
		internal struct Record : SortedTable<DeclSecurityTable.Record>.IRecord
		{
			// Token: 0x170008AE RID: 2222
			// (get) Token: 0x06002624 RID: 9764 RVA: 0x000B5677 File Offset: 0x000B3877
			int SortedTable<DeclSecurityTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x170008AF RID: 2223
			// (get) Token: 0x06002625 RID: 9765 RVA: 0x000B5677 File Offset: 0x000B3877
			int SortedTable<DeclSecurityTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x04000EBC RID: 3772
			internal short Action;

			// Token: 0x04000EBD RID: 3773
			internal int Parent;

			// Token: 0x04000EBE RID: 3774
			internal int PermissionSet;
		}
	}
}
