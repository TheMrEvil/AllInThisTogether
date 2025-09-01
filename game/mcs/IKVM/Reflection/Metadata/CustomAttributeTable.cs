using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B9 RID: 185
	internal sealed class CustomAttributeTable : SortedTable<CustomAttributeTable.Record>
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0001F48C File Offset: 0x0001D68C
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Parent = mr.ReadHasCustomAttribute();
				this.records[i].Type = mr.ReadCustomAttributeType();
				this.records[i].Value = mr.ReadBlobIndex();
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteHasCustomAttribute(this.records[i].Parent);
				mw.WriteCustomAttributeType(this.records[i].Type);
				mw.WriteBlobIndex(this.records[i].Value);
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001F557 File Offset: 0x0001D757
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteHasCustomAttribute().WriteCustomAttributeType().WriteBlobIndex().Value;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001F570 File Offset: 0x0001D770
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			int[] indexFixup = moduleBuilder.GenericParam.GetIndexFixup();
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].Type);
				moduleBuilder.FixupPseudoToken(ref this.records[i].Parent);
				if (this.records[i].Parent >> 24 == 42)
				{
					this.records[i].Parent = (42 << 24) + indexFixup[(this.records[i].Parent & 16777215) - 1] + 1;
				}
			}
			base.Sort();
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001F620 File Offset: 0x0001D820
		internal static int EncodeHasCustomAttribute(int token)
		{
			int num = token >> 24;
			if (num <= 17)
			{
				switch (num)
				{
				case 0:
					return (token & 16777215) << 5 | 7;
				case 1:
					return (token & 16777215) << 5 | 2;
				case 2:
					return (token & 16777215) << 5 | 3;
				case 3:
				case 5:
				case 7:
					break;
				case 4:
					return (token & 16777215) << 5 | 1;
				case 6:
					return (token & 16777215) << 5 | 0;
				case 8:
					return (token & 16777215) << 5 | 4;
				case 9:
					return (token & 16777215) << 5 | 5;
				case 10:
					return (token & 16777215) << 5 | 6;
				default:
					if (num == 17)
					{
						return (token & 16777215) << 5 | 11;
					}
					break;
				}
			}
			else
			{
				if (num == 20)
				{
					return (token & 16777215) << 5 | 10;
				}
				switch (num)
				{
				case 23:
					return (token & 16777215) << 5 | 9;
				case 24:
				case 25:
					break;
				case 26:
					return (token & 16777215) << 5 | 12;
				case 27:
					return (token & 16777215) << 5 | 13;
				default:
					switch (num)
					{
					case 32:
						return (token & 16777215) << 5 | 14;
					case 35:
						return (token & 16777215) << 5 | 15;
					case 38:
						return (token & 16777215) << 5 | 16;
					case 39:
						return (token & 16777215) << 5 | 17;
					case 40:
						return (token & 16777215) << 5 | 18;
					case 42:
						return (token & 16777215) << 5 | 19;
					}
					break;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001F7C9 File Offset: 0x0001D9C9
		public CustomAttributeTable()
		{
		}

		// Token: 0x040003D8 RID: 984
		internal const int Index = 12;

		// Token: 0x0200034D RID: 845
		internal struct Record : SortedTable<CustomAttributeTable.Record>.IRecord
		{
			// Token: 0x170008AA RID: 2218
			// (get) Token: 0x06002620 RID: 9760 RVA: 0x000B564D File Offset: 0x000B384D
			int SortedTable<CustomAttributeTable.Record>.IRecord.SortKey
			{
				get
				{
					return CustomAttributeTable.EncodeHasCustomAttribute(this.Parent);
				}
			}

			// Token: 0x170008AB RID: 2219
			// (get) Token: 0x06002621 RID: 9761 RVA: 0x000B565A File Offset: 0x000B385A
			int SortedTable<CustomAttributeTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x04000EB7 RID: 3767
			internal int Parent;

			// Token: 0x04000EB8 RID: 3768
			internal int Type;

			// Token: 0x04000EB9 RID: 3769
			internal int Value;
		}
	}
}
