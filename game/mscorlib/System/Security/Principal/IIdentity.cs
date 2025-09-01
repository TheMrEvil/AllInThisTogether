using System;

namespace System.Security.Principal
{
	/// <summary>Defines the basic functionality of an identity object.</summary>
	// Token: 0x020004DE RID: 1246
	public interface IIdentity
	{
		/// <summary>Gets the name of the current user.</summary>
		/// <returns>The name of the user on whose behalf the code is running.</returns>
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060031E4 RID: 12772
		string Name { get; }

		/// <summary>Gets the type of authentication used.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060031E5 RID: 12773
		string AuthenticationType { get; }

		/// <summary>Gets a value that indicates whether the user has been authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the user was authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060031E6 RID: 12774
		bool IsAuthenticated { get; }
	}
}
