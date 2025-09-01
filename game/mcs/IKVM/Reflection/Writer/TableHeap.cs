using System;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200007F RID: 127
	internal sealed class TableHeap : Heap
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00014561 File Offset: 0x00012761
		internal void Freeze(MetadataWriter mw)
		{
			if (this.frozen)
			{
				throw new InvalidOperationException();
			}
			this.frozen = true;
			this.unalignedlength = TableHeap.GetLength(mw);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00014584 File Offset: 0x00012784
		protected override void WriteImpl(MetadataWriter mw)
		{
			Table[] tables = mw.ModuleBuilder.GetTables();
			mw.Write(0);
			int mdstreamVersion = mw.ModuleBuilder.MDStreamVersion;
			mw.Write((byte)(mdstreamVersion >> 16));
			mw.Write((byte)mdstreamVersion);
			byte b = 0;
			if (mw.ModuleBuilder.Strings.IsBig)
			{
				b |= 1;
			}
			if (mw.ModuleBuilder.Guids.IsBig)
			{
				b |= 2;
			}
			if (mw.ModuleBuilder.Blobs.IsBig)
			{
				b |= 4;
			}
			mw.Write(b);
			mw.Write(16);
			long num = 1L;
			long num2 = 0L;
			foreach (Table table in tables)
			{
				if (table != null && table.RowCount > 0)
				{
					num2 |= num;
				}
				num <<= 1;
			}
			mw.Write(num2);
			mw.Write(24190111578624L);
			foreach (Table table2 in tables)
			{
				if (table2 != null && table2.RowCount > 0)
				{
					mw.Write(table2.RowCount);
				}
			}
			foreach (Table table3 in tables)
			{
				if (table3 != null && table3.RowCount > 0)
				{
					int position = mw.Position;
					table3.Write(mw);
				}
			}
			mw.Write(0);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000146E8 File Offset: 0x000128E8
		private static int GetLength(MetadataWriter mw)
		{
			int num = 24;
			foreach (Table table in mw.ModuleBuilder.GetTables())
			{
				if (table != null && table.RowCount > 0)
				{
					num += 4;
					num += table.GetLength(mw);
				}
			}
			return num + 1;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00014559 File Offset: 0x00012759
		public TableHeap()
		{
		}
	}
}
