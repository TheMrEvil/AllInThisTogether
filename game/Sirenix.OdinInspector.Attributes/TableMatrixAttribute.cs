using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200006E RID: 110
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class TableMatrixAttribute : Attribute
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00003B6A File Offset: 0x00001D6A
		public TableMatrixAttribute()
		{
		}

		// Token: 0x0400012E RID: 302
		public bool IsReadOnly;

		// Token: 0x0400012F RID: 303
		public bool ResizableColumns = true;

		// Token: 0x04000130 RID: 304
		public string VerticalTitle;

		// Token: 0x04000131 RID: 305
		public string HorizontalTitle;

		// Token: 0x04000132 RID: 306
		public string DrawElementMethod;

		// Token: 0x04000133 RID: 307
		public int RowHeight;

		// Token: 0x04000134 RID: 308
		public bool SquareCells;

		// Token: 0x04000135 RID: 309
		public bool HideColumnIndices;

		// Token: 0x04000136 RID: 310
		public bool HideRowIndices;

		// Token: 0x04000137 RID: 311
		public bool RespectIndentLevel = true;

		// Token: 0x04000138 RID: 312
		public bool Transpose;

		// Token: 0x04000139 RID: 313
		public string Labels;
	}
}
