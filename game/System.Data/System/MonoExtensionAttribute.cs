using System;

namespace System
{
	// Token: 0x0200006E RID: 110
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x00010D5D File Offset: 0x0000EF5D
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
