using System;

namespace System
{
	// Token: 0x02000070 RID: 112
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00010D5D File Offset: 0x0000EF5D
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
