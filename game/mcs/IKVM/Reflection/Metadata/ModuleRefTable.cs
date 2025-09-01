using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C7 RID: 199
	internal sealed class ModuleRefTable : Table<int>
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x00020500 File Offset: 0x0001E700
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i] = mr.ReadStringIndex();
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00020530 File Offset: 0x0001E730
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.WriteStringIndex(this.records[i]);
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0002055C File Offset: 0x0001E75C
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.WriteStringIndex().Value;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002056C File Offset: 0x0001E76C
		internal int FindOrAddRecord(int str)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				if (this.records[i] == str)
				{
					return i + 1;
				}
			}
			return base.AddRecord(str);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		public ModuleRefTable()
		{
		}

		// Token: 0x040003EC RID: 1004
		internal const int Index = 26;
	}
}
