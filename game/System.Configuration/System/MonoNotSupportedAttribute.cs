using System;

namespace System
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000020C7 File Offset: 0x000002C7
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
