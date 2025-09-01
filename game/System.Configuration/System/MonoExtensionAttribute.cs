using System;

namespace System
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020C7 File Offset: 0x000002C7
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
