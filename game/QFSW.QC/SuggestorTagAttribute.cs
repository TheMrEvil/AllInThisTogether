using System;

namespace QFSW.QC
{
	// Token: 0x0200004A RID: 74
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
	public abstract class SuggestorTagAttribute : Attribute
	{
		// Token: 0x06000193 RID: 403
		public abstract IQcSuggestorTag[] GetSuggestorTags();

		// Token: 0x06000194 RID: 404 RVA: 0x0000819B File Offset: 0x0000639B
		protected SuggestorTagAttribute()
		{
		}
	}
}
