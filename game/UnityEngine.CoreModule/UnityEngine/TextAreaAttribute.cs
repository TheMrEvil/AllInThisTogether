using System;

namespace UnityEngine
{
	// Token: 0x020001DE RID: 478
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class TextAreaAttribute : PropertyAttribute
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x0002323A File Offset: 0x0002143A
		public TextAreaAttribute()
		{
			this.minLines = 3;
			this.maxLines = 3;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00023252 File Offset: 0x00021452
		public TextAreaAttribute(int minLines, int maxLines)
		{
			this.minLines = minLines;
			this.maxLines = maxLines;
		}

		// Token: 0x040007B9 RID: 1977
		public readonly int minLines;

		// Token: 0x040007BA RID: 1978
		public readonly int maxLines;
	}
}
