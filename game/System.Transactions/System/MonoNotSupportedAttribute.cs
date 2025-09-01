using System;

namespace System
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002083 File Offset: 0x00000283
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
