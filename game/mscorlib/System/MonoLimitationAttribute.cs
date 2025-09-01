using System;

namespace System
{
	// Token: 0x020001DE RID: 478
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x00051778 File Offset: 0x0004F978
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
