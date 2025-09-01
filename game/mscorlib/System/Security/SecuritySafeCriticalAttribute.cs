using System;

namespace System.Security
{
	/// <summary>Identifies types or members as security-critical and safely accessible by transparent code.</summary>
	// Token: 0x020003D5 RID: 981
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class SecuritySafeCriticalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecuritySafeCriticalAttribute" /> class.</summary>
		// Token: 0x0600286A RID: 10346 RVA: 0x00002050 File Offset: 0x00000250
		public SecuritySafeCriticalAttribute()
		{
		}
	}
}
