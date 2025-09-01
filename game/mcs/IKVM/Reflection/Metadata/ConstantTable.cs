using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000B8 RID: 184
	internal sealed class ConstantTable : SortedTable<ConstantTable.Record>
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x0001F178 File Offset: 0x0001D378
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Type = mr.ReadInt16();
				this.records[i].Parent = mr.ReadHasConstant();
				this.records[i].Value = mr.ReadBlobIndex();
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Type);
				mw.WriteHasConstant(this.records[i].Parent);
				mw.WriteBlobIndex(this.records[i].Value);
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001F243 File Offset: 0x0001D443
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(2).WriteHasConstant().WriteBlobIndex().Value;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001F25C File Offset: 0x0001D45C
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].Parent);
			}
			base.Sort();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001F298 File Offset: 0x0001D498
		internal static int EncodeHasConstant(int token)
		{
			int num = token >> 24;
			if (num == 4)
			{
				return (token & 16777215) << 2 | 0;
			}
			if (num == 8)
			{
				return (token & 16777215) << 2 | 1;
			}
			if (num != 23)
			{
				throw new InvalidOperationException();
			}
			return (token & 16777215) << 2 | 2;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001F2E4 File Offset: 0x0001D4E4
		internal object GetRawConstantValue(Module module, int parent)
		{
			SortedTable<ConstantTable.Record>.Enumerator enumerator = base.Filter(parent).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				throw new InvalidOperationException();
			}
			int num = enumerator.Current;
			ByteReader blob = module.GetBlob(module.Constant.records[num].Value);
			switch (module.Constant.records[num].Type)
			{
			case 2:
				return blob.ReadByte() > 0;
			case 3:
				return blob.ReadChar();
			case 4:
				return blob.ReadSByte();
			case 5:
				return blob.ReadByte();
			case 6:
				return blob.ReadInt16();
			case 7:
				return blob.ReadUInt16();
			case 8:
				return blob.ReadInt32();
			case 9:
				return blob.ReadUInt32();
			case 10:
				return blob.ReadInt64();
			case 11:
				return blob.ReadUInt64();
			case 12:
				return blob.ReadSingle();
			case 13:
				return blob.ReadDouble();
			case 14:
			{
				char[] array = new char[blob.Length / 2];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = blob.ReadChar();
				}
				return new string(array);
			}
			case 18:
				if (blob.ReadInt32() != 0)
				{
					throw new BadImageFormatException();
				}
				return null;
			}
			throw new BadImageFormatException();
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001F484 File Offset: 0x0001D684
		public ConstantTable()
		{
		}

		// Token: 0x040003D7 RID: 983
		internal const int Index = 11;

		// Token: 0x0200034C RID: 844
		internal struct Record : SortedTable<ConstantTable.Record>.IRecord
		{
			// Token: 0x170008A8 RID: 2216
			// (get) Token: 0x0600261E RID: 9758 RVA: 0x000B5638 File Offset: 0x000B3838
			int SortedTable<ConstantTable.Record>.IRecord.SortKey
			{
				get
				{
					return ConstantTable.EncodeHasConstant(this.Parent);
				}
			}

			// Token: 0x170008A9 RID: 2217
			// (get) Token: 0x0600261F RID: 9759 RVA: 0x000B5645 File Offset: 0x000B3845
			int SortedTable<ConstantTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Parent;
				}
			}

			// Token: 0x04000EB4 RID: 3764
			internal short Type;

			// Token: 0x04000EB5 RID: 3765
			internal int Parent;

			// Token: 0x04000EB6 RID: 3766
			internal int Value;
		}
	}
}
