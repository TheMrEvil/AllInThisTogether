using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Controls rights to access Domain Name System (DNS) servers on the network.</summary>
	// Token: 0x0200067E RID: 1662
	[Serializable]
	public sealed class DnsPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.DnsPermission" /> class that either allows unrestricted DNS access or disallows DNS access.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value.</exception>
		// Token: 0x06003456 RID: 13398 RVA: 0x000B650B File Offset: 0x000B470B
		public DnsPermission(PermissionState state)
		{
			this.m_noRestriction = (state == PermissionState.Unrestricted);
		}

		/// <summary>Creates an identical copy of the current permission instance.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.DnsPermission" /> class that is an identical copy of the current instance.</returns>
		// Token: 0x06003457 RID: 13399 RVA: 0x000B651D File Offset: 0x000B471D
		public override IPermission Copy()
		{
			return new DnsPermission(this.m_noRestriction ? PermissionState.Unrestricted : PermissionState.None);
		}

		/// <summary>Creates a permission instance that is the intersection of the current permission instance and the specified permission instance.</summary>
		/// <param name="target">The <see cref="T:System.Net.DnsPermission" /> instance to intersect with the current instance.</param>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> instance that represents the intersection of the current <see cref="T:System.Net.DnsPermission" /> instance with the specified <see cref="T:System.Net.DnsPermission" /> instance, or <see langword="null" /> if the intersection is empty. If both the current instance and <paramref name="target" /> are unrestricted, this method returns a new <see cref="T:System.Net.DnsPermission" /> instance that is unrestricted; otherwise, it returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor <see langword="null" />.</exception>
		// Token: 0x06003458 RID: 13400 RVA: 0x000B6530 File Offset: 0x000B4730
		public override IPermission Intersect(IPermission target)
		{
			DnsPermission dnsPermission = this.Cast(target);
			if (dnsPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && dnsPermission.IsUnrestricted())
			{
				return new DnsPermission(PermissionState.Unrestricted);
			}
			return null;
		}

		/// <summary>Determines whether the current permission instance is a subset of the specified permission instance.</summary>
		/// <param name="target">The second <see cref="T:System.Net.DnsPermission" /> instance to be tested for the subset relationship.</param>
		/// <returns>
		///   <see langword="false" /> if the current instance is unrestricted and <paramref name="target" /> is either <see langword="null" /> or unrestricted; otherwise, <see langword="true" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor <see langword="null" />.</exception>
		// Token: 0x06003459 RID: 13401 RVA: 0x000B6564 File Offset: 0x000B4764
		public override bool IsSubsetOf(IPermission target)
		{
			DnsPermission dnsPermission = this.Cast(target);
			if (dnsPermission == null)
			{
				return this.IsEmpty();
			}
			return dnsPermission.IsUnrestricted() || this.m_noRestriction == dnsPermission.m_noRestriction;
		}

		/// <summary>Checks the overall permission state of the object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.DnsPermission" /> instance was created with <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600345A RID: 13402 RVA: 0x000B659B File Offset: 0x000B479B
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.DnsPermission" /> instance and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> instance that contains an XML-encoded representation of the security object, including state information.</returns>
		// Token: 0x0600345B RID: 13403 RVA: 0x000B65A4 File Offset: 0x000B47A4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(DnsPermission), 1);
			if (this.m_noRestriction)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.DnsPermission" /> instance from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the <see cref="T:System.Net.DnsPermission" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a <see cref="T:System.Net.DnsPermission" /> element.</exception>
		// Token: 0x0600345C RID: 13404 RVA: 0x000B65DB File Offset: 0x000B47DB
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			this.m_noRestriction = PermissionHelper.IsUnrestricted(securityElement);
		}

		/// <summary>Creates a permission instance that is the union of the current permission instance and the specified permission instance.</summary>
		/// <param name="target">The <see cref="T:System.Net.DnsPermission" /> instance to combine with the current instance.</param>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> instance that represents the union of the current <see cref="T:System.Net.DnsPermission" /> instance with the specified <see cref="T:System.Net.DnsPermission" /> instance. If <paramref name="target" /> is <see langword="null" />, this method returns a copy of the current instance. If the current instance or <paramref name="target" /> is unrestricted, this method returns a <see cref="T:System.Net.DnsPermission" /> instance that is unrestricted; otherwise, it returns a <see cref="T:System.Net.DnsPermission" /> instance that is restricted.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor <see langword="null" />.</exception>
		// Token: 0x0600345D RID: 13405 RVA: 0x000B6614 File Offset: 0x000B4814
		public override IPermission Union(IPermission target)
		{
			DnsPermission dnsPermission = this.Cast(target);
			if (dnsPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || dnsPermission.IsUnrestricted())
			{
				return new DnsPermission(PermissionState.Unrestricted);
			}
			return new DnsPermission(PermissionState.None);
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000B6650 File Offset: 0x000B4850
		private bool IsEmpty()
		{
			return !this.m_noRestriction;
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000B665B File Offset: 0x000B485B
		private DnsPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			DnsPermission dnsPermission = target as DnsPermission;
			if (dnsPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(DnsPermission));
			}
			return dnsPermission;
		}

		// Token: 0x04001E8D RID: 7821
		private const int version = 1;

		// Token: 0x04001E8E RID: 7822
		private bool m_noRestriction;
	}
}
