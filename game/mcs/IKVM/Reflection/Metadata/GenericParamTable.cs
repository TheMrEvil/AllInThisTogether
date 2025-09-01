using System;
using System.Collections.Generic;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000D1 RID: 209
	internal sealed class GenericParamTable : SortedTable<GenericParamTable.Record>, IComparer<GenericParamTable.Record>
	{
		// Token: 0x0600097C RID: 2428 RVA: 0x000212F8 File Offset: 0x0001F4F8
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Number = mr.ReadInt16();
				this.records[i].Flags = mr.ReadInt16();
				this.records[i].Owner = mr.ReadTypeOrMethodDef();
				this.records[i].Name = mr.ReadStringIndex();
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00021374 File Offset: 0x0001F574
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Number);
				mw.Write(this.records[i].Flags);
				mw.WriteTypeOrMethodDef(this.records[i].Owner);
				mw.WriteStringIndex(this.records[i].Name);
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000213EE File Offset: 0x0001F5EE
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(4).WriteTypeOrMethodDef().WriteStringIndex().Value;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00021408 File Offset: 0x0001F608
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				int owner = this.records[i].Owner;
				moduleBuilder.FixupPseudoToken(ref owner);
				int num = owner >> 24;
				if (num != 2)
				{
					if (num != 6)
					{
						throw new InvalidOperationException();
					}
					this.records[i].Owner = ((owner & 16777215) << 1 | 1);
				}
				else
				{
					this.records[i].Owner = ((owner & 16777215) << 1 | 0);
				}
				this.records[i].unsortedIndex = i;
			}
			Array.Sort<GenericParamTable.Record>(this.records, 0, this.rowCount, this);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000214BC File Offset: 0x0001F6BC
		int IComparer<GenericParamTable.Record>.Compare(GenericParamTable.Record x, GenericParamTable.Record y)
		{
			if (x.Owner == y.Owner)
			{
				if (x.Number == y.Number)
				{
					return 0;
				}
				if (x.Number <= y.Number)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (x.Owner <= y.Owner)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002150A File Offset: 0x0001F70A
		internal void PatchAttribute(int token, GenericParameterAttributes genericParameterAttributes)
		{
			this.records[(token & 16777215) - 1].Flags = (short)genericParameterAttributes;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00021528 File Offset: 0x0001F728
		internal int[] GetIndexFixup()
		{
			int[] array = new int[this.rowCount];
			for (int i = 0; i < this.rowCount; i++)
			{
				array[this.records[i].unsortedIndex] = i;
			}
			return array;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00021568 File Offset: 0x0001F768
		internal int FindFirstByOwner(int token)
		{
			SortedTable<GenericParamTable.Record>.Enumerator enumerator = base.Filter(token).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return -1;
			}
			return enumerator.Current;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00021599 File Offset: 0x0001F799
		public GenericParamTable()
		{
		}

		// Token: 0x040003F6 RID: 1014
		internal const int Index = 42;

		// Token: 0x02000360 RID: 864
		internal struct Record : SortedTable<GenericParamTable.Record>.IRecord
		{
			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x06002638 RID: 9784 RVA: 0x000B56C7 File Offset: 0x000B38C7
			int SortedTable<GenericParamTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Owner;
				}
			}

			// Token: 0x170008C3 RID: 2243
			// (get) Token: 0x06002639 RID: 9785 RVA: 0x000B56C7 File Offset: 0x000B38C7
			int SortedTable<GenericParamTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Owner;
				}
			}

			// Token: 0x04000EFA RID: 3834
			internal short Number;

			// Token: 0x04000EFB RID: 3835
			internal short Flags;

			// Token: 0x04000EFC RID: 3836
			internal int Owner;

			// Token: 0x04000EFD RID: 3837
			internal int Name;

			// Token: 0x04000EFE RID: 3838
			internal int unsortedIndex;
		}
	}
}
