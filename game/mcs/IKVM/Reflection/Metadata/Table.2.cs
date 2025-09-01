using System;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000AB RID: 171
	internal abstract class Table<T> : Table
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001E6D5 File Offset: 0x0001C8D5
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x0001E6DD File Offset: 0x0001C8DD
		internal sealed override int RowCount
		{
			get
			{
				return this.rowCount;
			}
			set
			{
				this.rowCount = value;
				this.records = new T[value];
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00002CD4 File Offset: 0x00000ED4
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001E6F4 File Offset: 0x0001C8F4
		internal int AddRecord(T newRecord)
		{
			if (this.rowCount == this.records.Length)
			{
				Array.Resize<T>(ref this.records, Math.Max(16, this.records.Length * 2));
			}
			T[] array = this.records;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = newRecord;
			return this.rowCount;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001E750 File Offset: 0x0001C950
		internal int AddVirtualRecord()
		{
			int result = this.rowCount + 1;
			this.rowCount = result;
			return result;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal override void Write(MetadataWriter mw)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001E76E File Offset: 0x0001C96E
		protected Table()
		{
		}

		// Token: 0x040003C9 RID: 969
		internal T[] records = Empty<T>.Array;

		// Token: 0x040003CA RID: 970
		protected int rowCount;
	}
}
