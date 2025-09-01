using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Confirms that a code assembly originates in the global assembly cache (GAC) as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000410 RID: 1040
	[ComVisible(true)]
	[Serializable]
	public sealed class GacInstalled : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.GacInstalled" /> class.</summary>
		// Token: 0x06002A89 RID: 10889 RVA: 0x00097E0A File Offset: 0x0009600A
		public GacInstalled()
		{
		}

		/// <summary>Creates an equivalent copy of the current object.</summary>
		/// <returns>An equivalent copy of <see cref="T:System.Security.Policy.GacInstalled" />.</returns>
		// Token: 0x06002A8A RID: 10890 RVA: 0x0009A067 File Offset: 0x00098267
		public object Copy()
		{
			return new GacInstalled();
		}

		/// <summary>Creates a new identity permission that corresponds to the current object.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission.</param>
		/// <returns>A new identity permission that corresponds to the current object.</returns>
		// Token: 0x06002A8B RID: 10891 RVA: 0x0009A06E File Offset: 0x0009826E
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new GacIdentityPermission();
		}

		/// <summary>Indicates whether the current object is equivalent to the specified object.</summary>
		/// <param name="o">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Security.Policy.GacInstalled" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A8C RID: 10892 RVA: 0x0009A075 File Offset: 0x00098275
		public override bool Equals(object o)
		{
			return o != null && o is GacInstalled;
		}

		/// <summary>Returns a hash code for the current object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06002A8D RID: 10893 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Returns a string representation of the current  object.</summary>
		/// <returns>A string representation of the current object.</returns>
		// Token: 0x06002A8E RID: 10894 RVA: 0x0009A085 File Offset: 0x00098285
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement.ToString();
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000040F7 File Offset: 0x000022F7
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 1;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00088677 File Offset: 0x00086877
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return position;
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x0009A0AC File Offset: 0x000982AC
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position] = '\t';
			return position + 1;
		}
	}
}
