using System;

namespace System.Security.Policy
{
	/// <summary>Defines the method that creates a new identity permission.</summary>
	// Token: 0x020003F9 RID: 1017
	public interface IIdentityPermissionFactory
	{
		/// <summary>Creates a new identity permission for the specified evidence.</summary>
		/// <param name="evidence">The evidence from which to create the new identity permission.</param>
		/// <returns>The new identity permission.</returns>
		// Token: 0x060029AE RID: 10670
		IPermission CreateIdentityPermission(Evidence evidence);
	}
}
