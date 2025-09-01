using System;

namespace System
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000206F File Offset: 0x0000026F
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
