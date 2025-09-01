using System;

namespace System.Security
{
	/// <summary>Identifies which of the nonpublic <see cref="T:System.Security.SecurityCriticalAttribute" /> members are accessible by transparent code within the assembly.</summary>
	// Token: 0x020003D4 RID: 980
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[Obsolete("SecurityTreatAsSafe is only used for .NET 2.0 transparency compatibility.  Please use the SecuritySafeCriticalAttribute instead.")]
	public sealed class SecurityTreatAsSafeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityTreatAsSafeAttribute" /> class.</summary>
		// Token: 0x06002869 RID: 10345 RVA: 0x00002050 File Offset: 0x00000250
		public SecurityTreatAsSafeAttribute()
		{
		}
	}
}
