using System;

namespace System
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000206F File Offset: 0x0000026F
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
