using System;

namespace System
{
	// Token: 0x020001DC RID: 476
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x00051778 File Offset: 0x0004F978
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
