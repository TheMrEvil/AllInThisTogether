using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its global assembly cache membership. This class cannot be inherited.</summary>
	// Token: 0x02000411 RID: 1041
	[ComVisible(true)]
	[Serializable]
	public sealed class GacMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.GacMembershipCondition" /> class.</summary>
		// Token: 0x06002A92 RID: 10898 RVA: 0x0009A0B6 File Offset: 0x000982B6
		public GacMembershipCondition()
		{
		}

		/// <summary>Indicates whether the specified evidence satisfies the membership condition.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified evidence satisfies the membership condition; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A93 RID: 10899 RVA: 0x0009A0C8 File Offset: 0x000982C8
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is GacInstalled)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new <see cref="T:System.Security.Policy.GacMembershipCondition" /> object.</returns>
		// Token: 0x06002A94 RID: 10900 RVA: 0x0009A0FB File Offset: 0x000982FB
		public IMembershipCondition Copy()
		{
			return new GacMembershipCondition();
		}

		/// <summary>Indicates whether the current object is equivalent to the specified object.</summary>
		/// <param name="o">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Security.Policy.GacMembershipCondition" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A95 RID: 10901 RVA: 0x0009A102 File Offset: 0x00098302
		public override bool Equals(object o)
		{
			return o != null && o is GacMembershipCondition;
		}

		/// <summary>Uses the specified XML encoding to reconstruct a security object.</summary>
		/// <param name="e">The <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid membership condition element.</exception>
		// Token: 0x06002A96 RID: 10902 RVA: 0x0009A112 File Offset: 0x00098312
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Uses the specified XML encoding to reconstruct a security object, using the specified policy level context.</summary>
		/// <param name="e">The <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for resolving <see cref="T:System.Security.NamedPermissionSet" /> references.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid membership condition element.</exception>
		// Token: 0x06002A97 RID: 10903 RVA: 0x0009A11C File Offset: 0x0009831C
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		/// <summary>Gets a hash code for the current membership condition.</summary>
		/// <returns>0 (zero).</returns>
		// Token: 0x06002A98 RID: 10904 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the membership condition.</returns>
		// Token: 0x06002A99 RID: 10905 RVA: 0x0009A136 File Offset: 0x00098336
		public override string ToString()
		{
			return "GAC";
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002A9A RID: 10906 RVA: 0x0009A13D File Offset: 0x0009833D
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state, using the specified policy level context.</summary>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for resolving <see cref="T:System.Security.NamedPermissionSet" /> references.</param>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002A9B RID: 10907 RVA: 0x0009A146 File Offset: 0x00098346
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(GacMembershipCondition), this.version);
		}

		// Token: 0x04001F97 RID: 8087
		private readonly int version = 1;
	}
}
