using System;

namespace System.Security
{
	// Token: 0x020003CD RID: 973
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	internal sealed class DynamicSecurityMethodAttribute : Attribute
	{
		// Token: 0x06002860 RID: 10336 RVA: 0x00002050 File Offset: 0x00000250
		public DynamicSecurityMethodAttribute()
		{
		}
	}
}
