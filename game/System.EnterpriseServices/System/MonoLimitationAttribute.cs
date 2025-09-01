using System;

namespace System
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000206F File Offset: 0x0000026F
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
