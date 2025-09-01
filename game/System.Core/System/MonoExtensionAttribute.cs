using System;

namespace System
{
	// Token: 0x0200001D RID: 29
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002414 File Offset: 0x00000614
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
