using System;

namespace System.Security
{
	/// <summary>Specifies that code or an assembly performs security-critical operations.</summary>
	// Token: 0x020003D3 RID: 979
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class SecurityCriticalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityCriticalAttribute" /> class.</summary>
		// Token: 0x06002866 RID: 10342 RVA: 0x00002050 File Offset: 0x00000250
		public SecurityCriticalAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityCriticalAttribute" /> class with the specified scope.</summary>
		/// <param name="scope">One of the enumeration values that specifies the scope of the attribute.</param>
		// Token: 0x06002867 RID: 10343 RVA: 0x00092A4A File Offset: 0x00090C4A
		public SecurityCriticalAttribute(SecurityCriticalScope scope)
		{
			this._val = scope;
		}

		/// <summary>Gets the scope for the attribute.</summary>
		/// <returns>One of the enumeration values that specifies the scope of the attribute. The default is <see cref="F:System.Security.SecurityCriticalScope.Explicit" />, which indicates that the attribute applies only to the immediate target.</returns>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x00092A59 File Offset: 0x00090C59
		[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
		public SecurityCriticalScope Scope
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001EA1 RID: 7841
		private SecurityCriticalScope _val;
	}
}
