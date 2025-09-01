using System;

namespace System
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002083 File Offset: 0x00000283
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
