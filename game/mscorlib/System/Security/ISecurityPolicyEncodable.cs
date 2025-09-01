using System;
using System.Security.Policy;

namespace System.Security
{
	/// <summary>Supports the methods that convert permission object state to and from an XML element representation.</summary>
	// Token: 0x020003C8 RID: 968
	public interface ISecurityPolicyEncodable
	{
		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy-level context to resolve named permission set references.</param>
		// Token: 0x06002854 RID: 10324
		void FromXml(SecurityElement e, PolicyLevel level);

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <param name="level">The policy-level context to resolve named permission set references.</param>
		/// <returns>The root element of the XML representation of the policy object.</returns>
		// Token: 0x06002855 RID: 10325
		SecurityElement ToXml(PolicyLevel level);
	}
}
