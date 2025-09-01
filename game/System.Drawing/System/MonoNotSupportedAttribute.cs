using System;

namespace System
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002083 File Offset: 0x00000283
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
