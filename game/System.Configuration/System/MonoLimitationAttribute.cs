using System;

namespace System
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020C7 File Offset: 0x000002C7
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
