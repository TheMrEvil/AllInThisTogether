using System;

namespace System.Security.Principal
{
	/// <summary>Defines the basic functionality of a principal object.</summary>
	// Token: 0x020004DF RID: 1247
	public interface IPrincipal
	{
		/// <summary>Gets the identity of the current principal.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.</returns>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060031E7 RID: 12775
		IIdentity Identity { get; }

		/// <summary>Determines whether the current principal belongs to the specified role.</summary>
		/// <param name="role">The name of the role for which to check membership.</param>
		/// <returns>
		///   <see langword="true" /> if the current principal is a member of the specified role; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031E8 RID: 12776
		bool IsInRole(string role);
	}
}
