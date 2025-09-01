using System;

namespace System.Security.Principal
{
	/// <summary>Specifies how principal and identity objects should be created for an application domain. The default is <see langword="UnauthenticatedPrincipal" />.</summary>
	// Token: 0x020004E0 RID: 1248
	public enum PrincipalPolicy
	{
		/// <summary>Principal and identity objects for the unauthenticated entity should be created. An unauthenticated entity has <see cref="P:System.Security.Principal.GenericIdentity.Name" /> set to the empty string ("") and <see cref="P:System.Security.Principal.GenericIdentity.IsAuthenticated" /> set to <see langword="false" />.</summary>
		// Token: 0x040022B4 RID: 8884
		UnauthenticatedPrincipal,
		/// <summary>No principal or identity objects should be created.</summary>
		// Token: 0x040022B5 RID: 8885
		NoPrincipal,
		/// <summary>Principal and identity objects that reflect the operating system token associated with the current execution thread should be created, and the associated operating system groups should be mapped into roles.</summary>
		// Token: 0x040022B6 RID: 8886
		WindowsPrincipal
	}
}
