using System;

namespace System
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002083 File Offset: 0x00000283
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
