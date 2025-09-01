using System;

namespace System.Security
{
	/// <summary>Specifies the scope of a <see cref="T:System.Security.SecurityCriticalAttribute" />.</summary>
	// Token: 0x020003D2 RID: 978
	[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
	public enum SecurityCriticalScope
	{
		/// <summary>The attribute applies only to the immediate target.</summary>
		// Token: 0x04001E9F RID: 7839
		Explicit,
		/// <summary>The attribute applies to all code that follows it.</summary>
		// Token: 0x04001EA0 RID: 7840
		Everything
	}
}
