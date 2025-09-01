using System;

namespace System.Data
{
	// Token: 0x02000127 RID: 295
	internal readonly struct IndexField
	{
		// Token: 0x0600102A RID: 4138 RVA: 0x00043EA6 File Offset: 0x000420A6
		internal IndexField(DataColumn column, bool isDescending)
		{
			this.Column = column;
			this.IsDescending = isDescending;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00043EB6 File Offset: 0x000420B6
		public static bool operator ==(IndexField if1, IndexField if2)
		{
			return if1.Column == if2.Column && if1.IsDescending == if2.IsDescending;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00043ED6 File Offset: 0x000420D6
		public static bool operator !=(IndexField if1, IndexField if2)
		{
			return !(if1 == if2);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00043EE2 File Offset: 0x000420E2
		public override bool Equals(object obj)
		{
			return obj is IndexField && this == (IndexField)obj;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00043EFF File Offset: 0x000420FF
		public override int GetHashCode()
		{
			return this.Column.GetHashCode() ^ this.IsDescending.GetHashCode();
		}

		// Token: 0x040009F7 RID: 2551
		public readonly DataColumn Column;

		// Token: 0x040009F8 RID: 2552
		public readonly bool IsDescending;
	}
}
