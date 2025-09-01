using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200006C RID: 108
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class TableColumnWidthAttribute : Attribute
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00003AD2 File Offset: 0x00001CD2
		public TableColumnWidthAttribute(int width, bool resizable = true)
		{
			this.Width = width;
			this.Resizable = resizable;
		}

		// Token: 0x04000120 RID: 288
		public int Width;

		// Token: 0x04000121 RID: 289
		public bool Resizable = true;
	}
}
