using System;

namespace System
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002083 File Offset: 0x00000283
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
